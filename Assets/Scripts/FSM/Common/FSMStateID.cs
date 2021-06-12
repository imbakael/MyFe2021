
public enum FSMStateID {
    None,
    Default,
    Idle, // 空闲
    Patrol, // 巡逻
    Pursuit, // 追逐
    Attack, // 攻击
    Dead, // 死亡
    Standby // 待机
            // 逃跑
            // 优先攻击hp最低的
}
