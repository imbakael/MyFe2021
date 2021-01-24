using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToTestHealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthBar hpBar;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int maxLayers;
    [SerializeField]
    private int onceHurt;

    private void Awake() {
        hpBar.MyStart(maxHp, maxLayers);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            hpBar.TakeDamage(onceHurt);
        }
    }
}
