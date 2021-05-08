
using UnityEngine;

public class AttackState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.Attack;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter AttackState");
        data.Attack();
    }
}
