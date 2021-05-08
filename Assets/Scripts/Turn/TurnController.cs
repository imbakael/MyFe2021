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
        List<MapUnit> ally = GameBoard.instance.GetTeam(TeamType.ALLY);
        List<MapUnit> enemy = GameBoard.instance.GetTeam(TeamType.ENEMY);
        List<MapUnit> neutral = GameBoard.instance.GetTeam(TeamType.NEUTRAL);
        List<List<MapUnit>> allUnits = new List<List<MapUnit>> {
            ally, enemy, neutral
        };
        for (int i = 0; i < allUnits.Count; i++) {
            List<MapUnit> units = allUnits[i];
            if (units.Count == 0) {
                continue;
            }
            // 本阵营回合动画
            bool isTurnTransOver = false;
            UIManager.Instance.CreateTurnTransPanel(teams[i], () => isTurnTransOver = true);
            while (!isTurnTransOver) {
                yield return null;
            }

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
        EnterPlayerTurn();
    }

    private void EnterPlayerTurn() {
        // 玩家回合开始动画

        // 隐藏遮罩
        
    }
}
