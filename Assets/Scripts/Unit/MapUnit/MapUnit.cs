﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 地图单位，只包含地图相关操作的数据和方法
public abstract class MapUnit : MonoBehaviour
{
    [SerializeField] protected MapUnitAttr mapUnitAttr = default;
    [SerializeField] protected MapState state = default;

    public bool IsDead { get; private set; }
    public TeamType Team { get; private set; }
    public LogicTile Tile { get; private set; }
    public LogicTile LastStandTile { get; protected set; }

    public Action moveEnd;

    protected GameBoard board;

    private Animator animator;
    public Role role;

    private void Awake() {
        board = GameBoard.instance;
        animator = GetComponent<Animator>();
    }

    // todo 应该把role也初始化
    public void Init(TeamType team, LogicTile tile) {
        Team = team;
        Tile = LastStandTile = tile;
        tile.UnitOnTile = this;
        role = team == TeamType.My ? new Role(400, 10, 5, 100, 100, 40, 8) : 
            new Role(600, 6, 2, 0, 0, 20, 7);
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

    public void Standby() {
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

    public bool CannotOperate() => state == MapState.MOVING; // 移动中不可操作

    public void SetAnimation(int x, int y, bool isActive = true) {
        animator.SetInteger("X", x);
        animator.SetInteger("Y", y);
        animator.SetBool("IsActive", isActive);
    }

    public MapState GetMapState() => state;
}