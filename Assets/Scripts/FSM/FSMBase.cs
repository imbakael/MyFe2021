using System.Collections.Generic;
using UnityEngine;

public class FSMBase : MonoBehaviour
{
    [SerializeField]
    private FSMStateID defaultStateID = default;

    public FSMStateID test_currentStateID;
    public Transform[] wayPoints;
    public FSMData fsmData;

    public List<FSMState> states;
    private FSMState defaultState;
    private FSMState currentState;

    private void Start() {
        fsmData = new FSMData(this);
        ConfigFSM();
        InitDefaultState();
    }

    public virtual void ConfigFSM() {
        states = new List<FSMState>();

        //DeadState dead = new DeadState();
        //states.Add(dead);

        //PatrolState patrol = new PatrolState();
        //patrol.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //patrol.AddMap(FSMTriggerID.FindTarget, FSMStateID.Pursuit);
        //states.Add(patrol);

        //PursuitState pursuit = new PursuitState();
        //pursuit.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //pursuit.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Patrol);
        //pursuit.AddMap(FSMTriggerID.InAttackRange, FSMStateID.Attack);
        //states.Add(pursuit);

        //AttackState attack = new AttackState();
        //attack.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //attack.AddMap(FSMTriggerID.TargetDead, FSMStateID.Patrol);
        //attack.AddMap(FSMTriggerID.OutAttackRange, FSMStateID.Pursuit);
        //states.Add(attack);
    }

    private void InitDefaultState() {
        defaultState = states.Find(t => t.StateID == defaultStateID);
        currentState = defaultState;
        currentState.Enter(fsmData);
    }

    private void Update() {
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
