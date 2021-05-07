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
    
}
