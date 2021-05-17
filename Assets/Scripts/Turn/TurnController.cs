using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 回合控制器, 优先行动顺序：我方 -> 友军 -> 敌人 -> 中立
public class TurnController : MonoBehaviour
{
    public static event Action turnUp;
    public static bool isMyTurn = true;

    private readonly List<TeamType> teams = new List<TeamType> { TeamType.ALLIANCE, TeamType.ENEMY, TeamType.NEUTRAL };

    private IEnumerator Start() {
        while (true) {
            while (isMyTurn) {
                yield return null;
            }
            yield return OtherTurn();
            turnUp?.Invoke();
        }
    }

    private IEnumerator OtherTurn() {
        UIManager.Instance.ShowMask();
        GameBoard.instance.NextTurn(TeamType.My);
        List<List<MapUnit>> allUnits = GetAllUnits();
        for (int i = 0; i < allUnits.Count; i++) {
            List<MapUnit> units = allUnits[i];
            if (units.All(t => t.IsDead)) {
                continue;
            }
            LevelManager.Instance.CurTeam = units[0].Team;
            yield return WaitTurnTrans(teams[i]);
            yield return WaitByOneKindOfTeam(units);
        }
        LevelManager.Instance.CurTeam = TeamType.My;
        yield return WaitTurnTrans(TeamType.My);
        UIManager.Instance.HideMask();
        isMyTurn = true;
    }

    private List<List<MapUnit>> GetAllUnits() {
        var result = new List<List<MapUnit>>();
        for (int i = 0; i < teams.Count; i++) {
            result.Add(GameBoard.instance.GetTeam(teams[i]));
        }
        return result;
    }

    private IEnumerator WaitTurnTrans(TeamType team) {
        bool isTurnTransOver = false;
        UIManager.Instance.CreateTurnTransPanel(team, () => isTurnTransOver = true);
        while (!isTurnTransOver) {
            yield return null;
        }
    }

    private IEnumerator WaitByOneKindOfTeam(List<MapUnit> units) {
        foreach (MapUnit item in units) {
            if (item.IsDead) {
                continue;
            }
            FSMBase fsm = item.GetComponent<FSMBase>();
            fsm.TurnUpdate();
            // 如果仍为空闲状态不管
            // 如果为移动状态，等待移动结束
            // 如果为攻击状态，等待攻击结束
            if (fsm.GetCurrentStateID() == FSMStateID.Pursuit || fsm.GetCurrentStateID() == FSMStateID.Attack) {
                while (fsm.GetCurrentStateID() != FSMStateID.Standby) {
                    if (fsm.GetCurrentStateID() == FSMStateID.Dead) {
                        break;
                    }
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return new WaitForSeconds(1f);
        foreach (MapUnit item in units) {
            FSMBase fsm = item.GetComponent<FSMBase>();
            if (fsm.GetCurrentStateID() != FSMStateID.Dead) {
                fsm.SetIdleState();
            }
        }
    }

}
