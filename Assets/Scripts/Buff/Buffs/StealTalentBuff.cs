using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 天赋偷取，击杀目标后偷取目标身上至多2个天赋
public class StealTalentBuff : BuffBase
{
    public StealTalentBuff(Role target, Role caster) : base(target, caster) {
    }

    public override bool CanCreate() {
        return true;
    }

    public override void OnKill(DamageInfo damageInfo) {
        // 成功击杀后如果目标身上有天赋则触发UI，玩家可以选择拖拽target身上的被动，上限2个

    }
}
