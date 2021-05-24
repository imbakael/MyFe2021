using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetailPanel : MonoBehaviour
{
    [SerializeField] private Button closeBtn = default;
    [SerializeField] private Image head = default;
    [SerializeField] private Text name = default;
    [SerializeField] private Text className = default;
    [SerializeField] private Text lv = default;
    [SerializeField] private Text exp = default;
    [SerializeField] private Text hp = default;
    // 属性
    [SerializeField] private Text str = default;
    [SerializeField] private Text mag = default;
    [SerializeField] private Text def = default;
    [SerializeField] private Text res = default;
    [SerializeField] private Text ski = default;
    [SerializeField] private Text con = default;
    [SerializeField] private Text spd = default;
    [SerializeField] private Text luck = default;
    [SerializeField] private Text move = default;

    [SerializeField] private Sprite[] headSprites = default;

    private Role role;

    private void Awake() {
        closeBtn.onClick.AddListener(OnCloseBtnClick);
    }

    private void OnCloseBtnClick() {
        Destroy(gameObject);
    }

    public void Init(MapUnit unit) {
        role = unit.Role;
        head.sprite = GetHeadSprite(unit.classId);
        name.text = GetName(unit.classId);
        className.text = role.ClassName;
        lv.text = role.Lv.ToString();
        exp.text = role.Exp.ToString();
        hp.text = role.Hp + "/" + role.MaxHp;
        str.text = role.Str.ToString();
        mag.text = role.Mag.ToString();
        def.text = role.Def.ToString();
        res.text = role.Res.ToString();
        ski.text = role.Ski.ToString();
        con.text = role.Con.ToString();
        spd.text = role.Spd.ToString();
        luck.text = role.Luck.ToString();
        move.text = unit.GetMovePower.ToString();
        if (role.ClassId == 0) {
            transform.FindChildByName("Sword").gameObject.SetActive(true);
        }
    }

    private Sprite GetHeadSprite(int classId) {
        Dictionary<int, int> dic = new Dictionary<int, int> {
            { 0, 0 },{ 1, 1 },{ 2, 2 },{ 3, 3 },{ 4, 4 },{ 10, 5 },{ 11, 6 },{ 12, 3 },{ 13, 7 }
        };
        return headSprites[dic[classId]];
    }

    private string GetName(int classId) {
        Dictionary<int, string> dic = new Dictionary<int, string> {
            { 0, "艾瑞科" },{ 1, "玛丽卡" },{ 2, "萨卡" },{ 3, "迪斯" },{ 4, "赛特" }
        };
        if (dic.TryGetValue(classId, out string value)) {
            return value;
        }
        return role.ClassName;
    }

    private void OnDestroy() {
        closeBtn.onClick.RemoveAllListeners();
    }
}
