using System.Collections.Generic;
using UnityEngine;

// 玩家控制的地图单位
public class CanControlMapUnit : MapUnit {

    public override void Click(LogicTile clickTile) {
        if (clickTile.UnitOnTile == this) {
            if (state == MapState.IDLE) {
                state = MapState.READY_MOVE;
                SetAnimation(0, -1);
                board.ShowMoveAndAttackTiles(tile, mapUnitAttr.movePower, mapUnitAttr.attackRange);
                return;
            } else if (state == MapState.GRAY) {
                board.ShowMoveAndAttackTiles(tile, mapUnitAttr.movePower, mapUnitAttr.attackRange);
                return;
            }
        }

        if (CanMove(clickTile)) {
            MoveTo(clickTile);
        } else {
            Cancel();
        }
    }

    private bool CanMove(LogicTile tile) {
        MapUnit unit = tile.UnitOnTile;
        return state == MapState.READY_MOVE && (unit == null || unit == this) && board.IsInMoveRange(tile);
    }

    private void Cancel() {
        if (state != MapState.GRAY) {
            state = MapState.IDLE;
            SetAnimation(0, 0);
        }
        transform.position = board.GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
        lastStandTile = tile;
        board.ClearUITiles();
    }

    public override void MoveTo(LogicTile destination) {
        List<LogicTile> path = AStar.FindPath(lastStandTile, destination);
        StartCoroutine(Move(path));
    }
}
