using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : Unit {

    // 空闲，普攻，必杀，躲避，受伤，死亡
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public override IEnumerator AttackTo() {
        FightState = FighterState.ATTACK;
        // 计算命中，必杀，天赋，可以暂时先不考虑，直接100%命中的普攻
        animator.SetTrigger("Attack"); // 这里也有可能播放必杀动画
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        Target.GetComponent<SpriteRenderer>().sortingOrder = 0;

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            yield return null;
        }
        // 攻击结束
        FightState = FighterState.IDLE;
    }

    public override void Die() {

    }

    public override void TakeDamage(int damage) {
        Attr.CurHp -= damage;
    }

    private void Attack() {
        Debug.Log("攻击帧");
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = 1;
        GetComponent<SpriteRenderer>().color = temp;
        //Target.TakeDamage(damage);
        StartCoroutine(Pause(1f));
    }

    private void Move() {
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = 0;
        GetComponent<SpriteRenderer>().color = temp;
        transform.position = Target.transform.position + Vector3.right * 2f;
        StartCoroutine(Pause(2f));
    }

    private void Dodge() {

    }

    private IEnumerator Pause(float time) {
        animator.speed = 0f;
        yield return new WaitForSeconds(time);
        animator.speed = 1f;

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            animator.SetTrigger("Attack");
        }
    }

}
