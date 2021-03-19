
public enum FSMStateID {
    None,
    Default,
    Idle, // 空闲
    Patrol, // 巡逻
    Attack, // 攻击
    Pursuit, // 追逐
    Dead, // 死亡

    // 大地图状态
    MapIdle, // 地图_空闲
    MapReadyMove, // 地图_准备移动
    MapMoving, // 地图_移动中
    MapMoveEnd, // 地图_移动结束
    MapGray, // 地图_变灰
}
