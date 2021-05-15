using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameBoard board = default;
    [SerializeField] private FightUnit myRole = default;
    [SerializeField] private FightUnit enemy = default;

    private void Awake() {
        // 加载各个势力的所有角色阵容
        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(5, 5),
            new Vector2Int(15, 7),
            //new Vector2Int(14, 0),

        }, TeamType.ENEMY);

        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(10, 5),
            //new Vector2Int(1, 0),
        }, TeamType.My);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            BattleController.Instance.StartBattle(myRole, enemy);
        }
    }
}
