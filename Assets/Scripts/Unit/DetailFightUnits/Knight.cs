using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : FightUnit
{
    [SerializeField]
    private float moveSpeed = 1f;

    private Animator animator;
    private Coroutine moveCoro;
    // 碰撞盒应该关掉
    private BoxCollider2D box;
    // 停止点, 对方走到这个点后会停止移动，进行普攻or必杀动画
    private Transform stop;
    // 控制UI掉血

    private void Awake() {
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    public override IEnumerator AttackTo() {
        FightState = FighterState.ATTACK;
        animator.SetTrigger("Attack");
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        Target.GetComponent<SpriteRenderer>().sortingOrder = 0;
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            yield return null;
        }
        
        // 移动到目标脸上后攻击
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            yield return null;
        }
        FightState = FighterState.IDLE;
    }

    public override void TakeDamage(int damage) {
        
    }

    public override void Die() {
        Debug.Log("Enemy Die");
    }

    private void Attack() {
        Debug.Log("重甲攻击");
        EndMove();
        StartCoroutine(Pause());
    }

    private void StartMove() {
        float xDistance = Target.transform.position.x - box.bounds.center.x;
        float time = 35f / 60f;
        if (xDistance > 0) {
            moveSpeed = xDistance / time;
            moveCoro = StartCoroutine(Move());
        }
    }

    private void EndMove() {
        if (moveCoro != null) {
            StopCoroutine(moveCoro);
            moveCoro = null;
        }
    }

    private IEnumerator Move() {
        while (true) {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
            yield return null;
        }
    }

    private IEnumerator Pause() {
        animator.speed = 0f;
        yield return new WaitForSeconds(1f);
        animator.speed = 1f;

    }

}
