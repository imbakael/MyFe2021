using UnityEngine;

public class PursuitState : FSMState {

    private FSMData data;

    public override void Init() {
        StateID = FSMStateID.Pursuit;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter PursuitState");
        // 进行移动操作，移动到目标点附近
        this.data = data;
        data.MoveEnd(() => data.TurnUpdate());
        data.Move();
    }
}
