using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 每次伤害请求都会产生一个DamageInfo，比如我方的一次剑的普通攻击时，UI伤害显示的依据也是DamgeInfo，每个DamageInfo就会在屏幕上跳出一次伤害值UI
public class DamageInfo {

    public Role attacker; // 攻击者，可以为null
    public Role defender; // 受击者
    public Damage damage;
    public bool isHit = true;
    public bool isCrit = false;

    public DamageInfo(Role attacker, Role defender) {
        this.attacker = attacker;
        this.defender = defender;
    }

    public void InitDamage(string config) {
        damage = new Damage(config);
    }

    public float GetFinalDamge() {
        return damage.GetTotalDamage() * (1 - defender.PhysicsResist) * (isCrit ? attacker.CritTimes : 1f);
    }

}
