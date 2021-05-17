using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{
    [SerializeField] private BattleHealthBar rightHealthBar = default;
    [SerializeField] private Text rightName = default;
    [SerializeField] private Text rightHit = default;
    [SerializeField] private Text rightDmg = default;
    [SerializeField] private Text rightCrit = default;
    [SerializeField] private Image rightWeapon = default;
    [SerializeField] private Text rightWeaponName = default;

    [SerializeField] private BattleHealthBar leftHealthBar = default;
    [SerializeField] private Text leftName = default;
    [SerializeField] private Text leftHit = default;
    [SerializeField] private Text leftDmg = default;
    [SerializeField] private Text leftCrit = default;
    [SerializeField] private Image leftWeapon = default;
    [SerializeField] private Text rleftWeaponName = default;

    public void Init() {
        rightHealthBar.Init(20, 20);
        leftHealthBar.Init(20, 20);
    }

}
