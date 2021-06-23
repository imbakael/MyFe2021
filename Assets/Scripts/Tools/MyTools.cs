using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public static Dictionary<string, float> GetPropertyDic(string s, string key) {
        JsonData data = JsonMapper.ToObject(s);
        string value = data[key].ToString();
        Debug.LogError("value = " + value);
        Dictionary<string, float> result = new Dictionary<string, float>();
        if (value != null) {
            string[] array = value.Split('|');
            for (int i = 0; i < array.Length; i++) {
                string item = array[i];
                string[] inside = item.Split('_');
                string k = inside[0];
                string v = inside[1];
                result[k] = Convert.ToSingle(v);
            }
        }
        return result;
    }

    public static void ChangeFieldValue(object src, string name, float change) {
        float propertyValue = GetFieldValue<float>(src, name);
        src.GetType().GetField(name).SetValue(src, propertyValue + change);
    }

    public static T GetFieldValue<T>(object src, string propName) {
        return (T)src.GetType().GetField(propName).GetValue(src);
    }

}
