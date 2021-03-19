using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAIFSM : FSMBase
{

    public override void ConfigFSM() {
        base.ConfigFSM();
        var idle = new MapIdleState();
        idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.MapReadyMove);
        states.Add(idle);

        var readyMove = new MapReadyMoveState();
        readyMove.AddMap(FSMTriggerID.NoHealth, FSMStateID.MapMoving);
        readyMove.AddMap(FSMTriggerID.NoHealth, FSMStateID.Idle);
        readyMove.AddMap(FSMTriggerID.NoHealth, FSMStateID.MapGray);
        states.Add(readyMove);

        var moving = new MapMovingState();
        moving.AddMap(FSMTriggerID.NoHealth, FSMStateID.MapMoveEnd);
        states.Add(moving);

        var moveEnd = new MapMoveEndState();
        moving.AddMap(FSMTriggerID.NoHealth, FSMStateID.Idle);
        moveEnd.AddMap(FSMTriggerID.NoHealth, FSMStateID.MapGray);
        states.Add(moveEnd);

        var gray = new MapGrayState();
        gray.AddMap(FSMTriggerID.NoHealth, FSMStateID.Idle);
        states.Add(gray);
    }
}
