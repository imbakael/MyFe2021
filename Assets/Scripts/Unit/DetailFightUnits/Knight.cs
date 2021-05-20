using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 只做具体战斗相关的事情
public class Knight : RealBattleUnit
{
    [SerializeField] private SpriteRenderer spearRender = default;

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
        base.Attack();
        CurData.HandleResult();
        spearRender.gameObject.SetActive(true);
        StartCoroutine(Pause());
    }

    protected override void AttackOver() {
        isAttackOver = true;
    }

    private void HideSpear() { // 动画event
        spearRender.gameObject.SetActive(false);
    }

    private IEnumerator Pause() {
        animator.speed = 0f;
        yield return new WaitForSeconds(1f);
        animator.speed = 1f;
    }

}
