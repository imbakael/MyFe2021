using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    private const int MAX_Movement = 50;
    /// <summary>
    /// 查找两点之间的最短路径
    /// </summary>
    /// <param name="start">起始点</param>
    /// <param name="end">终点</param>
    /// <param name="isMovePowerInfinity">移动力是否为无穷大</param>
    /// <returns></returns>
    public static List<LogicTile> FindPath(LogicTile start, LogicTile end, bool isMovePowerInfinity = false) {
        if (start == end) {
            return new List<LogicTile> { end };
        }
        if (isMovePowerInfinity) {
            GameBoard.instance.FindMovePaths(start, MAX_Movement);
        }
        GameBoard.instance.ClearTilePath();
        var openList = new List<LogicTile>();
        var closeList = new List<LogicTile>();
        openList.Add(start);

        while (openList.Count > 0) {
            LogicTile lowestF = GetLowestF(openList);
            openList.Remove(lowestF);
            closeList.Add(lowestF);
            List<LogicTile> neighbors = lowestF.GetNeighbors();
            for (int i = 0; i < neighbors.Count; i++) {
                LogicTile n = neighbors[i];
                if (closeList.Contains(n) || !CanMoveTo(n)) {
                    continue;
                }
                if (!openList.Contains(n)) {
                    openList.Add(n);
                    n.Parent = lowestF;
                    if (n == end) {
                        return GetPath(n);
                    }
                    n.G = n.Parent.G + n.MoveCost;
                    n.H = GetH(n, end);
                    n.F = n.G + n.H;
                } else {
                    if (lowestF.G + n.MoveCost < n.G) {
                        n.Parent = lowestF;
                        n.G = lowestF.G + n.MoveCost;
                        n.F = n.G + n.H;
                    }
                }
            }
        }
        Debug.LogError("不可能到达！" + ", startpos = (" + start.X + ", " + start.Y + ")  endpos = (" + end.X + ", " + end.Y + ")");
        return new List<LogicTile>();
    }

    private static LogicTile GetLowestF(List<LogicTile> tiles) => 
        tiles
        .OrderBy(t => t.F)
        .FirstOrDefault();

    private static bool CanMoveTo(LogicTile to) => GameBoard.instance.IsInMoveRange(to);

    private static List<LogicTile> GetPath(LogicTile n) {
        var path = new List<LogicTile> { n };
        LogicTile p = n.Parent;
        while (p != null) {
            path.Add(p);
            p = p.Parent;
        }
        path.Reverse();
        return path;
    }

    public static int GetH(LogicTile n, LogicTile end) => Mathf.Abs(n.X - end.X) + Mathf.Abs(n.Y - end.Y);

}
