using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UnitSelectedPanel unitSelectedPanel = default;
    [SerializeField] private UnitDetailPanel unitDetailPanel = default;
    [SerializeField] private BaseTipsUI baseTipsUI = default;
    [SerializeField] private ConversationPanel conversationPanel = default;
    [SerializeField] private TurnTransPanel turnTransPanel = default;

    [SerializeField] private Canvas uiCanvas = default;
    [SerializeField] private Camera uiCamera = default;

    public void CreateUnitSelectedPanel(MapUnit unit) {
        DestroyPanel<UnitSelectedPanel>();
        UnitSelectedPanel panel = Instantiate(unitSelectedPanel);
        panel.Init(unit);
        panel.transform.SetParent(uiCanvas.transform, false);
    }

    public void DestroyPanel<T>() where T : MonoBehaviour {
        T panel = uiCanvas.GetComponentInChildren<T>();
        if (panel != null) {
            Destroy(panel.gameObject);
        }
    }

    public void CreateUnitDetailPanel(MapUnit unit) {
        DestroyPanel<UnitDetailPanel>();
        UnitDetailPanel panel = Instantiate(unitDetailPanel);
        panel.Init(unit);
        panel.transform.SetParent(uiCanvas.transform, false);
    }

    public void CreateUnitTips(string name, Vector2 screenPos) {
        DestroyPanel<BaseTipsUI>();
        BaseTipsUI tips = Instantiate(baseTipsUI);
        tips.Init(name);
        tips.transform.SetParent(uiCanvas.transform, false);
        Vector2 uiPos = ScreenToUI(uiCanvas.GetComponent<RectTransform>(), screenPos, uiCamera);
        tips.GetComponent<RectTransform>().anchoredPosition = uiPos;
    }

    public static Vector2 ScreenToUI(RectTransform rect, Vector3 screenPos, Camera camera = null) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, camera, out Vector2 uiPos);
        return uiPos;
    }

    public void CreateConversationPanel() {
        DestroyPanel<ConversationPanel>();
        ConversationPanel panel = Instantiate(conversationPanel);
        panel.Init();
        panel.transform.SetParent(uiCanvas.transform, false);
    }

    public void CreateTurnTransPanel(TeamType team, Action endAction) {
        TurnTransPanel panel = Instantiate(turnTransPanel);
        panel.PlayTurnTrans(team, endAction);
        panel.transform.SetParent(uiCanvas.transform, false);
    }

    public void ShowMask() {

    }

    public void HideMask() { 
}
    
}
