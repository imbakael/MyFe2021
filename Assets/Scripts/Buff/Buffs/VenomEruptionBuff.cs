using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 毒液喷发，一次战斗中每受到20点伤害后会对周围敌人附加一层中毒buff，每次战斗开始时清0
public class VenomEruptionBuff : BuffBase {

    private int damage = 0;

    public VenomEruptionBuff(Role target, Role caster) : base(target, caster) {
    }

    public override bool CanCreate() {
        return true;
    }

    public void OnHpChange(int delta) {
        if (delta > 0) {
            damage += delta;
            if (damage >= 20) {
                damage = 0;
                // 对parent周围所有敌对单位附加一层中毒buff
                // 查找周围敌对单位，然后附加中毒
            }
        }
    }

}
