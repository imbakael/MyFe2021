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

    private void Start() {
        PickUpController.Instance.click -= OnClick;
        PickUpController.Instance.click += OnClick;
    }

    private void OnClick(bool isClickMyUnit) {
        standbyBtn.gameObject.SetActive(isClickMyUnit);
    }

    public void OnStandbyButtonClicked() {
        if (PickUpController.Instance.Standby()) {
            standbyBtn.gameObject.SetActive(false);
        }
    }

}
