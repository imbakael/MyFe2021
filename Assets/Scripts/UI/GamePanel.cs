using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public void OnStandbyButtonClicked() {
        PickUpController.Instance.Standby();
    }
}
