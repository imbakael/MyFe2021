using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    private FightUnit activeUnit; // 主动方
    private FightUnit passiveUnit; // 被动方

    public void StartBattle(FightUnit active, FightUnit passive) {
        activeUnit = active;
        passiveUnit = passive;

        activeUnit.Target = passive;
        passiveUnit.Target = active;

        /* 
        加载战斗UI，背景图等

        假设我方是攻击者，敌人是防守者，战斗开始
        我方小回合1
            第一次攻击时判断触发连续，攻击次数+1，判定命中、必杀、大盾
            第二次攻击判断命中、必杀、大盾
            第三次攻击同上

        敌方小回合1
            攻击次数1次
            第一次攻击，判断命中、必杀

        我方小回合2
            判定连续，攻击次数+1
            第一次攻击
            第二次攻击
        */

        StartCoroutine(Battle());
    }

    // 全局战斗
    private IEnumerator Battle() {
        // 战前已经算出双方的总回合数（假设期间双方都没有死），能影响回合数的只有速度差值，所以战斗内一方顶多2回合
        int myTotalTurn = 1;
        int enemyTotalTurn = 1; // 假设对面无法攻击（射程不够或者没有武器），则此值为0
        while (myTotalTurn > 0 || enemyTotalTurn > 0) {
            if (myTotalTurn > 0) {
                yield return TurnBattle(activeUnit);
                myTotalTurn--;
            }

            if (enemyTotalTurn > 0) {
                yield return TurnBattle(passiveUnit);
                enemyTotalTurn--;
            }
        }
    }

    /// <summary>
    /// 单回合战斗
    /// </summary>
    /// <param name="first">主动方</param>
    /// <returns></returns>
    private IEnumerator TurnBattle(FightUnit first) {
        int attackCount = 2; // 根据角色天赋、武器算出单回合的攻击次数
        bool isTurnStart = true;
        while (attackCount > 0) {
            if (isTurnStart) {
                // 计算第一次攻击时的才有可能触发天赋，产生增加攻击力、攻击次数等效果
                isTurnStart = false;
            }
            yield return first.AttackTo();
            // 每次攻击命中后会算攻击可能产生的效果，以及每次命中后己方or敌方死亡则结束战斗
            
            attackCount--;
        }
    }
}
