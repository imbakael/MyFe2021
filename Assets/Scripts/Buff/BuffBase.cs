using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 被动技能和buff区别？被动是由buff来实现，buff是底层，被动不会被驱散永久有效，物品也可能赋予角色被动
// 被动技能在Init时为角色添加一些不可驱散的buff
public abstract class BuffBase
{
    public int id;
    public Role caster; // buff施加者，可以为null
    public Role parent; // buff挂载的目标
    public int layer; // 层数
    public int level; // 等级
    public int duration; // 时长，-1表示无限时间
    public bool canDispel; // 能否被驱散
    public string tag = @"
        {
            'type':'fire',
            'fixed':'def_-3.2|str_2',
            'percent':'def_-0.1'
        }
    "; // fixed是固定值修改，percent是依据基础值的百分比修改

    // 其他，包含描述、图标等

    public BuffBase(Role parent, Role caster) {
        this.parent = parent;
        this.caster = caster;
    }

    // 根据当前buff属性，以及parent身上的属性和buff集合判断此buff能否被创建
    public abstract bool CanCreate();
    public virtual void OnRefresh() { }
    public virtual void OnRemove() { }
    public virtual void OnTurnStart() { } // parent回合开始时

    public virtual void OnHit(DamageInfo damageInfo) { } // 发起攻击时触发，如攻击时30%几率给敌人附加1层灼烧；需要知道target身上的一些信息，如其火抗较高则不会被灼烧
    public virtual void BeHurt(DamageInfo damageInfo) { } // 被攻击时触发，如受到伤害时反弹20%伤害，此时就需要attacker身上的信息
    public virtual void OnKill(DamageInfo damageInfo) { } // 杀死单位时触发，比如击杀后回复自身5点hp
    public virtual void OnBeforeDead(DamageInfo damageInfo) { } // 自身将死的时候触发，如受到致死攻击时保留1hp
    public virtual void OnAfterDead(DamageInfo damageInfo) { } // 自身死亡时触发，如死亡后爆炸

    public void ChangeAbility() {
        Dictionary<string, float> fixedDic = MyTools.GetPropertyDic(tag, "fixed");
        foreach (var item in fixedDic) {
            MyTools.ChangeFieldValue(parent.offsetAbility, item.Key, item.Value);
        }

        Dictionary<string, float> percentDic = MyTools.GetPropertyDic(tag, "percent");
        foreach (var item in percentDic) {
            float baseValue = MyTools.GetFieldValue<float>(parent.baseAbility, item.Key);
            MyTools.ChangeFieldValue(parent.offsetAbility, item.Key, baseValue * item.Value);
        }
    }

    /*
     * 最简单的一个buff：自身攻击力+3，持续2回合，如果再受到此buff则刷新持续时间
     * 或者点燃buff：每次攻击如果命中后则有30%几率给目标附加一层灼烧buff
     * 毒液喷发：累积受到20点伤害后对周围1格所有敌方单位附加一层剧毒buff，上限1层（效果在map时表现）
     * 钢铁之躯buff：防御+5，物理抗性+20%
     * 防爆服buff：如果受到暴击伤害，则免伤30%
     * 
     buff执行流程：
     1、检查当前buff是否可创建，比如目标火免疫，则不创建灼烧buff
     2、buff实例化后，生效之前（还未加入到buff容器中）是会抛出Awake事件
     3、buff生效时（加入到buff容器后）抛出Start事件，在这里执行buff效果，如攻击力+1
     4、当存在此类型buff时执行Refresh刷新流程，如更新buff层数、等级、持续时间等
     5、buff销毁前（还未从容器中移除），执行Remove，由策划配置具体效果
     6、buff销毁后（已经从容器移除），执行Destroy，由策划配置具体
     7、buff还可以创建定时器来触发间隔持续效果，调用StartIntervalThink创建，OnIntervalThink配置具体效果
     */
    
}
