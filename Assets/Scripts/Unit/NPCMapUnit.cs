using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// npc，非玩家控制单位，也就是友军、敌人、中立单位
public class NPCMapUnit : MapUnit {

    [SerializeField] private int viewRange = 10; // 视野范围，npc只会对视野范围内的敌人进行追击

    public override void Click(LogicTile clickTile) {
        if (clickTile.UnitOnTile == this) {
            board.ShowMoveAndAttackTiles(Tile, mapUnitAttr.movePower, mapUnitAttr.attackRange);
        } else {
            UIManager.Instance.DestroyPanel<UnitSelectedPanel>();
            board.ClearUITiles();
        }
    }

    public override void MoveTo(LogicTile destination) {
        board.FindMovePaths(Tile, mapUnitAttr.movePower);
        base.MoveTo(destination);
    }

    // 获取视野内（且自身可到达）离自己最近的敌方单位
    public MapUnit GetNearestUnitInView() {
        board.FindMovePaths(Tile, viewRange);
        return
            board.GetAllOtherUnits(Team)
            .Where(t => !t.IsDead && board.IsOneNeighborInMoveRange(t.Tile))
            .OrderBy(t => AStar.GetH(Tile, t.Tile))
            .FirstOrDefault();
    }

    public MapUnit GetNearestUnitInAttackRange() {
        return
            board.GetAllOtherUnits(Team)
            .Where(t => !t.IsDead && AStar.GetH(LastStandTile, t.Tile) <= mapUnitAttr.attackRange)
            .OrderBy(t => AStar.GetH(LastStandTile, t.Tile))
            .FirstOrDefault();
    }

    public void MoveToNearestTile() {
        LogicTile target = GetNearestTile();
        Debug.Assert(target != null, "目标tile == null！");
        MoveTo(target);
    }

    private LogicTile GetNearestTile() {
        MapUnit target = GetNearestUnitInView();
        Debug.Assert(target != null, "目标mapUnit == null！");
        board.FindMovePaths(Tile, mapUnitAttr.movePower);
        if (board.IsOneNeighborInMoveRange(target.Tile)) {
            List<LogicTile> targetNeighbors = target.Tile.GetNeighbors();
            LogicTile targetTile = targetNeighbors.Where(t => board.IsInMoveRange(t)).OrderBy(t => AStar.GetH(Tile, t)).FirstOrDefault();
            return targetTile;
        } else {
            List<LogicTile> boundTiles = board.GetMoveBoundTiles();
            List<LogicTile> path = AStar.FindPath(Tile, target.Tile, true);
            var intersectTiles = path.Intersect(boundTiles);
            foreach (LogicTile item in intersectTiles) {
                return item;
            }
            return null;
        }
    }

    public override void Attack() {
        Debug.Log("npc 开始攻击！");
    }
}
