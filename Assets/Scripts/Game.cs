using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

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

        board.InitPlayers(new Vector2Int[] {
            new Vector2Int(10, 5),
            new Vector2Int(1, 6)
        });
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            board.NextTurn();
        } else if (Input.GetKeyDown(KeyCode.Z)) {
            BattleManager.Instance.StartBattle(myRole, enemy);
        }
    }
}
