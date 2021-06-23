using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 谁来生成这个buff呢？ 可以是某个被动，如点燃：每次普攻有30%几率点燃目标，持续2回合；也可以是aoe，如熔岩地形

// 灼烧buff效果：目标-20%基础防御，每回合开始时受到3点火焰伤害，持续5回合
public class FireBuff : BuffBase, IDamageable, IModify {

    public FireBuff(Role parent, Role caster) : base(parent, caster) {
        duration = 5;
    }

    public override bool CanCreate() {
        // parent火焰抗性 < 50%时触发；如果parent火免疫则不触发
        return parent.offsetAbility.fireResist < 0.5f;
    }

    public void Apply() {
        ChangeAbility();
    }

    public void AddBuff() {
        if (!CanCreate()) {
            return;
        }
        parent.buffContainer.Add(this);
    }

    // 每次parent回合开始时受到3点火焰伤害（计算抗性）
    public override void OnTurnStart() {
        // 生成一次DamageInfo，遍历parent的所有buff，比如有减免火焰伤害，或者受到火焰伤害后回血，或者免死等buff
        parent.buffContainer.HandleDamgeInfoBeHurt(DoDamage());
    }

    public DamageInfo DoDamage() {
        DamageInfo damageInfo = new DamageInfo(caster, parent);
        damageInfo.InitDamage("Fire_3");
        return damageInfo;
    }
}
