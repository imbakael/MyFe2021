using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : FightUnit
{
    [SerializeField]
    private float moveSpeed = 1f;

    private Animator animator;
    private Coroutine moveCoro;
    // 停止点, 对方走到这个点后会停止移动，进行普攻or必杀动画
    private Transform stop;
    // 控制UI掉血

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // 一定是主要由游戏内在逻辑去驱动动画，而不是由动画驱动逻辑
    // 先走到对方FightUnit前的预设点point【内在逻辑】，期间播放行走动画，然后攻击【内在逻辑】，播放攻击动画；
    // 而不是播放整个行走->攻击动画，增加行走部分的动画关键帧来迎合整个过程，这就属于典型的动画驱动逻辑了！！
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
        float xDistance = Target.transform.position.x - stop.position.x;
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
