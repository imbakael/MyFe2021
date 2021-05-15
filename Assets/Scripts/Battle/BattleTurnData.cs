using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一次行为数据
// 本质是主动方 -> 被动方做了一个行为（攻击、治疗、睡眠等, 包含动画、特效），被动方产生一个反馈行为（miss或闪白）， 同时双方各产生一个结果（主动方武器耐久降低，被动方掉血等）
public class BattleTurnData {

    public bool IsOver { get; private set; }
    public BattleUnit ActiveUnit { get; private set; }
    public BattleUnit PassiveUnit { get; private set; }

    private BattleAtomicBehavior activeBehavior;
    private BattleAtomicBehavior passiveBehavior;

    public BattleTurnData(BattleUnit active, BattleUnit passive) {
        ActiveUnit = active;
        PassiveUnit = passive;
        activeBehavior = new BattleAtomicBehavior(active.Role);
        passiveBehavior = new BattleAtomicBehavior(passive.Role);
        CalculateOnce(active, passive);
    }

    private void CalculateOnce(BattleUnit active, BattleUnit passive) {
        active.Durability -= 1;
        activeBehavior.animationType = BattleAnimationType.Common;
        activeBehavior.AddActionStr("Durability", -1);

        int damage = GetDamage(active, passive);
        passive.Hp -= damage;
        passiveBehavior.animationType = BattleAnimationType.Damage;
        passiveBehavior.AddActionStr("Hp", -damage);

        if (passive.Hp <= 0) {
            Debug.Log("一次攻防中，relativePassive被杀死");
            IsOver = true;
        }
    }

    private int GetDamage(BattleUnit active, BattleUnit passive) {
        // todo 判断武器类型，可能是物理or魔法攻击
        int damage = active.Atk - passive.Def;
        float factCris = active.Crit - passive.CritAvoid;
        // todo 如果触发必杀则动画类型改为必杀动画
        bool isCris = Random.value <= factCris;
        float factDamage = damage * (isCris ? active.CritTimes : 1f);
        return MyTools.GetRound(factDamage);
    }

    public void HandleResult() {
        Debug.Log("一次攻防---");
        activeBehavior.Handle();
        passiveBehavior.Handle();
    }
}
