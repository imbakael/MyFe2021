﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpController : Singleton<PickUpController>
{
    public MapUnit CurMapUnit { get; private set; }

    public event Action<bool> click;

    private void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if (Input.GetMouseButtonUp(0)) {
            if (CannotOperate()) {
                return;
            }
            LogicTile tile = GetTile();
            if (tile == null) {
                return;
            }
            HandleLastAndCurrentMapUnit(tile);
        }
    }

    private bool CannotOperate() => CurMapUnit != null && CurMapUnit.CannotOperate();
    private bool CanOperate() => CurMapUnit != null && !CurMapUnit.CannotOperate();

    public LogicTile GetTile() {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
        return GameBoard.instance.GetLogicTile(worldPoint);
    }

    // 处理上一个选中的单位和当前单位逻辑
    private void HandleLastAndCurrentMapUnit(LogicTile tile) {
        MapUnit previous = CurMapUnit;
        previous?.Click(tile);
        CurMapUnit = tile.UnitOnTile ?? (!GameBoard.instance.IsExistMoveRange() ? null : CurMapUnit); // 没有任何单位（无论敌我）被选中时，点空地会置空curUnit
        if (previous != CurMapUnit && CurMapUnit != null && !MapBattleController.Instance.IsPassive(CurMapUnit)) {
            CurMapUnit.Click(tile);
            UIManager.Instance.CreateUnitSelectedPanel(CurMapUnit);
        } else {
            UIManager.Instance.DestroyPanel<UnitSelectedPanel>();
        }
        click?.Invoke(CurMapUnit != null && CurMapUnit.Team == TeamType.My && CurMapUnit.GetMapState() != 3);
    }

    // 待机
    public bool Standby() {
        if (CanOperate() && CurMapUnit.Team == TeamType.My) {
            CurMapUnit.Standby();
            return true;
        }
        return false;
    }

}
