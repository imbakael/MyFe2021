using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

// 棋盘，包含tile，所有角色，移动范围等UI
public class GameBoard : MonoBehaviour {
    
    public static GameBoard instance;

    [SerializeField]
    private Tilemap walkMap = default;
    [SerializeField]
    private Tilemap cannotWalkMap = default;
    [SerializeField]
    private GameObject[] uiTilePrefabs = default;
    [SerializeField]
    private Player[] playerPrefabs = default;
    [SerializeField]
    private Transform tileUI = default;

    private LogicTile[] allLogicTiles;
    private Dictionary<Vector3, LogicTile> worldTiles = new Dictionary<Vector3, LogicTile>();
    private List<LogicTile> movementTiles = new List<LogicTile>(); // 角色移动范围
    private List<GameObject> uiTiles = new List<GameObject>();
    private List<Player> allMyPlayers = new List<Player>();
    private Player currentPlayer = null;

    private void Awake() {
        instance = this;
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

    public void ClickOneTile(LogicTile tile) {
        if (currentPlayer != null) {
            if (currentPlayer.State == MapState.READY_MOVE) {
                Player tileUnit = tile.PlayerOnTile;
                if (tileUnit != null && tileUnit != currentPlayer && tileUnit.State == MapState.IDLE) {
                    currentPlayer.Cancel();
                    currentPlayer = tileUnit;
                    currentPlayer.CanBeSelected();
                    return;
                }

                if (IsInMoveRange(tile)) {
                    currentPlayer.MoveTo(tile);
                } else {
                    currentPlayer.Cancel();
                    currentPlayer = null;
                }
            }
            return;
        }

        Player player = tile.PlayerOnTile;
        if (player != null && player.CanBeSelected()) {
            currentPlayer = player;
        }
    }

    private bool IsInMoveRange(LogicTile tile) => movementTiles.Contains(tile);

    public void Cancel() {
        if (currentPlayer != null && currentPlayer.State == MapState.MOVE_END) {
            currentPlayer.GoBack();
        }
    }

    public void Standby() {
        if (currentPlayer != null && currentPlayer.State == MapState.MOVE_END) {
            ClearUITiles();
            currentPlayer.Standby();
            currentPlayer = null;
        }
    }

    public void NextTurn() {
        for (int i = 0; i < allMyPlayers.Count; i++) {
            allMyPlayers[i].NextTurn();
        }
    }

    // 获取移动路径
    public List<LogicTile> MoveToDestination(LogicTile start, LogicTile end) {
        List<LogicTile> results = new List<LogicTile> { end };
        LogicTile currentTile = end;
        while (currentTile.NextOnPath != null) {
            results.Add(currentTile.NextOnPath);
            currentTile = currentTile.NextOnPath;
        }
        results.Reverse();
        return results;
    }

    public void InitPlayers(int[] tileIndexes) {
        // 参数应该改为Vector2Int[]类型
        for (int i = 0; i < tileIndexes.Length; i++) {
            int index = tileIndexes[i];
            LogicTile tile = allLogicTiles[index];
            Player player = Instantiate(playerPrefabs[0]);
            allMyPlayers.Add(player);
            SetPlayerOnTile(tile, player);
            player.transform.position = GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
        }
    }

    private void SetPlayerOnTile(LogicTile tile, Player p) {
        tile.PlayerOnTile = p;
        p.Tile = tile;
    }

    public Vector3 GetWorldPos(LogicTile tile) => walkMap.CellToWorld(tile.CellPos);

    public void ShowMoveAndAttackTiles(LogicTile startTile, int movePower, int attackRange, bool showMove = true, bool showAttack = true) {
        ClearUITiles();
        FindMovePaths(startTile, movePower);
        if (showMove) {
            CreateUITile(movementTiles, UITileType.MOVE);
        }

        if (showAttack) {
            List<LogicTile> attackTiles = FindAttackPaths(attackRange);
            CreateUITile(attackTiles, UITileType.ATTACK);
        }
    }

    public void ClearUITiles() {
        for (int i = 0; i < uiTiles.Count; i++) {
            Destroy(uiTiles[i]);
        }
        uiTiles.Clear();
    }

    private void FindMovePaths(LogicTile startTile, int movePower) {
        movementTiles.Clear();
        foreach (var tile in allLogicTiles) {
            tile.ClearPath();
        }
        startTile.SetStart(movePower);
        Queue<LogicTile> searchFrontier = new Queue<LogicTile>();
        searchFrontier.Enqueue(startTile);

        while (searchFrontier.Count > 0) {
            LogicTile tile = searchFrontier.Dequeue();
            if (tile != null) {
                movementTiles.Add(tile);
                LogicTile north = tile.GrowNorth();
                if (north != null) {
                    searchFrontier.Enqueue(north);
                }
                LogicTile east = tile.GrowEast();
                if (east != null) {
                    searchFrontier.Enqueue(east);
                }
                LogicTile south = tile.GrowSouth();
                if (south != null) {
                    searchFrontier.Enqueue(south);
                }
                LogicTile west = tile.GrowWest();
                if (west != null) {
                    searchFrontier.Enqueue(west);
                }
            }
        }
    }

    private void CreateUITile(List<LogicTile> tiles, UITileType type) {
        for (int i = 0; i < tiles.Count; i++) {
            GameObject tile = Instantiate(uiTilePrefabs[(int)type]);
            tile.transform.SetParent(tileUI);
            tile.transform.position = GetWorldPos(tiles[i]);
            uiTiles.Add(tile);
        }
    }

    private List<LogicTile> FindAttackPaths(int attackRange) {
        List<LogicTile> attackTiles = new List<LogicTile>();
        Queue<LogicTile> searchFrontier = new Queue<LogicTile>();
        List<LogicTile> marginalTiles = LogicTile.GetAllBoundTiles(movementTiles);
        for (int i = 0; i < marginalTiles.Count; i++) {
            marginalTiles[i].LeftAttack = attackRange;
            searchFrontier.Enqueue(marginalTiles[i]);
        }

        while (searchFrontier.Count > 0) {
            LogicTile tile = searchFrontier.Dequeue();
            if (tile != null) {
                attackTiles.Add(tile);
                LogicTile north = tile.GrowNorthAttack();
                if (north != null) {
                    searchFrontier.Enqueue(north);
                }

                LogicTile east = tile.GrowEastAttack();
                if (east != null) {
                    searchFrontier.Enqueue(east);
                }

                LogicTile south = tile.GrowSouthAttack();
                if (south != null) {
                    searchFrontier.Enqueue(south);
                }

                LogicTile west = tile.GrowWestAttack();
                if (west != null) {
                    searchFrontier.Enqueue(west);
                }
            }
        }

        // 剔除属于移动范围的这些边缘点
        for (int i = attackTiles.Count - 1; i >= 0; i--) {
            if (marginalTiles.Contains(attackTiles[i])) {
                attackTiles.RemoveAt(i);
            }
        }
        return attackTiles;
    }

    public LogicTile GetLogicTile(Vector3 worldPos) => worldTiles.TryGetValue(worldPos, out LogicTile tile) ? tile : null;

    public void SetColor(LogicTile tile) {
        walkMap.SetTileFlags(tile.CellPos, TileFlags.None);
        walkMap.SetColor(tile.CellPos, Color.green);
    }
    
}
