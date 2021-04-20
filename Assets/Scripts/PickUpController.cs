using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpController : Singleton<PickUpController>
{
    private MapUnit curMapUnit;

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            if (EventSystem.current.IsPointerOverGameObject() || CannotOperate()) {
                return;
            }
            LogicTile tile = GetTile();
            if (tile == null) {
                return;
            }
            HandleLastAndCurrentMapUnit(tile);
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            GameBoard.instance.NextTurn(TeamType.MY_ARMY);
        }
    }

    private bool CannotOperate() => curMapUnit != null && curMapUnit.CannotOperate();
    private bool CanOperate() => curMapUnit != null && !curMapUnit.CannotOperate();

    private LogicTile GetTile() {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
        return GameBoard.instance.GetLogicTile(worldPoint);
    }

    // 处理上一个选中的单位和当前单位逻辑
    private void HandleLastAndCurrentMapUnit(LogicTile tile) {
        MapUnit lastUnit = curMapUnit;
        lastUnit?.Click(tile);
        curMapUnit = tile.UnitOnTile ?? (!GameBoard.instance.IsExistMoveRange() ? null : curMapUnit); // 没有任何单位（无论敌我）被选中时，点空地会置空curUnit
        if (lastUnit != curMapUnit) {
            curMapUnit?.Click(tile);
        }
    }

    // 待机
    public void Standby() {
        if (CanOperate()) {
            curMapUnit.Standby();
        }
    }

}
