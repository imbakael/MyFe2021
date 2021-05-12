using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回合控制器, 优先行动顺序：我方 -> 友军 -> 敌人 -> 中立
public class TurnController : MonoBehaviour
{
    private readonly List<TeamType> teams = new List<TeamType> { TeamType.ALLIANCE, TeamType.ENEMY, TeamType.NEUTRAL };

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            Run();
        }
    }

    public void Run() {
        StartCoroutine(StartTurn());
    }

    private IEnumerator StartTurn() {
        UIManager.Instance.ShowMask();
        List<List<MapUnit>> allUnits = GetAllUnits();
        for (int i = 0; i < allUnits.Count; i++) {
            List<MapUnit> units = allUnits[i];
            if (units.Count == 0) {
                continue;
            }
            yield return WaitTurnTrans(teams[i]);
            foreach (var item in units) {
                FSMBase fsm = item.GetComponent<FSMBase>();
                fsm.TurnUpdate();
                if (fsm.GetCurrentStateID() == FSMStateID.Pursuit || fsm.GetCurrentStateID() == FSMStateID.Attack) {
                    while (fsm.GetCurrentStateID() != FSMStateID.Standby) {
                        yield return null;
                    }
                }
                // 如果仍为Idle状态不管
                // 如果为移动状态，等待移动结束
                // 如果为攻击状态，等待攻击结束

            }
            yield return new WaitForSeconds(1f);
            foreach (var item in units) {
                item.GetComponent<FSMBase>().SetIdleState();
            }
        }

        yield return WaitTurnTrans(TeamType.My);
        UIManager.Instance.HideMask();
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

}
