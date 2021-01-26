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
            new Vector2Int(10, 5)
        });
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
            LogicTile tile = board.GetLogicTile(worldPoint);
            if (tile != null) {
                board.ClickOneTile(tile);
                //board.SetColor(tile);
            }
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            board.Standby();
        } else if (Input.GetKeyDown(KeyCode.K)) {
            board.Cancel();
        } else if (Input.GetKeyDown(KeyCode.L)) {
            board.NextTurn();
        } else if (Input.GetKeyDown(KeyCode.Z)) {
            BattleManager.Instance.StartBattle(myRole, enemy);
        }
    }
}
