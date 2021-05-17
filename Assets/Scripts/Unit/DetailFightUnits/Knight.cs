using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 只做具体战斗相关的事情
public class Knight : RealBattleUnit
{
    [SerializeField]
    private SpriteRenderer spearRender = default;

    private Animator animator;
    private bool isAttackOver = true;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public override IEnumerator AttackTo() {
        isAttackOver = false;
        animator.SetTrigger("Attack_");
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        Target.GetComponent<SpriteRenderer>().sortingOrder = 0;
        while (!isAttackOver) {
            yield return null;
        }
    }

    public override void TakeDamage(int damage) {
        // 更新血条UI
    }

    public override void Die() {
        Debug.Log(GetType() + " Die");
    }

    private void Attack() { // 动画event
        Target.TakeDamage(1);
        spearRender.gameObject.SetActive(true);
        StartCoroutine(Pause());
    }

    private void HideSpear() { // 动画event
        spearRender.gameObject.SetActive(false);
    }

    private void AttackOver() { // 动画event
        isAttackOver = true;
    }

    private IEnumerator Pause() {
        animator.speed = 0f;
        yield return new WaitForSeconds(1f);
        animator.speed = 1f;
    }

}
