using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brigand : RealBattleUnit
{
    public override IEnumerator AttackTo() {
        isAttackOver = false;
        animator.SetTrigger("Attack_");
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        Target.GetComponent<SpriteRenderer>().sortingOrder = 0;
        while (!isAttackOver) {
            yield return null;
        }
    }

    public override void Die() {
        Debug.Log(GetType() + " Die");
    }

    protected override void Attack() {
        CurData.HandleResult();
        StartCoroutine(Pause());
    }

    protected override void AttackOver() {
        isAttackOver = true;
    }

    private IEnumerator Pause() {
        animator.speed = 0f;
        yield return new WaitForSeconds(0.5f);
        animator.speed = 1f;
    }
}
