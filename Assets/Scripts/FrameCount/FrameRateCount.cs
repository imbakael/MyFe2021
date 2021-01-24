using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateCount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI displayText;
    [SerializeField, Range(0.2f, 2f)]
    private float checkTime = 1f;

    private int frames;
    private float duration;
    private float best = float.MaxValue;
    private float worst = 0f;

    void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameDuration;
        if (frameDuration < best) {
            best = frameDuration;
        }
        if (frameDuration > worst) {
            worst = frameDuration;
        }
        if (duration >= checkTime) {
            displayText.SetText("FPS\n{0:0}\n{1:0}\n{2:0}", 1f / best, frames / duration, 1f / worst);
            frames = 0;
            duration = 0f;
            best = float.MaxValue;
            worst = 0f;
        }
    }
}
