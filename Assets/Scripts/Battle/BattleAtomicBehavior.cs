using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一次基本战斗行为(动画+产生的结果)
public class BattleAtomicBehavior {

    public BattleAnimationType animationType;

    private string actionStr = string.Empty; // "Hp,3|Durability,-1"
    private Role role;

    public BattleAtomicBehavior(Role role) {
        this.role = role;
    }

    public void AddActionStr(string attr, object value) {
        actionStr += attr + "," + value + "|"; 
    }

    public void Handle() {
        Debug.Log(actionStr);
        actionStr = actionStr.Remove(actionStr.Length - 1);
        string[] strArray = actionStr.Split('|');
        foreach (string item in strArray) {
            role.ChangeValue(item);
        }
    }
}
