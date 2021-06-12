using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 地图单位，只包含地图相关操作的数据和方法
public abstract class MapUnit : MonoBehaviour
{
    [SerializeField] protected MapUnitAttr mapUnitAttr = default;
    [SerializeField] protected MapState state = default;

    public bool IsDead {
        get {
            return Role.Hp <= 0;
        }
    }

    public int GetMovePower { get { return mapUnitAttr.movePower; } }
    public TeamType Team { get; private set; }
    public LogicTile Tile { get; private set; }
    public LogicTile LastStandTile { get; protected set; }
    public Role Role { get; private set; } // 角色数据，包括属性、道具、天赋等

    public Action moveEnd;
    public int classId;

    protected GameBoard board;

    private Animator animator;

    private void Awake() {
        board = GameBoard.instance;
        animator = GetComponentInChildren<Animator>();
    }

    public void Init(Role role, LogicTile tile, int state) {
        Role = role;
        Team = role.Team;
        Tile = LastStandTile = tile;
        tile.UnitOnTile = this;
        if (state == (int)MapState.IDLE) {
            SetIdle();
        } else if (state == (int)MapState.GRAY) {
            SetGray();
        }
    }

    // 地图单位都可以被点击、移动、攻击、待机

    public abstract void Click(LogicTile clickTile);

    public virtual void MoveTo(LogicTile destination) {
        List<LogicTile> path = AStar.FindPath(LastStandTile, destination);
        StartCoroutine(Move(path));
    }

    private IEnumerator Move(List<LogicTile> tilePath) {
        if (tilePath.Count >= 2) {
            state = MapState.MOVING;
            Vector2Int previousDirection = Vector2Int.zero;
            for (int i = 1; i < tilePath.Count; i++) {
                LogicTile tile = tilePath[i];
                LogicTile previousTile = tilePath[i - 1];
                Vector3 nextPos = board.GetWorldPos(tile) + new Vector3(0.5f, 0, 0);
                var curDirection = new Vector2Int(tile.X - previousTile.X, tile.Y - previousTile.Y);

                if (curDirection != previousDirection) {
                    SetAnimation(curDirection.x, curDirection.y);
                    previousDirection = curDirection;
                }
                AudioController.Instance.Play("LightFootSteps1");
                while (Vector3.Distance(transform.position, nextPos) > 0.01f) {
                    transform.position = Vector3.MoveTowards(transform.position, nextPos, mapUnitAttr.moveSpeed * Time.deltaTime);
                    yield return null;
                }
                transform.position = nextPos;
            }
            LastStandTile = tilePath[tilePath.Count - 1];
        }
        state = MapState.READY_MOVE;
        moveEnd?.Invoke();
        moveEnd = null;
    }

    public virtual void Attack() { }

    public virtual void Standby() {
        if (state == MapState.GRAY) {
            return;
        }
        state = MapState.GRAY;
        SetAnimation(0, 0, false);
        Arrive();
    }

    private void Arrive() {
        Tile.UnitOnTile = null;
        Tile = LastStandTile;
        Tile.UnitOnTile = this;
        board.ClearUITiles();
    }

    public void SetIdle() {
        state = MapState.IDLE;
        SetAnimation(0, 0);
    }

    public void SetGray() {
        state = MapState.GRAY;
        SetAnimation(0, 0, false);
    }

    public bool CannotOperate() => state == MapState.MOVING; // 移动中不可操作
    public bool IsActionOver() => state == MapState.GRAY; // 本单位行动结束

    public void SetAnimation(int x, int y, bool isActive = true) {
        animator.SetInteger("X", x);
        animator.SetInteger("Y", y);
        animator.SetBool("IsActive", isActive);
    }

    public void Dead() {
        if (GamePanel.isMapBattleOpen) {
            AudioController.Instance.Play("CombatDeath");
        }
        Tile.UnitOnTile = null;
        LastStandTile.UnitOnTile = null;
        gameObject.SetActive(false);
    }

    public int GetMapState() => (int)state;
}
