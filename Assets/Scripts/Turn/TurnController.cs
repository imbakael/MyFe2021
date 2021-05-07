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

    public void Run() {
        StartCoroutine(Handle());
    }

    private IEnumerator Handle() {
        for (int i = 0; i < ally.Count; i++) {
            MapUnit unit = ally[i];
            unit.GetComponent<FSMBase>().TurnUpdate();
            yield return null;
        }
        for (int i = 0; i < ally.Count; i++) {
            ally[i].GetComponent<FSMBase>().SetIdleState();
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < enemy.Count; i++) {
            MapUnit unit = enemy[i];
            unit.GetComponent<FSMBase>().TurnUpdate();
            yield return null;
        }
        for (int i = 0; i < enemy.Count; i++) {
            enemy[i].GetComponent<FSMBase>().SetIdleState();
        }
    }
}
