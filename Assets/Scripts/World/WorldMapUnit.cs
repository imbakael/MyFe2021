using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapUnit : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void MoveTo(Vector2 target) {
        StopAllCoroutines();
        StartCoroutine(Move(target));
    }

    private IEnumerator Move(Vector2 target) {
        Vector2Int direction = GetDirection(target);
        SetAnimation(direction.x, direction.y);
        while (Vector2.Distance(transform.position, target) > 0.01f) {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        SetAnimation(0, 0);
    }

    private Vector2Int GetDirection(Vector2 target) {
        Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        if (Vector2.Angle(direction, Vector2.right) <= 45) {
            return Vector2Int.right;
        }
        if (Vector2.Angle(direction, Vector2.up) <= 45) {
            return Vector2Int.up;
        }
        if (Vector2.Angle(direction, Vector2.left) <= 45) {
            return Vector2Int.left;
        }
        if (Vector2.Angle(direction, Vector2.down) <= 45) {
            return Vector2Int.down;
        }
        return Vector2Int.zero;
    }

    private void SetAnimation(int x, int y) {
        animator.SetInteger("X", x);
        animator.SetInteger("Y", y);
    }
}
