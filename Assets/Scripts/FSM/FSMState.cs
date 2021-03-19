using System;
using System.Collections.Generic;

public abstract class FSMState
{
    public FSMStateID StateID { get; set; }

    private Dictionary<FSMTriggerID, FSMStateID> map;
    private List<FSMTrigger> triggers;

    public FSMState() {
        map = new Dictionary<FSMTriggerID, FSMStateID>();
        triggers = new List<FSMTrigger>();
        Init();
    }

    public abstract void Init();
    public virtual void Enter(FSMData data) { }
    public virtual void Tick(FSMData data) { }
    public virtual void Exit(FSMData data) { }

    public void AddMap(FSMTriggerID triggerID, FSMStateID stateID) {
        map[triggerID] = stateID;
        CreateTrigger(triggerID);
    }

    private void CreateTrigger(FSMTriggerID triggerID) {
        Type type = Type.GetType(triggerID + "Trigger");
        FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
        triggers.Add(trigger);
    }

    public void Check(FSMBase fsm) {
        for (int i = 0; i < triggers.Count; i++) {
            if (triggers[i].IsTrigger(fsm.fsmData)) {
                FSMStateID stateID = map[triggers[i].TriggerID];
                fsm.ChangeState(stateID);
                return;
            }
        }
    }
}
