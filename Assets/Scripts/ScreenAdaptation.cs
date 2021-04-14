using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdaptation : MonoBehaviour
{
    // 公式：相机宽度 / 相机高度 = 屏幕宽高比
    // 相机高度 = 相机size * 2

    public float validWith; // 有效内容宽度是指2340 * 1080分辨率（我的设计分辨率）下且camera size = 5时的相机宽度
    private void Start() {
        validWith = 5 * 2 * 2340f / 1080;
        CalculateAdapation();
    }

    private void CalculateAdapation() {
        float screenWH = Screen.width * 1f / Screen.height;
        float size = GetComponent<Camera>().orthographicSize;
        float cameraWith = size * screenWH * 2;
        if (cameraWith < validWith) {
            GetComponent<Camera>().orthographicSize = validWith / screenWH / 2;
        }
    }
}
