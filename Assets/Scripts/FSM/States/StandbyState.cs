using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandbyState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.Standby;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter StandbyState");
        data.Standby();
    }

}
