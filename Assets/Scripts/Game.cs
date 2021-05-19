using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameBoard board = default;

    private void Awake() {
        // 加载各个势力的所有角色阵容
        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(6, 5),
            //new Vector2Int(14, 0),
            //new Vector2Int(15, 7),
            new Vector2Int(13, 5),

        }, 
        TeamType.ENEMY,
        new int[] {
            10, 11
        });

        board.CreateMapUnits(new Vector2Int[] {
            new Vector2Int(10, 5),
            new Vector2Int(1, 6),
        }, 
        TeamType.My,
        new int[] {
            0, 1
        });
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            BattleController.Instance.StartBattle(GameBoard.instance.GetTeam(TeamType.My)[0].Role, GameBoard.instance.GetTeam(TeamType.ENEMY)[0].Role);
        }
    }
}
