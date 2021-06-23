using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 中毒debuff，每回合减少3hp(固定伤害)，持续3回合
public class PoisonBuff : BuffBase, IDamageable
{
    public PoisonBuff(Role target, Role caster) : base(target, caster) {
        duration = 3;
    }

    public override bool CanCreate() {
        return true;
    }

    public override void OnTurnStart() {
        parent.Hp -= 3; // 应该重新走整个damageInfo流程，因为敌人可能有不死等buff
        duration -= 1;
    }

    public DamageInfo DoDamage() {
        throw new System.NotImplementedException();
    }
}
