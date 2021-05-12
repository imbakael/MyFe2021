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
            //new Vector2Int(11, 2),
            ////new Vector2Int(11, 4),
            //new Vector2Int(10, 3),
            //new Vector2Int(12, 3),
            //new Vector2Int(11, 0),
            new Vector2Int(12, 4),

        }, TeamType.ENEMY);

        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(11, 3),
            new Vector2Int(13, 3),
        }, TeamType.My);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            BattleController.Instance.StartBattle(myRole, enemy);
        }
    }
}
