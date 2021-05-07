using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConversationPanel : MonoBehaviour
{
    public static ConversationPanel Instance { get; private set; }

    [SerializeField] private Image leftHead = default;
    [SerializeField] private Image rightHead = default;
    [SerializeField] private Text leftText = default;
    [SerializeField] private Text rightText = default;
    [SerializeField] private Transform leftPanel = default;
    [SerializeField] private Transform rightPanel = default;

    private bool isClickScreen;
    private Vector2 leftTextOriginalAnchorPos;
    private Vector2 rightTextOriginalAnchorPos;

    public void Init() {
        Instance = this;
        leftTextOriginalAnchorPos = leftText.GetComponent<RectTransform>().anchoredPosition;
        rightTextOriginalAnchorPos = rightText.GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnBackgroundClick() {
        isClickScreen = true;
    }

    public void HandleAction(ConversationAction action) {
        StopAllCoroutines();
        StartCoroutine(Talk(action));
    }

    private IEnumerator Talk(ConversationAction action) {
        bool isLeft = action.IsLeft();
        SetShow(isLeft);
        Text curText = isLeft ? leftText : rightText;
        string content = action.Content;
        string s = string.Empty;
        int beforelineCount = 2;
        RectTransform rect = curText.GetComponent<RectTransform>();
        rect.anchoredPosition = isLeft ? leftTextOriginalAnchorPos : rightTextOriginalAnchorPos;
        for (int i = 0; i < content.Length; i++) {
            if ('#'.Equals(content[i])) {
                isClickScreen = false;
                while (!isClickScreen) {
                    yield return null;
                }
                continue;
            }
            s += content[i];
            curText.text = s;
            yield return new WaitForSeconds(0.06f);
            if (curText.cachedTextGenerator.lineCount > beforelineCount) {
                beforelineCount = curText.cachedTextGenerator.lineCount;
                rect.DOAnchorPosY(rect.anchoredPosition.y + 41, 0.3f);
                yield return new WaitForSeconds(0.3f);
            }
        }
        
        action.IsOver = true;
    }

    private void SetShow(bool isLeft) {
        leftHead.gameObject.SetActive(isLeft || leftHead.gameObject.activeSelf);
        rightHead.gameObject.SetActive(!isLeft || rightHead.gameObject.activeSelf);
        leftPanel.gameObject.SetActive(isLeft);
        rightPanel.gameObject.SetActive(!isLeft);
    }

    private void OnDestroy() {
        Instance = null;
    }
}
