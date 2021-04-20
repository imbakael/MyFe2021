using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCondition
{
    public enum WinType {
        AllDie, // 敌人全灭
        OccupySpot, // 占领地点
        Escape, // 全员逃离
        Defend, // 防守一定回合
    }

    public enum LoseType {
        MainRoleDie, // 主角or某一核心角色死亡
    }

    private WinType win;
    private LoseType lose;

    public LevelCondition(WinType win, LoseType lose) {
        this.win = win;
        this.lose = lose;
    }

    public bool IsWin() {
        return GameBoard.instance.IsAllEnemyDie();
    }

    public bool IsLose() {
        // 主角die
        return false;
    }
}
