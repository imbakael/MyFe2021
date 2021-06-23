using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 狂战士之血，提升2点力量，每损失10%血量提升3点力量
public class BerserkerBloodBuff : BuffBase, IModify {

    public BerserkerBloodBuff(Role target, Role caster) : base(target, caster) {
    }

    public override bool CanCreate() {
        return true;
    }

    public void Apply() {
        ChangeAbility();
    }

    // 监听血量，每次损失10%血量提升3点力量
    public void OnHpChange(float curHpRate) {
        float delta = 1f - curHpRate;
        int time = (int)(delta / 0.1f);
        int add = 3 * time;
        parent.offsetAbility.str += add; // 重新计算所有offset
    }
}
