using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutAttackRangeTrigger : FSMTrigger {
    public override void Init() {
        TriggerID = FSMTriggerID.OutAttackRange;
    }

    public override bool IsTrigger(FSMData data) {
        return data.IsOutAttackRange();
    }
}
