using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GamePanel : MonoBehaviour
{
    public static bool isMapBattleOpen = false;

    [SerializeField] private Text curTurns = default;
    [SerializeField] private Button standbyBtn = default;
    [SerializeField] private Button switchBattleBtn = default;
    [SerializeField] private Text switchText = default;
    [SerializeField] private Button saveBtn = default;
    [SerializeField] private Button skillBtn = default;

    private int turnCount = 0;
    private bool isClickSkill = false;
    private int[][] skillRange;
    private int rangeIndex = -1;
    private int previousRangeIndex = -1;
    private int curShowRangeIndex = -1;

    private void Start() {
        PickUpController.Instance.click -= OnClick;
        PickUpController.Instance.click += OnClick;
        TurnController.turnCountUp -= ChangeTurns;
        TurnController.turnCountUp += ChangeTurns;
        TurnController.showSaveBtn -= ShowSaveBtn;
        TurnController.showSaveBtn += ShowSaveBtn;
        TurnController.hideSaveBtn -= HideSaveBtn;
        TurnController.hideSaveBtn += HideSaveBtn;
        ChangeTurns();
        switchBattleBtn.onClick.AddListener(OnSwitchClick);
        saveBtn.onClick.AddListener(OnSave);
        skillBtn.onClick.AddListener(ClickSkill);
        standbyBtn.transform.DOScale(Vector3.one * 1.5f, 2f).SetLoops(-1);
    }

    private void Update() {
        if (isClickSkill) {
            LogicTile tile = PickUpController.Instance.GetTile();
            if (tile != null && PickUpController.Instance.CurMapUnit != null && PickUpController.Instance.CurMapUnit.Team == TeamType.My) {
                LogicTile curTile = PickUpController.Instance.CurMapUnit.LastStandTile;
                previousRangeIndex = rangeIndex;
                int index = IceSpell.GetRangeIndex(tile.X - curTile.X, tile.Y - curTile.Y);
                rangeIndex = index;
                if (index != -1 && rangeIndex != previousRangeIndex) {
                    GameBoard.instance.ClearUITiles();
                    GameBoard.instance.CreateUITile(skillRange[index], UITileType.ATTACK);
                    curShowRangeIndex = index;
                }
            }
        }
    }

    public static bool IsInRange(int deltaX, int deltaY, int length) {
        if (deltaX != 0 && deltaY != 0 && Mathf.Abs(deltaX) != Mathf.Abs(deltaY)) {
            return false;
        }
        return Mathf.Max(Mathf.Abs(deltaX), Mathf.Abs(deltaY)) <= length;
    }

    private void OnSwitchClick() {
        isMapBattleOpen = !isMapBattleOpen;
        switchText.text = isMapBattleOpen ? "当前：地图战斗" : "当前：实际战斗";
    }

    private void OnSave() {
        GameDataManager.Instance.SaveAll();
    }

    private void ShowSaveBtn() {
        saveBtn.gameObject.SetActive(true);
    }

    private void HideSaveBtn() {
        saveBtn.gameObject.SetActive(false);
    }

    private void OnClick(bool isClickMyUnit) {
        standbyBtn.gameObject.SetActive(isClickMyUnit);
    }

    private void ChangeTurns() {
        turnCount++;
        curTurns.text = "回合数:" + turnCount;
    }

    private void ClickSkill() {
        isClickSkill = !isClickSkill;
        GameBoard.instance.ClearUITiles();
        if (isClickSkill) {
            LogicTile tile = PickUpController.Instance.CurMapUnit.LastStandTile;
            skillRange = SkillController.Instance.GetAllRange(tile.X, tile.Y, IceSpell.range, 1, 3);
        } else {
            SkillController.Instance.CreateIceSpells(skillRange[curShowRangeIndex]);
            OnStandbyButtonClicked();
        }
    }

    public void OnStandbyButtonClicked() {
        if (PickUpController.Instance.Standby()) {
            standbyBtn.gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        switchBattleBtn.onClick.RemoveAllListeners();
        saveBtn.onClick.RemoveAllListeners();
        skillBtn.onClick.RemoveAllListeners();
    }

}
