public enum UITileType {
    MOVE, // 移动格子
    ATTACK // 攻击格子
}

public enum MapState {
    IDLE, // 空闲
    READY_MOVE, // 准备移动
    MOVING, // 移动中
    MOVE_END, // 移动结束
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
    MY_ARMY, // 我方
    ENEMY, // 敌方
    ALLY, // 友军
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
