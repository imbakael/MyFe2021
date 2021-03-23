﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : Singleton<PickUpController>
{
    private LogicTile curTile;
    private MapUnit curUnit;

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            if (curUnit != null && curUnit.CannotOperate()) {
                return;
            }
            LogicTile tile = GetTile();
            if (tile == null) {
                return;
            }
            curTile = tile;
            curUnit?.Action(curTile);
            if (curTile.UnitOnTile != null) {
                curUnit = curTile.UnitOnTile;
                if (curUnit.CannotOperate()) {
                    return;
                }
                curUnit.Action(curTile);
            } else if (!GameBoard.instance.IsExistMoveRange()) {
                curUnit = null;
            }
        }
    }

    private LogicTile GetTile() {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
        return GameBoard.instance.GetLogicTile(worldPoint);
    }
}
