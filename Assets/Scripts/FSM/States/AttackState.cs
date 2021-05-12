
using UnityEngine;

public class AttackState : FSMState
{
    private FSMData data;

    public override void Init() {
        StateID = FSMStateID.Attack;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter AttackState");
        this.data = data;
        data.Attack();
        MapBattleController.Instance.attackEnd = HandleWhenAttackEnd;
    }

    private void HandleWhenAttackEnd() {
        Debug.LogError("HandleWhenAttackEnd");
        data.TurnUpdate();
    }
}
