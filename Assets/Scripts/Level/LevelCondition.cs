using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCondition
{
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
        return GameBoard.instance.IsMainRoleDie();
    }
}
