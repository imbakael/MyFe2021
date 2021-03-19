
public class PursuitState : FSMState {

    public override void Init() {
        StateID = FSMStateID.Pursuit;
    }

    public override void Enter(FSMData data) {
        data.animation.Play("Run");
    }

    public override void Tick(FSMData data) {
        data.PursuitPlayer();
    }

    public override void Exit(FSMData data) {
        
    }
}
