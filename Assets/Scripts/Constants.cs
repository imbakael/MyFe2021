public enum UITileType {
    MOVE, // 移动格子
    ATTACK // 攻击格子
}

public enum MapState {
    IDLE, // 空闲
    READY_MOVE, // 准备移动
    MOVING, // 移动中
    GRAY // 灰掉
}

public enum TipsType {
    ATTR, // 属性
    TALENT, // 天赋
    SKILL // 技能
}

public enum DamageType {
    SWORD, // 剑
    SPEAR, // 枪
    AXE, // 斧
    BOW, // 弓
    LIGHT, // 光
    DARK, // 暗
    FIRE, // 火
    THUNDER, // 雷
    WIND // 风
}

public enum CanWearType {
    WEAPON, // 武器
    ARMOR, // 盔甲
    SHOE, // 鞋子
}

public enum TeamType {
    My, // 我方
    ENEMY, // 敌方
    ALLIANCE, // 友军
    NEUTRAL // 中立
}

public enum BattleState {
    START, // 开始
    ACTIVE_TURN, // 先手方
    PASSIVE_TURN, // 后手方
    OVER // 结束
}

public enum FighterState {
    IDLE, // 空闲
    ATTACK, // 普攻
    CRI, // 必杀
    DODGE, // 躲避
    HURT, // 受伤
    DIE // 死亡
}

public enum WinType {
    AllDie, // 敌人全灭
    OccupySpot, // 占领地点
    Escape, // 全员逃离
    Defend, // 防守一定回合
}

public enum LoseType {
    MainRoleDie, // 主角or某一核心角色死亡
}

public enum PosType {
    Left,
    Right
}

public enum BattleAnimationType {
    Common, // 普攻
    Cris, // 必杀
    Heal, //治疗
    Dodge, // 躲闪
    Damage, // 受到攻击
    Dead // 死亡
}

// 新建一个类，记录每日想法

// 2.主动技分类：地图技and职业技，职业技类似国王的恩赐里的兵种技能，有使用次数限制或cd限制，只有先手方战斗刚开始的时候可以选择释放一次；
// 被动技 = 特技 = 天赋

// 3.主动技(战斗内使用)举例：A、主动增加所有几率型天赋5%的触发几率 B、此次战斗不会死亡，保留1HP，但1回合内无法行动 

// 4.地图技举例(释放后进入待机状态)：A、标记，对被标记单位伤害+50% B、持盾状态，移动力-2，受伤降低50%，但战斗内至多进行一次攻击

// 6.爽点：1-重甲的枪必杀时插进对方体内，对方头上冒一个叹号，然后身后一道超大的龟派气功逐渐消失，屏幕抖动

// 7.年前出一个安卓上的火纹apk，完成一些基本功能，过年时体验

// 8.


