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

    private Role rightRole;
    private Role leftRole;

    // 右侧为我方or友军，左侧为敌人or中立，当同时出现敌人vs中立时，主动方在右侧，被动方在左侧
    public void Init(Role active, Role passive) {
        AudioController.Instance.PlayBattle();
        rightRole = active.Team == TeamType.My ? active : passive;
        leftRole = active == rightRole ? passive : active;

        rightRole.onDamge += rightHealthBar.ChangeHp;
        leftRole.onDamge += leftHealthBar.ChangeHp;
        rightHealthBar.Init(rightRole.Hp, rightRole.MaxHp);
        leftHealthBar.Init(leftRole.Hp, leftRole.MaxHp);
        rightName.text = rightRole.ClassName;
        rightHit.text = "100";
        rightDmg.text = (rightRole.Attack - leftRole.Defence).ToString();
        rightCrit.text = GetCrit(rightRole.Crit - leftRole.CritAvoid).ToString();

        leftName.text = leftRole.ClassName;
        leftHit.text = "100";
        leftDmg.text = (leftRole.Attack - rightRole.Defence).ToString();
        leftCrit.text = GetCrit(leftRole.Crit - rightRole.CritAvoid).ToString();
    }

    private int GetCrit(float factCrit) {
        float showCrit = factCrit * 100;
        showCrit = Mathf.Clamp(showCrit, 0f, 100f);
        return MyTools.GetRound(showCrit);
    }

    private void OnDestroy() {
        AudioController.Instance.PlayBgm();
        rightRole.onDamge -= rightHealthBar.ChangeHp;
        leftRole.onDamge -= leftHealthBar.ChangeHp;
    }

}
