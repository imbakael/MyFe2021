using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDeadTrigger : FSMTrigger {
    public override void Init() {
        TriggerID = FSMTriggerID.TargetDead;
    }

    public override bool IsTrigger(FSMData data) {
        return data.IsTargetDead();
    }
}
