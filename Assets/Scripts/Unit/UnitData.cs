using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData {
    // 大地图数据，包括角色移动力，位置，当前状态（待命or灰掉）等
    private int movePower = 1;
    private Vector2Int position = new Vector2Int(3, 3);
    private MapState mapState = MapState.IDLE;
    private LogicTile tile;

    // 战斗数据，包括各种属性值，特技，战前技能等
    private UnitAttr attr;
    // 装备数据，包括身上的武器、道具
    private Item item;

    public UnitData() {
    	
    }
}
