using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIdleState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.MapIdle;
    }

}
