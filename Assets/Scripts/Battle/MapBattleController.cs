using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleController : Singleton<MapBattleController>
{
    public Role ActiveRole {
        get {
            return active.Role;
        }
    }

    public Role PassiveRole {
        get {
            return passive.Role;
        }
    }

    public Action attackEnd;

    private MapUnit active;
    private MapUnit passive;

    public void StartMapBattle(MapUnit active, MapUnit passive) {
        this.active = active;
        this.passive = passive;
        List<BattleTurnData> data = BattleCalculate.GetTurnData(ActiveRole, PassiveRole);
        PlayBattleAnimation(data);
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
