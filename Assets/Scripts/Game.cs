﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameBoard board = default;
    [SerializeField]
    private FightUnit myRole = default;
    [SerializeField]
    private FightUnit enemy = default;

    private void Awake() {
        // 加载各个势力的所有角色阵容
        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(11, 2),
        }, TeamType.ENEMY);

        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(1, 0),
        }, TeamType.MY_ARMY);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            BattleController.Instance.StartBattle(myRole, enemy);
        }
    }
}
