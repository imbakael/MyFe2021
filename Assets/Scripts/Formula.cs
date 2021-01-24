using UnityEngine;

// 各种公式
public static class Formula {

    // 值修正（不能为负数）
    public static int Modify(int value) {
        return value > 0 ? value : 0;
    }

    #region 战斗内
    // 实际伤害(物理/魔法) = (自身攻击 - 敌人防御) * 抗性
    public static int GetActualDamage(int attack, int defend, float resist) {
        return Modify(Mathf.FloorToInt((attack - defend) * (1 - resist)));
    }

    // 实际命中 = 自身命中 - 敌人回避
    public static int GetActualHit(int hit, int dodge) {
        return Modify(hit - dodge);
    }

    // 实际必杀率 = 自身必杀 - 敌人必杀回避
    public static int GetActualCri(int cri, int criDodge) {
        return Modify(cri - criDodge);
    }
    #endregion

    #region 战斗外
    // 攻击力 = 武器攻击力 + 力量/魔力，第二个参数可能是力量or魔力
    public static int GetAttack(int weaponAtk, int str) {
        return weaponAtk + str;
    }

    // 防御/魔防
    public static int GetDefend(int def) {
        return def;
    }

    // 攻速
    public static int GetAttackSpeed(int speed, int weight, int con) {
        return speed - Modify(weight - con);
    }

    // 命中
    public static int GetHit(int weaponHit, int ski, int luck) {
        return weaponHit + ski * 2 + luck / 2;
    }

    // 回避
    public static int GetDodge(int attackSpeed, int luck) {
        return attackSpeed * 2 + luck;
    }

    // 必杀
    public static int GetCri(int weaponCri, int ski) {
        return weaponCri + ski / 2;
    }

    // 必杀回避
    public static int GetCriDodge(int luck) {
        return luck;
    }
    #endregion

}
