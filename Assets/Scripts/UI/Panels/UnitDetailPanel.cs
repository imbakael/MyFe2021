using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetailPanel : MonoBehaviour
{
    [SerializeField] private Button closeBtn = default;

    private void Awake() {
        closeBtn.onClick.AddListener(OnCloseBtnClick);
    }

    private void OnCloseBtnClick() {
        Destroy(gameObject);
    }

    public void Init(MapUnit unit) {

        // 创建背包界面
    }

    private void OnDestroy() {
        closeBtn.onClick.RemoveAllListeners();
    }
}
