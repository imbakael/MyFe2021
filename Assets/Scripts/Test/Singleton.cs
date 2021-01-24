using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 尽量少用Singleton，只有在必要的时候如声音控制、网络控制等时才用单例
public class Singleton<T> : MonoBehaviour
    where T: MonoBehaviour
{
    private static T instance;
    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();
                if (instance == null) {
                    instance = new GameObject(name: "Instance of " + typeof(T)).AddComponent<T>();
                }
            }
            
            return instance;
        }
    }

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
    }
}
