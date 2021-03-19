using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : FightUnit
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private SpriteRenderer spearRender = default;

    private Animator animator;
    private bool isAttackOver = true;
    // 控制UI掉血

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // 一定是主要由游戏内在逻辑去驱动动画，而不是由动画驱动逻辑
    // 先走到对方FightUnit前的预设点point【内在逻辑】，期间播放行走动画，然后攻击【内在逻辑】，播放攻击动画；
    // 而不是播放整个行走->攻击动画，增加行走部分的动画关键帧来迎合整个过程，这就属于典型的动画驱动逻辑了！！
    public override IEnumerator AttackTo() {
        // 先走到敌人，同时播放行走动画
        animator.SetTrigger("Walk");
        yield return Move();
        isAttackOver = false;
        animator.SetTrigger("Attack");
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        Target.GetComponent<SpriteRenderer>().sortingOrder = 0;
        while (!isAttackOver) {
            yield return null;
        }
    }

    public override void TakeDamage(int damage) {
        
    }

    public override void Die() {
        Debug.Log("me Die");
    }

    private void Attack() { // 动画event
        Target.TakeDamage(1);
        ShowSpear();
        StartCoroutine(Pause());
    }

    private void AttackOver() { // 动画event
        isAttackOver = true;
        HiddenSpear();
    }

    private void ShowSpear() => spearRender.gameObject.SetActive(true);

    private void HiddenSpear() => spearRender.gameObject.SetActive(false);

    private IEnumerator Move() {
        Vector3 direction = transform.position.x > Target.collidePoint.position.x ? Vector3.left : Vector3.right;
        while (Mathf.Abs(transform.position.x - Target.collidePoint.position.x) > 0.1f) {
            transform.Translate(moveSpeed * Time.deltaTime * direction);
            yield return null;
        }
    }

    private IEnumerator Pause() {
        animator.speed = 0f;
        yield return new WaitForSeconds(1f);
        animator.speed = 1f;
    }

}
