
using UnityEngine;

public class AttackState : FSMState
{
    private FSMData data;

    public override void Init() {
        StateID = FSMStateID.Attack;
    }

    public override void Enter(FSMData data) {
        this.data = data;
        Debug.Log("Enter AttackState");
        data.Attack();
        MapBattleController.Instance.attackEnd += AttackEnd; 
    }

    private void AttackEnd() {
        Debug.LogError("npc 攻击结束");
        data.TurnUpdate();
    }

    public override void Exit(FSMData data) {
        MapBattleController.Instance.attackEnd -= AttackEnd;
    }
}
