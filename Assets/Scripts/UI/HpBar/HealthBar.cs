using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthPoint;
    private Role role;

    private void Start() {
        healthPoint = MyTools.Find(transform, "HealthPoint").GetComponent<Image>();
        role = GetComponent<MapUnit>().Role;
        role.onHpChange = ChangeHp;
    }

    public void ChangeHp(float curHpRate) {
        healthPoint.fillAmount = curHpRate;
    }
}
