using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RealBattleUnit : MonoBehaviour
{
    public RealBattleUnit Target { get; set; } // 战斗时的对手
    public abstract IEnumerator AttackTo(); // 对目标进行攻击or治疗
    public abstract void TakeDamage(int damage);
    public abstract void Die();

}
