using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LogicTile
{
    private const int ATTACK_COST = 1; // 经过此tile所消耗的攻击射程

    public int X { get; private set; }
    public int Y { get; private set; }
    public LogicTile NextOnPath { get; private set; }
    public Vector3Int CellPos { get; set; }
    public Player PlayerOnTile { get; set; }
    public bool CanWalk { get; set; } = true;
    public int LeftAttack { get; set; } // 走到此tile后剩余的攻击射程

    private bool HasPath => distance != int.MaxValue;

    private LogicTile north, east, south, west;
    private int moveCost = 1; // 经过此Tile所消耗的移动力
    private int leftMovePower; // 走到此Tile后剩余的移动力
    private int distance;

    public LogicTile(int x, int y) {
        X = x;
        Y = y;
    }

    public static void MakeNorthSouthNeighbors(LogicTile north, LogicTile south) {
        Debug.Assert(north.south == null && south.north == null, "logicTile已经被初始化！");
        north.south = south;
        south.north = north;
    }

    public static void MakeEastWestNeighbors(LogicTile east, LogicTile west) {
        Debug.Assert(east.west == null && west.east == null, "logicTile已经被初始化！");
        east.west = west;
        west.east = east;
    }

    public void ClearPath() {
        distance = int.MaxValue;
        NextOnPath = null;
        LeftAttack = 0;
        leftMovePower = 0;
    }

    public void SetStart(int unitMovement) {
        leftMovePower = unitMovement;
        distance = 0;
        NextOnPath = null;
    }

    #region 移动范围相关
    public LogicTile GrowNorth() => GrowPathTo(north);
    public LogicTile GrowEast() => GrowPathTo(east);
    public LogicTile GrowSouth() => GrowPathTo(south);
    public LogicTile GrowWest() => GrowPathTo(west);

    private LogicTile GrowPathTo(LogicTile neighbor) {
        if (!HasPath || neighbor == null || neighbor.HasPath || !neighbor.CanWalk || leftMovePower < neighbor.moveCost) {
            return null;
        }
        neighbor.leftMovePower = leftMovePower - neighbor.moveCost;
        neighbor.distance = distance + 1;
        neighbor.NextOnPath = this;
        return neighbor;
    }
    #endregion

    #region 攻击范围相关
    // 获得某一范围内的所有的边缘tile
    public static List<LogicTile> GetAllBoundTiles(List<LogicTile> tiles) => tiles.Where(t => IsTileBound(t, tiles)).ToList();

    /// <summary>
    /// 判定某个tile是否为边缘tile，依次检测东西南北的相邻格是否属于移动范围
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="tiles">移动范围tiles</param>
    /// <returns></returns>
    private static bool IsTileBound(LogicTile tile, List<LogicTile> tiles) {
        if (tile.north != null && !tiles.Contains(tile.north)) {
            return true;
        }

        if (tile.east != null && !tiles.Contains(tile.east)) {
            return true;
        }

        if (tile.south != null && !tiles.Contains(tile.south)) {
            return true;
        }

        if (tile.west != null && !tiles.Contains(tile.west)) {
            return true;
        }

        return false;
    }

    public LogicTile GrowNorthAttack() => GrowAttackPathTo(north);
    public LogicTile GrowEastAttack() => GrowAttackPathTo(east);
    public LogicTile GrowSouthAttack() => GrowAttackPathTo(south);
    public LogicTile GrowWestAttack() => GrowAttackPathTo(west);

    private LogicTile GrowAttackPathTo(LogicTile neighbor) {
        if (!HasPath || neighbor == null || neighbor.HasPath || LeftAttack < ATTACK_COST) {
            return null;
        }
        neighbor.LeftAttack = LeftAttack - ATTACK_COST;
        neighbor.distance = distance + 1;
        return neighbor;
    }
    #endregion
}
