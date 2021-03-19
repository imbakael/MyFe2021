
public abstract class FSMTrigger {
    public FSMTriggerID TriggerID { get; set; }

    public FSMTrigger() {
        Init();
    }

    public abstract void Init();

    public abstract bool IsTrigger(FSMData data);
}
