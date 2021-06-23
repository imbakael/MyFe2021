using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Role {

    public Action<float> onHpChange; // 大地图血条进度
    public Action<int> onDamge; // 实际战斗血条变化
    public BuffContainer buffContainer;
    // 实时属性 = base + offset
    public Ability baseAbility; // 固定值，包含升级、永久型消耗品带来的增长（白字属性）
    public Ability offsetAbility; // 偏离值，包含被动、buff、武器道具等提供的属性加成(绿字属性)；所以每次固定值变化、武器更换、buff变更等都会重新计算offset

    // 基本信息
    public TeamType Team;
    public string Name;
    // 职业id，我方 - 0：剑圣，1：盗贼，2：战士，3：枪兵（我方），4：圣骑士， 5：魔法师
    // 敌人 - 10：山贼，11：重甲，12：枪兵，13：将军
    public int ClassId; 
    public string ClassName;
    public int Lv = 1;
    public int Exp; // 经验值取值范围0-99，到达100时升级，到达等级上限时重置为0
    public int Hp;
    public int MaxHp;
    // 每个角色没有魔法值的概念，释放的魔法要么有每章次数限制，要么有内置回合cd
    public int Durability { get; set; }

    // 实时属性
    public int Attack { // 物理攻击包含剑、斧、枪、弓；以及一些特殊攻击比如丧失的爪子普攻，恶魔的普攻，地狱三头犬的普攻等
        get { return MyTools.GetRound(baseAbility.str + offsetAbility.str);}
    }
    public int Defence {
        get { return MyTools.GetRound(baseAbility.def + offsetAbility.def); }
    }
    public int MagicAttack { // 魔法攻击包含火、雷、风、光、暗；毒和冰暂时不受任何抗性影响
        get { return MyTools.GetRound(baseAbility.mag + offsetAbility.mag); }
    }
    public int MagicDefence {
        get { return MyTools.GetRound(baseAbility.res + offsetAbility.res); }
    }
    public int Speed { // 攻速 = 总速度 - (武器重量 - 体格)，如果体格 > 武器重量则 = 总速度
        get { return MyTools.GetRound(baseAbility.spd + offsetAbility.spd); }
    }
    public float Crit {
        get { return (baseAbility.ski + offsetAbility.ski) / 100f + offsetAbility.crit; }
    }
    public float CritAvoid {
        get { return (baseAbility.luck + offsetAbility.luck) / 100f + offsetAbility.critAvoid; }
    }
    public float CritTimes {
        get { return baseAbility.critTimes + offsetAbility.critTimes; }
    }

    // 实时抗性
    public float PhysicsResist {
        get { return baseAbility.physicsResist + offsetAbility.physicsResist; }
    }
    public float LightResist {
        get { return baseAbility.lightResist + offsetAbility.lightResist; }
    }
    public float DarkResist {
        get { return baseAbility.darkResist + offsetAbility.darkResist; }
    }
    public float FireResist {
        get { return baseAbility.fireResist + offsetAbility.fireResist; }
    }
    public float ThunderResist {
        get { return baseAbility.thunderResist + offsetAbility.thunderResist; }
    }
    public float WindResist {
        get { return baseAbility.windResist + offsetAbility.windResist; }
    }

    // 装备信息，会修改offset中对应的值

    // 读取json表
    public Role(TeamType team, int classId, int hp, int str, int def, int ski, int luck, int durability, int spd) {
        Team = team;
        Hp = MaxHp = hp;
        baseAbility = new Ability {
            str = str,
            def = def,
            mag = 0,
            res = 0,
            ski = ski,
            spd = spd,
            luck = luck,
            con = 0,
            move = 0,

            physicsResist = 0.5f,
            lightResist = 0.5f,
            darkResist = 0.5f,
            fireResist = 0.5f,
            thunderResist = 0.5f,
            windResist = 0.5f,

            critTimes = 2
        };
        offsetAbility = new Ability();
        ClassId = classId;
        ClassName = GetNameByClassId(classId);
        Durability = durability;
        // 初始化装备、身上被动时 -> 会导致offset属性发生变化
    }

    private string GetNameByClassId(int classId) {
        Dictionary<int, string> names = new Dictionary<int, string> {
            { 0, "剑圣" }, { 1, "盗贼" }, { 2, "战士" }, { 3, "枪兵" }, { 4, "圣骑士" },
            { 10, "山贼" }, { 11, "重甲" }, { 12, "枪兵" }, { 13, "将军" },
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
