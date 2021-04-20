using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 地图单位，只包含地图相关操作的数据和方法
public abstract class MapUnit : MonoBehaviour
{
    [SerializeField]
    protected MapUnitAttr mapUnitAttr;
    [SerializeField]
    protected MapState state = default; // 状态标识，非状态机

    public bool IsDead { get; set; }

    public TeamType team;

    protected GameBoard board;
    protected LogicTile tile;
    protected LogicTile lastStandTile;

    private Animator animator;

    private void Awake() {
        board = GameBoard.instance;
        animator = GetComponent<Animator>();
    }

    public void Init(TeamType team, LogicTile tile) {
        this.team = team;
        this.tile = tile;
        lastStandTile = tile;
        tile.UnitOnTile = this;
    }

    // 地图单位可以被点击、移动、攻击、待机等
    public abstract void Click(LogicTile clickTile);

    public abstract void MoveTo(LogicTile destination);

    protected IEnumerator Move(List<LogicTile> tilePath) {
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
                    transform.position = Vector3.MoveTowards(transform.position, nextPos, mapUnitAttr.moveSpeed * Time.deltaTime);
                    yield return null;
                }
                transform.position = nextPos;
            }
            lastStandTile = tilePath[tilePath.Count - 1];
        }

        state = MapState.READY_MOVE;
    }

    public virtual void Attack() { }

    // 待机
    public void Standby() {
        if (state == MapState.GRAY) {
            return;
        }
        state = MapState.GRAY;
        SetAnimation(0, 0, false);
        Arrive();
    }

    protected void Arrive() {
        tile.UnitOnTile = null;
        tile = lastStandTile;
        tile.UnitOnTile = this;
        board.ClearUITiles();
    }

    public void NextTurn() {
        state = MapState.IDLE;
        SetAnimation(0, 0);
    }

    public bool CannotOperate() => state == MapState.MOVING; // 移动中时不可操作

    protected void SetAnimation(int x, int y, bool isActive = true) {
        animator.SetInteger("X", x);
        animator.SetInteger("Y", y);
        animator.SetBool("IsActive", isActive);
    }

}
