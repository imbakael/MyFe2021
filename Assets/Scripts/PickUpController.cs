using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : Singleton<PickUpController>
{
    private GameBoard board;
    private LogicTile currentTIle;
    private MapUnit lastUnit;
    private MapUnit currentUnit;

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            if (currentUnit != null && currentUnit.CannotOperate()) {
                return;
            }
            LogicTile tile = GetTile();
            currentUnit?.GoBack();
            currentUnit = tile.UnitOnTile;
            currentUnit?.Selected();
        }
    }

    private LogicTile GetTile() {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
        return board.GetLogicTile(worldPoint);
    }
}
