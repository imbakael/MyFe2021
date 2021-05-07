using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 长按组件 长按后可以弹出提示框or其他something
public class BaseLongTouchUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private Text text;
    private string textName;

    private void Awake() {
        text = GetComponent<Text>();
        textName = GetName();
    }

    private string GetName() {
        string name = text.text;
        return name.Replace(",", string.Empty).Replace(":", string.Empty);
    }

    public void OnPointerDown(PointerEventData eventData) {
        UIManager.Instance.CreateUnitTips(textName, eventData.position);
        //Debug.Log("localpos = " + text.transform.localPosition + ". pos = " + text.transform.position + ". archerpos = " + text.GetComponent<RectTransform>().anchoredPosition);
        //Debug.LogError("eventpos = " + eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData) {
        UIManager.Instance.DestroyPanel<BaseTipsUI>();
    }
}
