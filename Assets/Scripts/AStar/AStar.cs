using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    public static List<LogicTile> PathFind(LogicTile start, LogicTile end) {
        if (start == end) {
            return new List<LogicTile> { end };
        }
        GameBoard.instance.ClearLogicTiles();
        List<LogicTile> openList = new List<LogicTile>();
        List<LogicTile> closeList = new List<LogicTile>();
        openList.Add(start);

        while (openList.Count > 0) {
            LogicTile lowestF = GetLowestF(openList);
            openList.Remove(lowestF);
            closeList.Add(lowestF);
            List<LogicTile> neighbors = lowestF.GetNeighbors();
            for (int i = neighbors.Count - 1; i >= 0; i--) {
                if (!GameBoard.instance.IsInMoveRange(neighbors[i])) {
                    neighbors.RemoveAt(i);
                }
            }
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
        Debug.LogError("不可能到达！");
        return null;
    }

    private static int GetH(LogicTile n, LogicTile end) {
        return Mathf.Abs(n.X - end.X) + Mathf.Abs(n.Y - end.Y);
    }

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

    private static bool CanMoveTo(LogicTile to) => GameBoard.instance.IsInMoveRange(to);

    private static LogicTile GetLowestF(List<LogicTile> tiles) {
        var temp = tiles.OrderBy(t => t.F).ToList();
        return temp[0];
    }
}
