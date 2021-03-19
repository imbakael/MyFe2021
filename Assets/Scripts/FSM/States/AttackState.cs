
public class AttackState : FSMState
{
    public override void Init() {
        StateID = FSMStateID.Attack;
    }

    public override void Enter(FSMData data) {
        data.animation.Play("stand");
    }

    public override void Tick(FSMData data) {
        data.Attack();
    }

}
