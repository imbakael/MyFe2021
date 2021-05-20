﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Role {

    public Action<float> onHpChange; // 大地图血条进度
    public Action<int> onDamge; // 实际战斗血条变化

    // 基本信息
    public TeamType Team;
    public string Name;
    // 职业id，我方 - 0：剑圣，1：盗贼，2：战士，3：枪兵（我方），4：圣骑士
    // 敌人 - 10：山贼，11：重甲，12：枪兵，13：将军
    public int ClassId; 
    public string ClassName;
    public int Lv = 1;
    public int Exp; // 经验值取值范围0-99，到达100时升级，到达等级上限时重置为0
    public int Hp;
    public int MaxHp;
    public int Mp;
    public int MaxMp;
    public int Durability { get; set; }

    // 基础属性（可成长属性）
    public int Str; // 力量
    public int Def; // 守备
    public int Mag; // 魔力
    public int Res; // 魔防
    public int Ski; // 技巧
    public int Spd; // 速度
    public int Luck; // 幸运
    public int Con; // 体格
    public int Move; // 移动力

    // 战斗属性
    public int Attack {
        get {
            return Str;
        }
    }

    public int Defence {
        get {
            return Def;
        }
    }

    public float Crit {
        get {
            return Ski / 100f;
        }
    }

    public float CritAvoid {
        get {
            return Luck / 100f;
        }
    }

    public float CritTimes {
        get {
            return 3f;
        }
    }

    // 基础抗性
    public float PhysicsResist;
    public float LightResist;
    public float DarkResist;
    public float FireResist;
    public float ThunderResist;
    public float WindResist;

    // 装备信息

    
    public Role(TeamType team, int classId, int hp, int str, int def, int ski, int luck, int durability, int spd) {
        Team = team;
        ClassId = classId;
        ClassName = GetNameByClassId(classId);
        Hp = MaxHp = hp;
        Str = str;
        Def = def;
        Ski = ski;
        Luck = luck;
        Durability = durability;
        Spd = spd;
    }

    private string GetNameByClassId(int classId) {
        Dictionary<int, string> names = new Dictionary<int, string> {
            { 0, "剑圣" },
            { 1, "盗贼" },
            { 2, "战士" },
            { 3, "枪兵" },
            { 4, "圣骑士" },

            { 10, "山贼" },
            { 11, "重甲" },
            { 12, "枪兵" },
            { 13, "将军" },
        };
        return names[classId];
    }

    public void ChangeValue(string s) {
        string[] name_value = s.Split(',');
        int previousHp = Hp;
        ChangeValue(name_value[0], name_value[1]);
        int lastHp = Hp;
        if (lastHp != previousHp) {
            onHpChange?.Invoke(Hp * 1f / MaxHp);
            onDamge?.Invoke(lastHp - previousHp);
        }
    }

    private void ChangeValue(string name, string value) {
        switch (name) {
            case "Hp":
                Hp += Convert.ToInt32(value);
                break;
            case "Durability":
                Durability += Convert.ToInt32(value);
                break;
            default:
                Debug.LogError("没找到对应属性！ s = " + name);
                break;
        }
    }

    public bool AddExp(int delta) {
        Exp += delta;
        if (Exp >= 100) {
            Exp = Exp - 100;
            // 升级
            return true;
        }
        return false;
    }

}
