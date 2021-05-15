using System.Collections.Generic;
using UnityEngine;

// 只负责state的初始化和切换
public class FSMBase : MonoBehaviour
{
    [SerializeField] private FSMStateID defaultStateID = default;

    public FSMData FsmData { get; private set; }

    public FSMStateID test_currentStateID;

    private List<FSMState> states;
    private FSMState defaultState;
    private FSMState currentState;

    private void Start() {
        FsmData = new FSMData(this);
        ConfigFSM();
        InitDefaultState();
    }

    public virtual void ConfigFSM() {
        states = new List<FSMState>();

        //PatrolState patrol = new PatrolState();
        //patrol.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //patrol.AddMap(FSMTriggerID.FindTarget, FSMStateID.Pursuit);
        //states.Add(patrol);

        /* AI类型
         * 1.视野内查找可到达的敌人(友军)，然后追击（治疗）
         * 2.沿指定路径移动(如沿着A -> B -> C路径依次经过这三个点)，途中遇到敌人追击，丢失敌人则继续沿指定路径移动
         * 3.到达指定位置，然后做出一定行为（如强盗破坏村庄，盗贼偷取宝物，友军占领城池等），算是第2条的特殊情况
         * 4.[中立生物]查找领地范围内可以到达的敌人，然后追击，没有敌人则归位
         * 
         */
        IdleState idle = new IdleState();
        idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        idle.AddMap(FSMTriggerID.InAttackRange, FSMStateID.Attack);
        idle.AddMap(FSMTriggerID.FindTarget, FSMStateID.Pursuit);
        states.Add(idle);

        PursuitState pursuit = new PursuitState();
        pursuit.AddMap(FSMTriggerID.InAttackRange, FSMStateID.Attack);
        pursuit.AddMap(FSMTriggerID.EndAction, FSMStateID.Standby);
        states.Add(pursuit);

        AttackState attack = new AttackState();
        attack.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        attack.AddMap(FSMTriggerID.EndAction, FSMStateID.Standby);
        states.Add(attack);

        StandbyState standby = new StandbyState();
        states.Add(standby);

        DeadState dead = new DeadState();
        states.Add(dead);
    }

    private void InitDefaultState() {
        defaultState = states.Find(t => t.StateID == defaultStateID);
        currentState = defaultState;
        currentState.Enter(FsmData);
    }
    
    public void TurnUpdate() {
        currentState.Check(this);
    }

    public void SetIdleState() {
        ChangeState(FSMStateID.Idle);
    }

    public void ChangeState(FSMStateID stateID) {
        FSMState nextState = stateID == FSMStateID.Default ? defaultState : states.Find(t => t.StateID == stateID);
        if (nextState == currentState) {
            return;
        }
        currentState.Exit(FsmData);
        currentState = nextState;
        currentState.Enter(FsmData);
        test_currentStateID = currentState.StateID;
    }

    public FSMStateID GetCurrentStateID() {
        return currentState.StateID;
    }
}
