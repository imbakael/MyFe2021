using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : Singleton<BattleController>
{
    [SerializeField] private Transform right = default;
    [SerializeField] private Transform left = default;
    [SerializeField] private RealBattleUnit[] allRealUnits = default;

    public Action attackEnd;

    private RealBattleUnit activeUnit;
    private RealBattleUnit passiveUnit;

    public void StartBattle(Role activeRole, Role passiveRole) {
        InitRealBattleUnits(activeRole, passiveRole);
        UIManager.Instance.CreateFightPanel(activeRole, passiveRole);

        activeUnit = GetUnit(activeRole);
        activeUnit.Init(activeRole);
        passiveUnit = GetUnit(passiveRole);
        passiveUnit.Init(passiveRole);

        activeUnit.Target = passiveUnit;
        passiveUnit.Target = activeUnit;

        StartCoroutine(Battle(activeRole, passiveRole));
    }

    private void InitRealBattleUnits(Role activeRole, Role passiveRole) {
        int rightClassId = activeRole.Team == TeamType.My ? activeRole.ClassId : passiveRole.ClassId;
        int leftClassId = activeRole.Team != TeamType.My ? activeRole.ClassId : passiveRole.ClassId;
        RealBattleUnit rightUnit = Instantiate(GetPrefab(rightClassId));
        rightUnit.transform.SetParent(right, false);
        RealBattleUnit leftUnit = Instantiate(GetPrefab(leftClassId));
        leftUnit.transform.SetParent(left, false);
    }

    private RealBattleUnit GetPrefab(int classId) {
        return allRealUnits.Where(t => t.classId == classId).FirstOrDefault();
    }

    private RealBattleUnit GetUnit(Role role) {
        return role.Team == TeamType.My ? right.GetComponentInChildren<RealBattleUnit>() : left.GetComponentInChildren<RealBattleUnit>();
    }

    private IEnumerator Battle(Role activeRole, Role passiveRole) {
        List<BattleTurnData> data = BattleCalculate.GetTurnData(activeRole, passiveRole);
        for (int i = 0; i < data.Count; i++) {
            BattleTurnData item = data[i];
            RealBattleUnit relativeActive = GetRelativeActive(item.ActiveUnit);
            relativeActive.SetData(item);
            yield return relativeActive.AttackTo();
        }
        AudioController.Instance.PlayBgm();
        // 结束战斗，退出战斗界面
        Destroy(right.GetChild(0).gameObject);
        Destroy(left.GetChild(0).gameObject);
        UIManager.Instance.DestroyPanel<FightPanel>();
        attackEnd?.Invoke();
        if (passiveRole.Hp <= 0) {
            MapUnit passiveMapUnit = GameBoard.instance.GetMapUnitByRole(passiveRole);
            passiveMapUnit.Dead();
        }
    }

    private RealBattleUnit GetRelativeActive(BattleUnit battleUnit) {
        return battleUnit.Role == activeUnit.Role ? activeUnit : passiveUnit;
    }

    /* 
        假设我方是攻击者，敌人是防守者
        战斗开始:
        
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

    /*
    // 总战斗
    private IEnumerator Battle() {
        // 战前已经算出双方的总回合数（假设期间双方都没有死），能影响回合数的只有速度差值，所以战斗内一方顶多2回合
        int myTotalTurn = GetBattleTurn(activeUnit);
        int enemyTotalTurn = 1; 
        bool isBattleStart = true;
        while (myTotalTurn > 0 || enemyTotalTurn > 0) {
            if (isBattleStart) {
                isBattleStart = false;
                // 等待主动方选择释放战斗技
            }
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

    // 假设对面无法攻击（射程不够或者没有武器），则此值为0
    private int GetBattleTurn(RealBattleUnit unit) {
        return 1;
    }

    // 每次回合战斗
    private IEnumerator TurnBattle(RealBattleUnit unit) {
        int attackCount = GetAttackTimes(unit); 
        bool isTurnStart = true;
        while (attackCount > 0) {
            if (isTurnStart) {
                // 每回合第一次攻击时的才有可能触发流星剑等天赋，产生增加攻击力、攻击次数等效果
                isTurnStart = false;
            }
            yield return unit.AttackTo();
            // 每次攻击命中后会算攻击可能产生的效果，如果某次命中后有一方死亡则结束战斗

            attackCount--;
        }
    }

    // 根据角色天赋、武器算出单回合的攻击次数，but可能会受对方如见切等天赋的影响，所以还需要看对方的天赋
    private int GetAttackTimes(RealBattleUnit unit) {
        return 1;
    }
    */
}
