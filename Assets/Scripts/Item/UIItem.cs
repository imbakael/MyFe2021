using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private Image icon = default;
    [SerializeField] private Text name = default;
    [SerializeField] private Text count = default;
    [SerializeField] private Text equip = default;

    private Item item;

    public void Init(Item item) {
        this.item = item;
        icon.sprite = GetSprite();
        count.text = item.curCount + " / " + item.count;
        equip.gameObject.SetActive(IsEquiped());
    }

    public Sprite GetSprite() {
        return null;
    }

    private bool IsEquiped() {
        throw new NotImplementedException();
    }

}
