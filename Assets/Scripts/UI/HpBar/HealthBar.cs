using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthPoint;
    private Role role;

    private void Start() {
        healthPoint = transform.FindChildByName("HealthPoint").GetComponent<Image>();
        role = GetComponent<MapUnit>().Role;
        role.onHpChange = ChangeHp;
        ChangeHp(role.Hp * 1f / role.MaxHp);
    }

    public void ChangeHp(float curHpRate) {
        healthPoint.fillAmount = curHpRate;
    }
}
