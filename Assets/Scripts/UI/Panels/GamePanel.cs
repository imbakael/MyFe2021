using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private Text curTurns = default;
    [SerializeField] private Button standbyBtn = default;

    private int turnCount = 0;

    private void Start() {
        PickUpController.Instance.click -= OnClick;
        PickUpController.Instance.click += OnClick;
        TurnController.turnUp -= ChangeTurns;
        TurnController.turnUp += ChangeTurns;
        ChangeTurns();
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

}
