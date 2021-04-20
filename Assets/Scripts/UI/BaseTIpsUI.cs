using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseTipsUI : MonoBehaviour
{
    [SerializeField] private Text name = default;
    [SerializeField] private Text content = default;

    private Vector2 showPos;

    public void Init() {
        // 位置，大小，内容
    }

}
