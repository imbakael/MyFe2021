
public class AttackState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.Attack;
    }

    public override void Enter(FSMData data) {
        data.Attack();
    }

    public override void Exit(FSMData data) {

    }
}
