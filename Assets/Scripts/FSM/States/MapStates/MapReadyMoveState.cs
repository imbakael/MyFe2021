using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReadyMoveState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.MapReadyMove;
    }

}
