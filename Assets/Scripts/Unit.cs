using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitAttr Attr { get; }
    
    public const int MAX_ITEM_NUM = 5; // 最大可持有道具数量（背包格子上限）
    public List<Item> Items { get; set; } // 身上的所有道具
    public List<Item> WearingItems { get; set; } // 装备中的Item（武器、盔甲、鞋子）
    public List<int> SkillIds { get; set; } // 技能id
    public List<int> TalentIds { get; set; } // 天赋id
    public List<int> StatusIds { get; set; } // 身上的持续性buff或debuff的id
    public MapState State { get; set; } = MapState.IDLE; // 大地图状态
    public DamageType DamType { get; set; } = DamageType.SWORD; // 伤害类型

    #region 战斗相关
    public FighterState FightState { get; set; } = FighterState.IDLE;
    public Unit Target { get; set; } // 战斗时的对手
    public abstract IEnumerator AttackTo(); // 对目标进行攻击or治疗
    public abstract void TakeDamage(int damage);
    public abstract void Die();
    #endregion

    // 计算物理伤害
    //public static int GetPhysicsDamage(Unit attackUnit, Unit defendUnit) {
    //    return Formula.GetActualDamage(attackUnit.attr.Attack, defendUnit.attr.Defend, defendUnit.attr.PhysicsResist);
    //}

    //// 计算魔法伤害
    //public static int GetMagicDamage(Unit attackUnit, Unit defendUnit) {
    //    return Formula.GetActualDamage(attackUnit.attr.Attack, defendUnit.DefOrMagicDef(attackUnit.DamType), defendUnit.GetResist(attackUnit.DamType));
    //}

    //// 获得物理防御or魔防
    //public int DefOrMagicDef(DamageType type) {
    //    if (type == DamageType.PHYSICS) {
    //        return attr.Defend;
    //    }
    //    return attr.MagicDefend;
    //}

    // 获得对应抗性
    //public float GetResist(DamageType type) {
    //    switch (type) {
    //        case DamageType.PHYSICS:
    //            return Attr.PhysicsResist;
    //        case DamageType.LIGHT:
    //            return Attr.LightResist;
    //        case DamageType.DARK:
    //            return Attr.DarkResist;
    //        case DamageType.FIRE:
    //            return Attr.FireResist;
    //        case DamageType.THUNDER:
    //            return Attr.ThunderResist;
    //        case DamageType.WIND:
    //            return Attr.WindResist;
    //    }
    //    Debug.LogError("错误的攻击类型：" + type);
    //    return 0;
    //}

}
