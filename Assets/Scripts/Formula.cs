using System;
using UnityEngine;

// 各种公式
public static class Formula {

    // 伤害公式 (最终攻击 - 最终防御) * (1 - 最终抗性) * (暴击？最终暴击倍率 : 1)
    public static float GetDamge(Role attacker, Role defender) {
        return 
            (attacker.Attack - defender.Defence)
            * (1 - defender.PhysicsResist)
            * (IsCrit(attacker, defender) ? attacker.CritTimes : 1f);
    }

    private static bool IsCrit(Role attacker, Role defender) {
        float critRate = attacker.Crit - defender.CritAvoid;
        return UnityEngine.Random.value <= critRate;
    }

}
