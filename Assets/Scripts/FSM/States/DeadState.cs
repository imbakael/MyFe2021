﻿
using UnityEngine;

public class DeadState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.Dead;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter DeadState");
        data.Dead();
    }

    public override void Exit(FSMData data) {

    }
}
