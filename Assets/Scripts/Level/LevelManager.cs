using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    // 当前行动阵营
    public TeamType CurTeam { get; set; } = TeamType.My;
    // 关卡回合数
    private int roundCount = 0;
    // 胜利条件
    private LevelCondition condition;
    // 关卡剧本

    private void Start() {
        condition = new LevelCondition(WinType.AllDie, LoseType.MainRoleDie);
    }
}
