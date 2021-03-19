﻿
public enum FSMTriggerID {
    None,
    Default,
    NoHealth, // 生命为0
    FindTarget, // 发现目标
    LoseTarget, // 丢失目标
    InAttackRange, // 进入攻击范围
    OutAttackRange, // 离开攻击范围
    TargetDead, // 目标死亡

    // 大地图相关
    Selected,
    MoveTo,
    Cancle,
    Standby
}