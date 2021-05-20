using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScriptController : Singleton<LevelScriptController>
{
    [SerializeField] private LevelScript currentScript = default;

    public bool isOver = false;

    private List<LevelScript> currentLevelScripts; // 每一关剧本由多个剧本子集构成
    private List<List<LevelScript>> allScripts; // 所有关卡剧本

    //private void Start() {
    //    currentScript = new LevelScript();
    //}

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.U)) {
    //        StartScript();
    //    }
    //}

    public void StartScript() {
        currentScript = new LevelScript();
        StartCoroutine(HandleScript());
    }

    private IEnumerator HandleScript() {
        IAction action = currentScript.GetAction();
        while (action != null) {
            action.Action();
            while (!action.IsOver) {
                yield return null;
            }
            action = currentScript.GetAction();
        }
        currentScript.Skip();
        isOver = true;
        UIManager.Instance.DestroyPanel<ConversationPanel>();
    }
}
