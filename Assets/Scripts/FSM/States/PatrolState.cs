
public class PatrolState : FSMState
{
    private int index = 0;

    public override void Init() {
        StateID = FSMStateID.Patrol;
    }

    public override void Enter(FSMData data) {
        data.animation.Play("walk");
    }

    public override void Tick(FSMData data) {
        // 循环巡逻 A -> B -> C -> A -> B
        if (data.IsArrive(data.wayPoints[index].position)) {
            index = (index + 1) % data.wayPoints.Length;
        }
        data.MoveToTarget(data.wayPoints[index].position, 0f);
    }

}
