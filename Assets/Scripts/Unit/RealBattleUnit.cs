using System.Collections;
using UnityEngine;

public abstract class RealBattleUnit : MonoBehaviour
{
    public RealBattleUnit Target { get; set; } // 战斗时的对手
    public BattleTurnData CurData { get; private set; }
    public Role Role { get; private set; }

    public int classId;

    protected Animator animator;
    protected bool isAttackOver;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Init(Role role) {
        Role = role;
    }

    public abstract IEnumerator AttackTo(); // 对目标进行攻击or治疗

    public abstract void Die();

    protected abstract void Attack(); // 动画event
     
    protected abstract void AttackOver(); // 动画event

    public void SetData(BattleTurnData data) {
        CurData = data;
    }

}
