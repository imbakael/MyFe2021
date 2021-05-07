using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回合控制器
public class TurnController : MonoBehaviour
{
    // 优先行动顺序：我方 -> 友军 -> 敌人 -> 中立

    // 玩家回合结束后通知控制器开始运行，添加遮罩
    private List<MapUnit> ally;
    private List<MapUnit> enemy;
    private List<MapUnit> neutral;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            Run();
        }
    }

    public void Run() {
        StartCoroutine(Handle());
    }

    private IEnumerator Handle() {
        enemy = GameBoard.instance.GetTeam(TeamType.ENEMY);
        // 敌方回合动画
        for (int i = 0; i < enemy.Count; i++) {
            MapUnit unit = enemy[i];
            unit.GetComponent<FSMBase>().TurnUpdate();
            if (unit.GetMapState() != MapState.IDLE) {
                while (unit.GetMapState() != MapState.GRAY) {
                    yield return null;
                }
            }
        }
        for (int i = 0; i < enemy.Count; i++) {
            enemy[i].GetComponent<FSMBase>().SetIdleState();
        }
        
    }
}
