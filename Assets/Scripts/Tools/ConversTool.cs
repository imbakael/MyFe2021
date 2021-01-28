using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversTool {
    public static List<Dictionary<DamageType, float>> relations = new List<Dictionary<DamageType, float>> {
        new Dictionary<DamageType, float> {
            { DamageType.AXE, 0.5f}
        },
        new Dictionary<DamageType, float> {
            { DamageType.SWORD, 0.5f}
        },
        new Dictionary<DamageType, float> {
            { DamageType.SPEAR, 0.5f}
        },
        new Dictionary<DamageType, float> {
            
        },
        new Dictionary<DamageType, float> {
            { DamageType.DARK, 0.5f}
        },
        new Dictionary<DamageType, float> {
            { DamageType.FIRE, 0.5f},
            { DamageType.THUNDER, 0.5f},
            { DamageType.WIND, 0.5f}
        },
        new Dictionary<DamageType, float> {
            { DamageType.THUNDER, 0.5f},
            { DamageType.LIGHT, 0.5f}
        },
        new Dictionary<DamageType, float> {
            { DamageType.WIND, 0.5f},
            { DamageType.LIGHT, 0.5f}
        },
        new Dictionary<DamageType, float> {
            { DamageType.FIRE, 0.5f},
            { DamageType.LIGHT, 0.5f}
        },
    };

    // 根据伤害类型获取克制时的【属性】增加系数
    public static float CompareDamageType(DamageType myType, DamageType otherType) {
        if (relations[(int)myType].TryGetValue(otherType, out float value)) {
            return value;
        }
        return 0;
    }

    // 根据武器类型and兵种类型获得【伤害】增加系数(弓打飞行单位，魔法打重甲)

}
