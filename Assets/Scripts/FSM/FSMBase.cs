using System.Collections.Generic;
using UnityEngine;

public class FSMBase : MonoBehaviour
{
    [SerializeField] private FSMStateID defaultStateID = default;

    public FSMStateID test_currentStateID;
    public FSMData fsmData;

    private List<FSMState> states;
    private FSMState defaultState;
    private FSMState currentState;

    private void Start() {
        fsmData = new FSMData(this);
        ConfigFSM();
        InitDefaultState();
    }

    public virtual void ConfigFSM() {
        states = new List<FSMState>();

        //PatrolState patrol = new PatrolState();
        //patrol.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //patrol.AddMap(FSMTriggerID.FindTarget, FSMStateID.Pursuit);
        //states.Add(patrol);

        IdleState idle = new IdleState();
        idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        idle.AddMap(FSMTriggerID.FindTarget, FSMStateID.Pursuit);
        states.Add(idle);

        PursuitState pursuit = new PursuitState();
        pursuit.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Idle);
        pursuit.AddMap(FSMTriggerID.InAttackRange, FSMStateID.Attack);
        states.Add(pursuit);

        AttackState attack = new AttackState();
        attack.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        attack.AddMap(FSMTriggerID.OutAttackRange, FSMStateID.Pursuit);
        states.Add(attack);

        DeadState dead = new DeadState();
        states.Add(dead);
    }

    private void InitDefaultState() {
        defaultState = states.Find(t => t.StateID == defaultStateID);
        currentState = defaultState;
        currentState.Enter(fsmData);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            currentState.Check(this);
            currentState.Tick(fsmData);
            test_currentStateID = currentState.StateID;
        }
        
    }

    // 每次回合开始调用
    public void MyUpdate() {
        currentState.Check(this);
        currentState.Tick(fsmData);
        test_currentStateID = currentState.StateID;
    }

    public void ChangeState(FSMStateID stateID) {
        FSMState nextState = stateID == FSMStateID.Default ? defaultState : states.Find(t => t.StateID == stateID);
        if (nextState == currentState) {
            return;
        }
        currentState.Exit(fsmData);
        currentState = nextState;
        currentState.Enter(fsmData);
    }
}
