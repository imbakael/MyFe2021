
public class NoHealthTrigger : FSMTrigger
{
    public override void Init() {
        TriggerID = FSMTriggerID.NoHealth;
    }

    public override bool IsTrigger(FSMData data) {
        return data.NoHealth();
    }
}
