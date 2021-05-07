using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    // 当前行动阵营
    public TeamType CurTeam { get; private set; } = TeamType.ENEMY;
    // 关卡回合数
    private int roundCount = 0;
    // 胜利条件
    private LevelCondition condition;
    // 关卡剧本(剧情or一些aoe触发，如第20回合火山喷发)

    private void Start() {
        condition = new LevelCondition(WinType.AllDie, LoseType.MainRoleDie);
    }
}
