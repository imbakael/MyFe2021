using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyTools
{
    public static void EnqueueAfterCheckNull<T>(this Queue<T> queue, T item) {
        if (item != null) {
            queue.Enqueue(item);
        }
    }

    // 四舍五入
    public static int GetRound(float value) {
        float v = (value + 0.5f) * 10;
        return Mathf.FloorToInt(v) / 10;
    }
}
