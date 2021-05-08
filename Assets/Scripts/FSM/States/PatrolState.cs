
using UnityEngine;

public class PatrolState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.Patrol;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter PatrolState");
    }

}
