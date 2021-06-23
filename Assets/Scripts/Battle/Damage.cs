using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 此伤害值不考虑暴击，所以最终伤害 = 此值 * 如果暴击的暴击倍率
public class Damage
{
    public enum DamageType {
        Physics,
        Fire,
        Wind,
        Thunder,
        Light,
        Dark
    }

    private Dictionary<DamageType, float> damageBase;
    private Dictionary<DamageType, float> damageOffset;

    // 初始化基础伤害
    public Damage(string config) { // config举例：Physics_20|Fire_10
        damageBase = new Dictionary<DamageType, float>();
        damageOffset = new Dictionary<DamageType, float>();
        string[] array = config.Split('|');
        for (int i = 0; i < array.Length; i++) {
            string item = array[i];
            string[] data = item.Split('_');
            string damageType = data[0];
            string value = data[1];
            DamageType type = (DamageType)Enum.Parse(typeof(DamageType), damageType);
            damageBase[type] = Convert.ToSingle(value);
            damageOffset[type] = 0f;
        }
    }

    // 如降低30%伤害
    public void SetPercentDamge(DamageType damageType, float offsetRate) {
        float baseDamage = damageBase[damageType];
        float offset = baseDamage * offsetRate;
        SetFixedDamge(damageType, offset);
    }

    // 如降低5点伤害
    public void SetFixedDamge(DamageType damageType, float offset) {
        damageOffset[damageType] += offset;
    }

    public float GetRealDamge(DamageType damageType) {
        return damageBase[damageType] + damageOffset[damageType];
    }

    public float GetTotalDamage() {
        float totalBase = damageBase.Values.Sum();
        float totalOffset = damageOffset.Values.Sum();
        return totalBase + totalOffset;
    }
}
