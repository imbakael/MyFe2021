using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovingState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.MapMoving;
    }

}
