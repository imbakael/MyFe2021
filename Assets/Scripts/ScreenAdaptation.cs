using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdaptation : MonoBehaviour
{
    // 公式：相机宽度 / 相机高度 = 屏幕宽高比
    // 相机高度 = 相机size * 2

    public float validWith; // 有效内容宽度
    private float designWidth = 1920f;
    private float desingHeight = 1080f;
    private float designSize = 5f;

    private void Start() {
        validWith = designSize * 2 * designWidth / desingHeight;
        CalculateAdapation();
    }

    private void CalculateAdapation() {
        float screenRate = Screen.width * 1f / Screen.height;
        float cameraWith = GetComponent<Camera>().orthographicSize * screenRate * 2;
        if (cameraWith < validWith) {
            GetComponent<Camera>().orthographicSize = validWith / screenRate / 2;
        }
    }
}
