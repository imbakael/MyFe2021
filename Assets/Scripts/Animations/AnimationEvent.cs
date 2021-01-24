using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private void Attack() {
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = 1;
        GetComponent<SpriteRenderer>().color = temp;
        Debug.Log("攻击");
    }

    private void Move() {
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = 0;
        GetComponent<SpriteRenderer>().color = temp;
        //transform.position += Vector3.left * 5;
    }
}
