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

    public MapState State { get; private set; } = MapState.IDLE;
    public LogicTile Tile { get; set; }

    private TeamType type;
    private GameBoard board;
    private LogicTile destination;

    private void Start() {
        board = GameBoard.instance;
    }

    public void Init(TeamType type) {
        this.type = type;
    }

    public bool CannotOperate() => State == MapState.MOVING;

    public void Selected() {

    }

    public bool CanBeSelected() {
        if (State == MapState.IDLE) {
            State = MapState.READY_MOVE;
            board.ShowMoveAndAttackTiles(Tile, playerMovePower, playerAttackRange);
            SetAnimation(0, -1);
            return true;
        }
        return false;
    }

    public void MoveTo(LogicTile destination) {
        if (destination.UnitOnTile != null && destination.UnitOnTile != this) {
            return;
        }

        List<LogicTile> tilePath = LogicTile.GetPath(Tile, destination);
        StartCoroutine(Move(tilePath));
    }

    // 返回原处
    public void GoBack() {
        State = MapState.READY_MOVE;
        transform.position = board.GetWorldPos(Tile) + new Vector3(0.5f, 0, 0);
        board.ShowMoveAndAttackTiles(Tile, playerMovePower, playerAttackRange);
        SetAnimation(0, -1);
    }

    public void Cancel() {
        State = MapState.IDLE;
        SetAnimation(0, 0);
        board.ClearUITiles();
    }

    // 待机
    public void Standby() {
        State = MapState.GRAY;
        SetAnimation(0, 0, false);
        Tile.UnitOnTile = null;
        destination.UnitOnTile = this;
        Tile = destination;
    }

    public void NextTurn() {
        State = MapState.IDLE;
        SetAnimation(0, 0);
    }

    private void SetAnimation(int x, int y, bool isActive = true) {
        animator.SetInteger("X", x);
        animator.SetInteger("Y", y);
        animator.SetBool("IsActive", isActive);
    }

    private IEnumerator Move(List<LogicTile> tilePath) {
        if (tilePath.Count >= 2) {
            State = MapState.MOVING;
            board.ClearUITiles();
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
            }
        }
        destination = tilePath[tilePath.Count - 1];
        board.ShowMoveAndAttackTiles(destination, 0, playerAttackRange, false);
        State = MapState.MOVE_END;
    }
}
