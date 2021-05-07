using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationAction : IAction {

    public bool IsOver { get; set; }
    public string Content { get; private set; }

    private PosType posType;

    public ConversationAction(PosType posType, string content) {
        this.posType = posType;
        Content = content;
        IsOver = false;
    }

    public void Action() {
        if (ConversationPanel.Instance == null) {
            UIManager.Instance.CreateConversationPanel();
        }
        ConversationPanel.Instance.HandleAction(this);
    }

    public bool IsLeft() => posType == PosType.Left;
}
