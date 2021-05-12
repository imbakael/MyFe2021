﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleController : Singleton<MapBattleController>
{
    public Action attackEnd;

    private MapUnit active;
    private MapUnit passive;

    public void StartMapBattle(MapUnit active, MapUnit passive) {
        this.active = active;
        this.passive = passive;

        BattleUnit activeBattleUnit = new BattleUnit();
        activeBattleUnit.Load(active.role);
        BattleUnit passiveBattleUnit = new BattleUnit();
        passiveBattleUnit.Load(passive.role);

        List<BattleTurnData> data = GetTurnData(activeBattleUnit, passiveBattleUnit);
        //Debug.Log("a : hp = " + a.Hp + ", dur = " + a.Durability);
        //Debug.Log("p : hp = " + p.Hp + ", dur = " + p.Durability);
        //Debug.Log("a.role : hp = " + active.role.Hp + ", dur = " + active.role.Durability);
        //Debug.Log("p.role : hp = " + passive.role.Hp + ", dur = " + passive.role.Durability);

        // 朝着某个方向攻击
        PlayBattleAnimation(data);
    }

    // 计算战斗结果
    private List<BattleTurnData> GetTurnData(BattleUnit active, BattleUnit passive) {
        var result = new List<BattleTurnData>();
        // 攻击
        BattleTurnData oneTurnData = new BattleTurnData(active, passive);
        result.Add(oneTurnData);
        if (oneTurnData.IsOver) {
            return result;
        }
        // 反击
        oneTurnData = new BattleTurnData(passive, active);
        result.Add(oneTurnData);
        if (oneTurnData.IsOver) {
            return result;
        }
        // 追击
        BattleUnit relativeActive =
            active.Speed - passive.Speed >= 4 ? active :
            passive.Speed - active.Speed >= 4 ? passive :
            null;
        if (relativeActive != null) {
            BattleUnit relativePassive = relativeActive == active ? passive : active;
            oneTurnData = new BattleTurnData(relativeActive, relativePassive);
            result.Add(oneTurnData);
        }
        return result;
    }

    private void PlayBattleAnimation(List<BattleTurnData> data) {
        StartCoroutine(Play(data));
    }

    private IEnumerator Play(List<BattleTurnData> data) {
        var direction = new Vector2Int(passive.Tile.X - active.LastStandTile.X, passive.Tile.Y - active.LastStandTile.Y);
        active.SetAnimation(direction.x, direction.y);
        passive.SetAnimation(-direction.x, -direction.y);
        foreach (BattleTurnData turnData in data) {
            yield return new WaitForSeconds(1f);
            // 一次战斗动画
            MapUnit currentActiveMapUnit = GetMapUnit(turnData.ActiveUnit);
            MapUnit currentPassiveMapUnit = GetMapUnit(turnData.PassiveUnit);
            Vector3 originalPos = currentActiveMapUnit.transform.position;
            Vector3 deltaPos = currentPassiveMapUnit.transform.position - originalPos;
            Vector3 nextPos = originalPos + 0.3f * deltaPos;
            while (Vector3.Distance(currentActiveMapUnit.transform.position, nextPos) > 0.01f) {
                currentActiveMapUnit.transform.position = Vector3.MoveTowards(currentActiveMapUnit.transform.position, nextPos, 5 * Time.deltaTime);
                yield return null;
            }
            currentActiveMapUnit.transform.position = nextPos;
            // 处理结果
            turnData.HandleResult();
            while (Vector3.Distance(currentActiveMapUnit.transform.position, originalPos) > 0.01f) {
                currentActiveMapUnit.transform.position = Vector3.MoveTowards(currentActiveMapUnit.transform.position, originalPos, 5 * Time.deltaTime);
                yield return null;
            }
            currentActiveMapUnit.transform.position = originalPos;
        }
        yield return new WaitForSeconds(0.5f);
        passive.SetAnimation(0, 0);
        Debug.LogError("npc 攻击结束");
        attackEnd?.Invoke();
        attackEnd = null;
    }

    private MapUnit GetMapUnit(BattleUnit unit) {
        return unit.Role == active.role ? active : passive;
    }

}