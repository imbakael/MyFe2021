using System;
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
        activeBattleUnit.Load(active.Role);
        BattleUnit passiveBattleUnit = new BattleUnit();
        passiveBattleUnit.Load(passive.Role);

        List<BattleTurnData> data = GetTurnData(activeBattleUnit, passiveBattleUnit);
        PlayBattleAnimation(data);
        //Debug.Log("a : hp = " + a.Hp + ", dur = " + a.Durability);
        //Debug.Log("p : hp = " + p.Hp + ", dur = " + p.Durability);
        //Debug.Log("a.role : hp = " + active.role.Hp + ", dur = " + active.role.Durability);
        //Debug.Log("p.role : hp = " + passive.role.Hp + ", dur = " + passive.role.Durability);
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
            yield return MoveTo(currentActiveMapUnit.transform, nextPos);
            turnData.HandleResult();
            yield return MoveTo(currentActiveMapUnit.transform, originalPos);
        }
        yield return new WaitForSeconds(1f);
        if (passive.IsDead) {
            passive.Dead();
        } else {
            passive.SetAnimation(0, 0);
        }
        Debug.LogError(active.Team + " 攻击结束");
        // 如果有一方是玩家阵营，且升级，则等待升级界面消失后再结束
        yield return Exp.Calculate(active, passive);
        attackEnd?.Invoke();
        active = null;
        passive = null;
    }

    private MapUnit GetMapUnit(BattleUnit unit) {
        return unit.Role == active.Role ? active : passive;
    }

    private IEnumerator MoveTo(Transform transform, Vector3 target) {
        while (Vector3.Distance(transform.position, target) > 0.01f) {
            transform.position = Vector3.MoveTowards(transform.position, target, 5 * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }

    public bool IsPassive(MapUnit unit) {
        return passive != null && passive == unit;
    }
}
