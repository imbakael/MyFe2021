using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMData
{
    private FSMBase fsm;
    private NPCMapUnit npc;

    public FSMData(FSMBase fsm) {
        this.fsm = fsm;
        npc = fsm.GetComponent<NPCMapUnit>();
    }

    public bool NoHealth() => npc.IsDead;

    public bool FindTarget() => npc.GetNearestUnitInView() != null;

    public bool LoseTarget() => !FindTarget();

    public bool IsInAttackRange() => npc.GetNearestUnitInAttackRange() != null;

    public bool IsOutAttackRange() => !IsInAttackRange();

    public void Dead() {
        fsm.enabled = false;
    }

    public void Attack() => npc.Attack();

    public void SetIdle() => npc.SetIdle();

    public void Move() => npc.MoveToNearestTile();
}
