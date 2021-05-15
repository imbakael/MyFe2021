using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 玩家控制的地图单位
public class PlayerMapUnit : MapUnit {

    public override void Click(LogicTile clickTile) {
        if (clickTile.UnitOnTile == this) {
            if (state == MapState.IDLE) {
                state = MapState.READY_MOVE;
                SetAnimation(0, -1);
                board.ShowMoveAndAttackTiles(Tile, mapUnitAttr.movePower, mapUnitAttr.attackRange);
                return;
            } else if (state == MapState.GRAY) {
                board.ShowMoveAndAttackTiles(Tile, mapUnitAttr.movePower, mapUnitAttr.attackRange);
                return;
            }
        }

        if (CanMove(clickTile)) {
            MoveTo(clickTile);
        } else {
            MapUnit target = clickTile.UnitOnTile;
            if (CanAttack(target)) {
                Attack(target);
                board.ClearUITiles();
                return;
            }
            //UIManager.Instance.DestroyPanel<UnitSelectedPanel>();
            Cancel();
        }
    }

    private bool CanMove(LogicTile tile) {
        MapUnit unit = tile.UnitOnTile;
        return state == MapState.READY_MOVE && (unit == null || unit == this) && board.IsInMoveRange(tile);
    }

    private bool CanAttack(MapUnit target) {
        return 
            target != null
            && (target.Team == TeamType.ENEMY || target.Team == TeamType.NEUTRAL)
            && AStar.GetH(target.Tile, LastStandTile) <= mapUnitAttr.attackRange;
    }

    private void Attack(MapUnit target) {
        MapBattleController.Instance.StartMapBattle(this, target);
        MapBattleController.Instance.attackEnd = () => {
            if (IsDead) {
                Dead();
            } else {
                Standby();
            }
        };
    }

    public override void Standby() {
        base.Standby();
        List<MapUnit> myUnits = GameBoard.instance.GetTeam(Team);
        if (myUnits.Where(t => !t.IsDead).All(t => t.IsActionOver())) {
            TurnController.isMyTurn = false;
        }
    }

    private void Cancel() {
        if (state != MapState.GRAY) {
            state = MapState.IDLE;
            SetAnimation(0, 0);
        }
        transform.position = board.GetWorldPos(Tile) + new Vector3(0.5f, 0, 0);
        LastStandTile = Tile;
        board.ClearUITiles();
    }

}
