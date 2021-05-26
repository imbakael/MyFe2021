using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    public static int[][] range = new int[8][] {
        new int[] {0, 1},
        new int[] {1, 1},
        new int[] {1, 0},
        new int[] {1, -1},
        new int[] {0, -1},
        new int[] {-1, -1},
        new int[] {-1, 0},
        new int[] {-1, 1},
    };

    public LogicTile Tile { get; private set; }

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Init(LogicTile tile) {
        Tile = tile;
        tile.HasObstacle = true;
    }

    public void DestroyMyself() {
        Tile.HasObstacle = false;
        animator.SetTrigger("Destroy");
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy() {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public static int GetRangeIndex(int deltaX, int deltaY) {
        if (deltaX == 0 && deltaY == 0) {
            return -1;
        }
        int divisor = Mathf.Max(Mathf.Abs(deltaX), Mathf.Abs(deltaY));
        if (divisor > 3) {
            return -1;
        }
        int x = deltaX / divisor;
        int y = deltaY / divisor;
        if (x == 0 && deltaX != 0) {
            return -1;
        }
        if (y == 0 && deltaY != 0) {
            return -1;
        }
        for (int i = 0; i < range.Length; i++) {
            int[] item = range[i];
            if (item[0] == x && item[1] == y) {
                return i;
            }
        }
        return -1;
    }
}
