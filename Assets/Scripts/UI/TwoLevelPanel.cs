using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoLevelPanel : MonoBehaviour
{
    [SerializeField] private Transform swordMaster = default;

    public void ShowMaster() {
        swordMaster.gameObject.SetActive(true);
    }
}
