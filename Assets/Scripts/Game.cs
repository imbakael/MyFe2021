using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameBoard board = default;

    private void Awake() {
        // 加载各个势力的所有角色阵容
        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(10, 5),
            new Vector2Int(13, 3),
            new Vector2Int(15, 1),
            new Vector2Int(10, 0),
            new Vector2Int(7, 3),
            new Vector2Int(15, 7),
            new Vector2Int(16, 4),
            new Vector2Int(11, 9),
        }, 
        TeamType.ENEMY,
        new int[] {
            10, 10, 11, 11, 12, 12, 12, 13
        });

        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(0, 8),
            new Vector2Int(1, 9),
            new Vector2Int(1, 7),
            new Vector2Int(2, 8),
            new Vector2Int(0, 6),
        }, 
        TeamType.My,
        new int[] {
            0, 1, 2, 3, 4
        });

        List<MapUnit> npcUnits = GameBoard.instance.GetTeam(TeamType.ENEMY);
        foreach (var item in npcUnits) {
            ((NPCMapUnit)item).SetViewRange(20);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            BattleController.Instance.StartBattle(GameBoard.instance.GetTeam(TeamType.My)[0].Role, GameBoard.instance.GetTeam(TeamType.ENEMY)[0].Role);
        }
    }
}
