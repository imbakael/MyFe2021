using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class TurnTransPanel : MonoBehaviour
{
    [SerializeField] private RectTransform start = default;
    [SerializeField] private RectTransform end = default;
    [SerializeField] private RectTransform banner = default;
    [SerializeField] private Sprite[] sprites = default;
    [SerializeField] private Image bannerImg = default;
    [SerializeField] private float duration = 1f;

    public void PlayTurnTrans(TeamType team, Action endAction) {
        bannerImg.sprite = sprites[(int)team];
        bannerImg.color = new Color(1, 1, 1, 0);
        banner.anchoredPosition = start.anchoredPosition;
        banner.DOAnchorPos(Vector2.zero, duration);
        bannerImg.DOFade(1, duration).OnComplete(() => {
            banner.DOAnchorPos(end.anchoredPosition, duration);
            bannerImg.DOFade(0, duration).OnComplete(() => {
                Destroy(gameObject);
                endAction();
            });
        });
    }
}
