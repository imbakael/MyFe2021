using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleController : Singleton<MapBattleController>
{
    public static void StartMapBattle(MapUnit active, MapUnit passive) {
        // 调整攻守双方的朝向
        // 计算得到战后数据，包含每小回合双方的攻击情况和血量
        /*
         * 数据类型可以是 
         * {
         *     "A,普攻-被闪避,伤害0,自身受到伤害0,buff_1,",
         *     "B,普攻-命中,伤害5",
         *     "B,必杀-命中,伤害15",
         * } 
         */
        // 根据数据播放对应战斗动画，战斗结束

        BattleUnit a = new BattleUnit();
        a.Load(active.role);
        BattleUnit p = new BattleUnit();
        p.Load(passive.role);

        List<BattleTurnData> data = GetTurnData(a, p);
        //Debug.Log("a : hp = " + a.Hp + ", dur = " + a.Durability);
        //Debug.Log("p : hp = " + p.Hp + ", dur = " + p.Durability);
        //Debug.Log("a.role : hp = " + active.role.Hp + ", dur = " + active.role.Durability);
        //Debug.Log("p.role : hp = " + passive.role.Hp + ", dur = " + passive.role.Durability);

        PlayBattleAnimation(data);
    }

    private static void PlayBattleAnimation(List<BattleTurnData> data) {
        foreach (var item in data) {
            // 播放动画

            // 结算结果
            item.HandleResult();
        }
    }

    private static List<BattleTurnData> GetTurnData(BattleUnit active, BattleUnit passive) {
        List<BattleTurnData> data = new List<BattleTurnData>();
        BattleTurnData oneTurnData;
        // 攻击
        oneTurnData = new BattleTurnData(active, passive);
        data.Add(oneTurnData);
        if (oneTurnData.IsOver) {
            return data;
        }
        // 反击
        oneTurnData = new BattleTurnData(passive, active);
        data.Add(oneTurnData);
        if (oneTurnData.IsOver) {
            return data;
        }

        BattleUnit relativeActive =
            active.Speed - passive.Speed >= 4 ? active :
            passive.Speed - active.Speed >= 4 ? passive :
            null;
        if (relativeActive != null) {
            BattleUnit relativePassive = relativeActive == active ? passive : active;
            oneTurnData = new BattleTurnData(relativeActive, relativePassive);
            data.Add(oneTurnData);
        }
        return data;
    }

    
}
