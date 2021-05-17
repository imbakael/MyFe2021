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

    public static List<T> Clone<T>(List<T> target) {
        List<T> temp = new List<T>();
        temp.AddRange(target);
        return temp;
    }

    public static Transform FindChildByName(this Transform parent, string name) {
        var queue = new Queue<Transform>();
        queue.Enqueue(parent);
        while (queue.Count != 0) {
            Transform item = queue.Dequeue();
            for (int i = 0; i < item.childCount; i++) {
                Transform child = item.GetChild(i);
                if (child.name == name) {
                    return child;
                } else {
                    queue.Enqueue(child);
                }
            }
        }
        return null;
    }

    public static Vector3 SetX(this Vector3 vector, float x) {
        return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 SetY(this Vector3 vector, float y) {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 SetZ(this Vector3 vector, float z) {
        return new Vector3(vector.x, vector.y, z);
    }
}
