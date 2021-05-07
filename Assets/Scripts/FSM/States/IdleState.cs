
public class IdleState : FSMState {

    public override void Init() {
        StateID = FSMStateID.Idle;
    }

    public override void Enter(FSMData data) {
        // 空闲动画
        data.SetIdle();
    }

    public override void Exit(FSMData data) {
        
    }
}
