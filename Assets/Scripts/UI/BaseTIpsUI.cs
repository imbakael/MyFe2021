using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseTipsUI : MonoBehaviour
{
    [SerializeField] private Text content = default;

    private Vector2 showPos;

    public void Init(string name) {
        content.text = Data.GetDetail(name);
    }


}
