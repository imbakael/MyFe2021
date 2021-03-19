using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAttackRangeTrigger : FSMTrigger
{
    public override void Init() {
        TriggerID = FSMTriggerID.InAttackRange;
    }

    public override bool IsTrigger(FSMData data) {
        return data.IsInAttackRange();
    }

}
