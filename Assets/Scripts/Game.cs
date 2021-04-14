using System.Collections;
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
            new Vector2Int(10, 5),
            new Vector2Int(1, 6),
            new Vector2Int(12, 3)
        }, TeamType.MY_ARMY);

        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(9, 3)
        }, TeamType.ENEMY);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            BattleManager.Instance.StartBattle(myRole, enemy);
        }
    }
}
