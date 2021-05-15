using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public void OnStandbyButtonClicked() {
        TurnController.isMyTurn = false;
        PickUpController.Instance.Standby();
    }
}
