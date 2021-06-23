using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 战斗中临时数据，在战斗开始前生成
public class BattleUnit {

    public int Hp { get; set; }
    public int MaxHp { get; private set; }
    public int Mp { get; private set; }
    public int MaxMp { get; private set; }
    public int Atk { get; private set; }
    public int MagicAtk { get; private set; }
    public int Def { get; private set; }
    public int MagicDef { get; private set; }
    public int Speed { get; private set; } // 攻速
    public float Crit { get; private set; }
    public float CritAvoid { get; private set; }
    public float CritTimes { get; private set; }
    public int WeaponType { get; private set; }
    public int Durability { get; set; }

    public Role Role { get; private set; }
    // 一些其他参数，比如抗性等
    
    public BattleUnit(Role role) {
        Role = role;
        Hp = role.Hp;
        Atk = role.Attack;
        Def = role.Defence;
        Crit = role.Crit;
        CritAvoid = role.CritAvoid;
        CritTimes = role.CritTimes;
        Durability = role.Durability;
        Speed = role.Speed;
    }
}
