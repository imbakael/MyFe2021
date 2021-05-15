using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role {

    public Action<float> onHpChange;

    // 基本信息
    public TeamType Team;
    public int Name;
    public int ClassId; // 职业id，如剑士、战士、刺客，通过id查表即可获得职业名
    public int Lv;
    public int Exp; // 经验值取值范围0-99，到达100时升级，到达等级上限时重置为0
    public int Hp { get; set; }
    public int MaxHp;
    public int Mp;
    public int MaxMp;
    public int Durability { get; set; }

    // 基础属性（可成长属性）
    public int Str { get; set; } // 力量
    public int Def { get; set; } // 守备
    public int Mag { get; set; } // 魔力
    public int Res { get; set; } // 魔防
    public int Ski { get; set; } // 技巧
    public int Spd { get; set; } // 速度
    public int Luck { get; set; } // 幸运
    public int Con { get; set; } // 体格
    public int Move { get; set; } // 移动力

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

    
    public Role(int hp, int str, int def, int ski, int luck, int durability, int spd) {
        Hp = MaxHp = hp;
        Str = str;
        Def = def;
        Ski = ski;
        Luck = luck;
        Durability = durability;
        Spd = spd;
    }

    public void ChangeValue(string s) {
        string[] name_value = s.Split(',');
        ChangeValue(name_value[0], name_value[1]);
        onHpChange?.Invoke(Hp * 1f / MaxHp);
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

}
