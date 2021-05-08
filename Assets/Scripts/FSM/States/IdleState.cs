using UnityEngine;

public class IdleState : FSMState {

    public override void Init() {
        StateID = FSMStateID.Idle;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter IdleState");
        // 空闲动画
        data.SetIdle();
    }
}
