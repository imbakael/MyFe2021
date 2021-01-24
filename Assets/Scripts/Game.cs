using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameBoard board = default;
    [SerializeField]
    private Unit myRole = default;
    [SerializeField]
    private Unit enemy = default;

    private void Awake() {
        board.InitPlayers(new int[] { 100 });
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            if (Stage.isTouchOnUI) {
                return;
            }

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
            LogicTile tile = board.GetLogicTile(worldPoint);
            if (tile != null) {
                board.ClickOneTile(tile);
                board.SetColor(tile);
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
