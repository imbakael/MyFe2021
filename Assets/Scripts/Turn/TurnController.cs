using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回合控制器, 优先行动顺序：我方 -> 友军 -> 敌人 -> 中立
public class TurnController : MonoBehaviour
{
    private readonly List<TeamType> teams = new List<TeamType> { TeamType.ALLY, TeamType.ENEMY, TeamType.NEUTRAL };

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            Run();
        }
    }

    public void Run() {
        StartCoroutine(Handle());
    }

    private IEnumerator Handle() {
        // 显示遮罩
        List<List<MapUnit>> allUnits = GetAllUnits();
        for (int i = 0; i < allUnits.Count; i++) {
            List<MapUnit> units = allUnits[i];
            if (units.Count == 0) {
                continue;
            }
            yield return StartCoroutine(WaitTurnTrans(teams[i]));
            foreach (var item in units) {
                item.GetComponent<FSMBase>().TurnUpdate();
                if (item.GetMapState() != MapState.IDLE) {
                    while (item.GetMapState() != MapState.GRAY) {
                        yield return null;
                    }
                }
            }
            foreach (var item in units) {
                item.GetComponent<FSMBase>().SetIdleState();
            }
        }
        yield return StartCoroutine(WaitTurnTrans(TeamType.MY_ARMY));
        // 隐藏遮罩

    }

    private List<List<MapUnit>> GetAllUnits() {
        List<List<MapUnit>> result = new List<List<MapUnit>>();
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
