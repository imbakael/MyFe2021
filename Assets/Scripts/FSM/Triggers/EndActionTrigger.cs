using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndActionTrigger : FSMTrigger
{
    public override void Init() {
        TriggerID = FSMTriggerID.EndAction;
    }

    public override bool IsTrigger(FSMData data) {
        return true;
    }

}
