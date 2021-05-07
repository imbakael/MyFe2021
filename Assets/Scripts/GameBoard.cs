using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

// 棋盘，包含tile，所有角色，移动范围等UI
public partial class GameBoard : MonoBehaviour {
    
    public static GameBoard instance;

    [SerializeField] private Tilemap walkMap = default;
    [SerializeField] private Tilemap cannotWalkMap = default;
    [SerializeField] private MapUnit[] playerPrefabs = default;
    [SerializeField] private GameObject[] uiTilePrefabs = default;
    [SerializeField] private Transform tileUIContainer = default;

    private LogicTile[] allLogicTiles;
    private Dictionary<Vector3, LogicTile> worldTiles;
    private List<LogicTile> movementTiles = new List<LogicTile>(); // 当前选中角色移动范围，此变量很重要，很多寻路方面的方法都需要此变量
    private List<GameObject> uiTiles = new List<GameObject>();

    private void Awake() {
        instance = this;
        mapUnitsCollection = new MapUnitsCollection();
        InitLogicTiles();
    }

    private void InitLogicTiles() {
        Vector2Int tileSize = (Vector2Int)walkMap.size;
        allLogicTiles = new LogicTile[tileSize.x * tileSize.y];
        for (int y = 0, i = 0; y < tileSize.y; y++) {
            for (int x = 0; x < tileSize.x; x++, i++) {
                LogicTile tile = allLogicTiles[i] = new LogicTile(x, y);
                if (x > 0) {
                    LogicTile.MakeEastWestNeighbors(tile, allLogicTiles[i - 1]);
                }
                if (y > 0) {
                    LogicTile.MakeNorthSouthNeighbors(tile, allLogicTiles[i - tileSize.x]);
                }
            }
        }
        InitCellPos();
        InitWalkTile();
    }

    private void InitCellPos() {
        worldTiles = new Dictionary<Vector3, LogicTile>();
        int i = 0;
        foreach (Vector3Int cellPos in walkMap.cellBounds.allPositionsWithin) {
            allLogicTiles[i].CellPos = cellPos;
            worldTiles.Add(walkMap.CellToWorld(cellPos), allLogicTiles[i]);
            i++;
        }
    }

    private void InitWalkTile() {
        foreach (Vector3Int cellPos in cannotWalkMap.cellBounds.allPositionsWithin) {
            if (cannotWalkMap.GetTile(cellPos) == null) {
                continue;
            }
            LogicTile tile = worldTiles[walkMap.CellToWorld(cellPos)];
            tile.CanWalk = false;
        }
    }

    public Vector3 GetWorldPos(LogicTile tile) => walkMap.CellToWorld(tile.CellPos);

    public bool IsExistNeighborInMoveRange(LogicTile tile) {
        List<LogicTile> neighbors = tile.GetNeighbors();
        foreach (var item in neighbors) {
            if (IsInMoveRange(item)) {
                return true;
            }
        }
        return false;
    }

    public bool IsInMoveRange(LogicTile tile) => movementTiles.Contains(tile);

    public bool IsExistMoveRange() => uiTiles.Count > 0;

    public LogicTile GetLogicTile(Vector3 worldPos) => worldTiles.TryGetValue(worldPos, out LogicTile tile) ? tile : null;

    public void SetColor(LogicTile tile) {
        walkMap.SetTileFlags(tile.CellPos, TileFlags.None);
        walkMap.SetColor(tile.CellPos, Color.green);
    }

    // -------------以下为计算和显示路径-------------
    public void ShowMoveAndAttackTiles(LogicTile start, int movePower, int attackRange) {
        ClearUITiles();
        ShowMoveTiles(start, movePower);
        ShowAttackTiles(start, attackRange);
    }

    public void ClearUITiles() {
        for (int i = 0; i < uiTiles.Count; i++) {
            Destroy(uiTiles[i]);
        }
        uiTiles.Clear();
    }

    private void ShowMoveTiles(LogicTile start, int movePower) {
        FindMovePaths(start, movePower);
        CreateUITile(movementTiles, UITileType.MOVE);
    }

    public void FindMovePaths(LogicTile startTile, int movePower) {
        movementTiles.Clear();
        ClearTilePath();
        startTile.SetStart(movePower);
        var searchFrontier = new Queue<LogicTile>();
        searchFrontier.Enqueue(startTile);

        while (searchFrontier.Count > 0) {
            LogicTile tile = searchFrontier.Dequeue();
            movementTiles.Add(tile);
            searchFrontier.EnqueueAfterCheckNull(tile.GrowNorth());
            searchFrontier.EnqueueAfterCheckNull(tile.GrowEast());
            searchFrontier.EnqueueAfterCheckNull(tile.GrowSouth());
            searchFrontier.EnqueueAfterCheckNull(tile.GrowWest());
        }
    }

    public void ClearTilePath() {
        foreach (LogicTile tile in allLogicTiles) {
            tile.ClearPath();
        }
    }

    private void CreateUITile(List<LogicTile> tiles, UITileType type) {
        for (int i = 0; i < tiles.Count; i++) {
            GameObject tile = Instantiate(uiTilePrefabs[(int)type]);
            tile.transform.SetParent(tileUIContainer);
            tile.transform.position = GetWorldPos(tiles[i]);
            uiTiles.Add(tile);
        }
    }

    private void ShowAttackTiles(LogicTile start, int attackRange) {
        List<LogicTile> attackTiles = FindAttackPaths(attackRange);
        CreateUITile(attackTiles, UITileType.ATTACK);
    }

    private List<LogicTile> FindAttackPaths(int attackRange) {
        var attackTiles = new List<LogicTile>();
        var searchFrontier = new Queue<LogicTile>();
        List<LogicTile> boundTiles = GetMoveBoundTiles();
        for (int i = 0; i < boundTiles.Count; i++) {
            boundTiles[i].LeftAttack = attackRange;
            searchFrontier.Enqueue(boundTiles[i]);
        }

        while (searchFrontier.Count > 0) {
            LogicTile tile = searchFrontier.Dequeue();
            attackTiles.Add(tile);
            searchFrontier.EnqueueAfterCheckNull(tile.GrowNorthAttack());
            searchFrontier.EnqueueAfterCheckNull(tile.GrowEastAttack());
            searchFrontier.EnqueueAfterCheckNull(tile.GrowSouthAttack());
            searchFrontier.EnqueueAfterCheckNull(tile.GrowWestAttack());
        }

        for (int i = attackTiles.Count - 1; i >= 0; i--) {
            if (boundTiles.Contains(attackTiles[i])) {
                attackTiles.RemoveAt(i);
            }
        }
        return attackTiles;
    }
    
    public List<LogicTile> GetMoveBoundTiles() => LogicTile.GetAllBoundTiles(movementTiles);
}
