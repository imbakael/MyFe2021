using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private Text level = default;
    [SerializeField] private Text hp = default;
    [SerializeField] private Text str = default;
    [SerializeField] private Text ski = default;
    [SerializeField] private Text spd = default;
    [SerializeField] private Text luck = default;
    [SerializeField] private Text def = default;
    [SerializeField] private Text magicDef = default;
    [SerializeField] private Text con = default;
    [SerializeField] private Text className = default;
    [SerializeField] private Button mask = default;

    public bool isDestroyed = false;

    private List<Text> allText;
    private Role role;

    private void Awake() {
        allText = new List<Text> {
            level, hp, str, ski, spd, luck, def, magicDef, con
        };
    }

    public void Init(Role role) {
        this.role = role;
        className.text = role.ClassName;
        level.text = role.Lv.ToString();
        hp.text = role.MaxHp.ToString();
        str.text = role.Str.ToString();
        ski.text = role.Ski.ToString();
        spd.text = role.Spd.ToString();
        luck.text = role.Luck.ToString();
        def.text = role.Def.ToString();
        magicDef.text = role.Res.ToString();
        con.text = role.Con.ToString();
    }

    public void LevelUp(string value) {
        string[] valueArray = value.Split('|');
        List<int> values = new List<int>();
        for (int i = 0; i < valueArray.Length; i++) {
            values.Add(Convert.ToInt32(valueArray[i]));
        }
        StartCoroutine(Play(values));
    }

    private IEnumerator Play(List<int> values) {
        for (int i = 0; i < values.Count; i++) {
            int item = values[i];
            if (item != 0) {
                LevelUpEffect effect = allText[i].GetComponent<LevelUpEffect>();
                yield return effect.PlayEffect(item);
            }
        }
        mask.onClick.AddListener(() => {
            Destroy(gameObject);
            isDestroyed = true;
        });
    }

}
