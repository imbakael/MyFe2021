using System;
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
            .Where(t => !t.IsDead && board.IsExistNeighborInMoveRange(t.Tile))
            .OrderBy(t => AStar.GetH(Tile, t.Tile))
            .FirstOrDefault();
    }

    // 获取攻击范围内离自己最近的敌方单位
    public MapUnit GetNearestUnitInAttackRange() {
        return
            board.GetAllOtherUnits(Team)
            .Where(t => !t.IsDead && AStar.GetH(LastStandTile, t.Tile) <= mapUnitAttr.attackRange)
            .OrderBy(t => AStar.GetH(LastStandTile, t.Tile))
            .FirstOrDefault();
    }

    // 移动到视野中离自己最近的敌方单位旁
    public void MoveToNearestTile() {
        LogicTile targetTile = GetNearestTileOnPath();
        Debug.Assert(targetTile != null, "targetTile == null！");
        MoveTo(targetTile);
    }

    private LogicTile GetNearestTileOnPath() {
        LogicTile targetTile = GetNearestUnitInView().Tile;
        board.FindMovePaths(Tile, mapUnitAttr.movePower);
        if (board.IsExistNeighborInMoveRange(targetTile)) {
            return GetNearestTileFromNeighbor(targetTile);
        }
        List<LogicTile> movementTiles = board.GetMovementTiles();
        LogicTile neighbor = GetNearestTileFromNeighborIgnoreMove(targetTile);
        List<LogicTile> path = AStar.FindPath(Tile, neighbor, true);

        return 
            path.Intersect(movementTiles)
            .Where(t => t.UnitOnTile == null)
            .OrderByDescending(t => path.IndexOf(t))
            .FirstOrDefault();
    }

    private LogicTile GetNearestTileFromNeighbor(LogicTile targetTile) {
        List<LogicTile> neighbors = targetTile.GetNeighbors();
        return 
            neighbors
            .Where(t => board.IsInMoveRange(t) && t.UnitOnTile == null)
            .OrderBy(t => AStar.GetH(Tile, t))
            .FirstOrDefault();
    }

    private LogicTile GetNearestTileFromNeighborIgnoreMove(LogicTile targetTile) {
        List<LogicTile> neighbors = targetTile.GetNeighbors();
        GameBoard.instance.FindMovePaths(Tile, AStar.MAX_Movement);
        for (int i = neighbors.Count - 1; i >= 0; i--) {
            if (!board.IsInMoveRange(neighbors[i])) {
                neighbors.RemoveAt(i);
            }
        }
        return 
            neighbors
            .Where(t => t.CanWalk && t.UnitOnTile == null)
            .OrderBy(t => AStar.GetH(Tile, t))
            .FirstOrDefault();
    }

    public override void Attack() {
        Debug.Log("npc 开始攻击！");
        MapUnit target = GetNearestUnitInAttackRange();
        MapBattleController.Instance.StartMapBattle(this, target);
    }
}
