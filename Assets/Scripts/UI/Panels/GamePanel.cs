using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public static bool isMapBattleOpen = false;

    [SerializeField] private Text curTurns = default;
    [SerializeField] private Button standbyBtn = default;
    [SerializeField] private Button switchBattleBtn = default;
    [SerializeField] private Text switchText = default;
    [SerializeField] private Button saveBtn = default;

    private int turnCount = 0;

    private void Start() {
        PickUpController.Instance.click -= OnClick;
        PickUpController.Instance.click += OnClick;
        TurnController.turnUp -= ChangeTurns;
        TurnController.turnUp += ChangeTurns;
        TurnController.showSaveBtn -= ShowSaveBtn;
        TurnController.showSaveBtn += ShowSaveBtn;
        TurnController.hideSaveBtn -= HideSaveBtn;
        TurnController.hideSaveBtn += HideSaveBtn;
        ChangeTurns();
        switchBattleBtn.onClick.AddListener(OnSwitchClick);
        saveBtn.onClick.AddListener(OnSave);
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

    public void OnStandbyButtonClicked() {
        if (PickUpController.Instance.Standby()) {
            standbyBtn.gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        switchBattleBtn.onClick.RemoveAllListeners();
        saveBtn.onClick.RemoveAllListeners();
    }

}
