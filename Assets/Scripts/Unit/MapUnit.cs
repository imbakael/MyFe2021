using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 单个角色，包含大地图和战斗内的所有数据
public class MapUnit : MonoBehaviour
{
    [SerializeField]
    private int playerMovePower = default;
    [SerializeField]
    private int playerAttackRange = default;
    [SerializeField]
    private float moveSpeed = default;
    [SerializeField]
    private Animator animator = default;
    [SerializeField]
    private MapState state = default;

    private TeamType type;
    private LogicTile tile;
    private LogicTile lastStandTile;
    private GameBoard board;

    private void Start() {
        board = GameBoard.instance;
    }

    public void Init(TeamType type, LogicTile tile) {
        this.type = type;
        this.tile = tile;
        lastStandTile = tile;
        tile.UnitOnTile = this;
    }

    public bool CannotOperate() => state == MapState.MOVING;

    public void Action(LogicTile tile) {
        if (tile.UnitOnTile == this) {
            if (state == MapState.IDLE) {
                state = MapState.READY_MOVE;
                board.ShowMoveAndAttackTiles(this.tile, playerMovePower, playerAttackRange);
                SetAnimation(0, -1);
                return;
            }
            if (state == MapState.GRAY) {
                board.ShowMoveAndAttackTiles(this.tile, playerMovePower, playerAttackRange);
                return;
            }
        }
        if (CanMove(tile)) {
            // 移动
            MoveTo(tile);
        } else {
            Cancel();
        }
    }

    private bool CanMove(LogicTile tile) {
        MapUnit unit = tile.UnitOnTile;
        return state == MapState.READY_MOVE && (unit == null || unit == this) && board.IsInMoveRange(tile);
    }

    public void MoveTo(LogicTile destination) {
        //List<LogicTile> path = LogicTile.GetPath(lastStandTile, destination);
        List<LogicTile> path = AStar.PathFind(lastStandTile, destination);
        StartCoroutine(Move(path));
    }

    // 返回原处
    public void GoBack() {
        state = MapState.READY_MOVE;
        transform.position = board.GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
        board.ShowMoveAndAttackTiles(tile, playerMovePower, playerAttackRange);
        SetAnimation(0, -1);
    }

    public void Cancel() {
        if (state != MapState.GRAY) {
            state = MapState.IDLE;
        }
        SetAnimation(0, 0);
        transform.position = board.GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
        lastStandTile = tile;
        board.ClearUITiles();
    }

    // 待机
    public void Standby() {
        state = MapState.GRAY;
        SetAnimation(0, 0, false);
        tile.UnitOnTile = null;
        lastStandTile.UnitOnTile = this;
        tile = lastStandTile;
    }

    public void NextTurn() {
        state = MapState.IDLE;
        SetAnimation(0, 0);
    }

    private void SetAnimation(int x, int y, bool isActive = true) {
        animator.SetInteger("X", x);
        animator.SetInteger("Y", y);
        animator.SetBool("IsActive", isActive);
    }

    private IEnumerator Move(List<LogicTile> tilePath) {
        if (tilePath.Count >= 2) {
            state = MapState.MOVING;
            Vector2Int previousDirection = Vector2Int.zero;
            for (int i = 1; i < tilePath.Count; i++) {
                LogicTile tile = tilePath[i];
                LogicTile previousTile = tilePath[i - 1];
                Vector3 nextPos = board.GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
                Vector2Int currentDirection = new Vector2Int(tile.X - previousTile.X, tile.Y - previousTile.Y);

                if (currentDirection != previousDirection) {
                    SetAnimation(currentDirection.x, currentDirection.y);
                    previousDirection = currentDirection;
                }

                while (Vector3.Distance(transform.position, nextPos) > 0.01f) {
                    transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
                    yield return null;
                }
                transform.position = nextPos;
            }
            lastStandTile = tilePath[tilePath.Count - 1];
        }
        
        //board.ShowMoveAndAttackTiles(destination, 0, playerAttackRange, false);
        state = MapState.READY_MOVE;
    }
}
