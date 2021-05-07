
public class PursuitState : FSMState {

    public override void Init() {
        StateID = FSMStateID.Pursuit;
    }

    public override void Enter(FSMData data) {
        // 进行移动操作，移动到目标点附近
        data.Move();
    }

    public override void Exit(FSMData data) {
        
    }
}
