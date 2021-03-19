using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class FSMData
{
    public Transform[] wayPoints;
    public Animation animation;

    private FSMBase fsm;
    private NavMeshAgent agent;

    public FSMData(FSMBase fsm) {
        this.fsm = fsm;

        animation = fsm.GetComponentInChildren<Animation>();
        agent = fsm.GetComponent<NavMeshAgent>();
        wayPoints = fsm.wayPoints;
    }

    public bool NoHealth() => false;

    public bool FindTarget() {
        if (IsTargetDead()) {
            return false;
        }
        return IsArrive(Vector3.zero, 1);
    }

    public bool IsTargetDead() => false;

    public bool IsArrive(Vector3 target, float distance = 0.5f) => Vector3.Distance(fsm.transform.position, target) <= distance;

    public bool LoseTarget() => !FindTarget();

    public void MoveToTarget(Vector3 target, float stopDistance) {
        agent.SetDestination(target);
        agent.stoppingDistance = stopDistance;
    }

    public void PursuitPlayer() {
        
    }
    
    public bool IsInAttackRange() {
        if (IsTargetDead()) {
            return false;
        }
        return IsArrive(Vector3.zero, 1);
    }

    public bool IsOutAttackRange() => !IsInAttackRange();

    public void Dead() {
        agent.isStopped = true;
        fsm.enabled = false;
    }

    public void Attack() {
        
    }
}
