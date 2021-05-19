using System.Collections.Generic;
using UnityEngine;

public static class BattleCalculate {

    public static List<BattleTurnData> GetTurnData(Role active, Role passive) {
        BattleUnit activeBattleUnit = new BattleUnit(active);
        BattleUnit passiveBattleUnit = new BattleUnit(passive);
        return GetTurnData(activeBattleUnit, passiveBattleUnit);
    }

    private static List<BattleTurnData> GetTurnData(BattleUnit active, BattleUnit passive) {
        var result = new List<BattleTurnData>();
        // 攻击
        BattleTurnData turnData = new BattleTurnData(active, passive);
        result.Add(turnData);
        if (turnData.IsOver) {
            return result;
        }
        // 反击
        turnData = new BattleTurnData(passive, active);
        result.Add(turnData);
        if (turnData.IsOver) {
            return result;
        }
        // 追击
        BattleUnit relativeActive =
            active.Speed - passive.Speed >= 4 ? active :
            passive.Speed - active.Speed >= 4 ? passive :
            null;
        if (relativeActive != null) {
            BattleUnit relativePassive = relativeActive == active ? passive : active;
            turnData = new BattleTurnData(relativeActive, relativePassive);
            result.Add(turnData);
        }
        return result;
    }
}
