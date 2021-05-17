using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleHealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform bound = default;
    [SerializeField] private RectTransform prefab = default;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Text hpText = default;

    private List<RectTransform> items;
    private int curHp;
    private int maxHp;
    private int index;

    private void Start() {
        Init(30, 30);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ChangeHp(-31);
        } else if (Input.GetKeyDown(KeyCode.W)) {
            ChangeHp(40);
        }
    }

    public void Init(int curHp, int maxHp) {
        this.curHp = curHp;
        this.maxHp = maxHp;
        hpText.text = curHp.ToString();
        items = new List<RectTransform>();
        float width = prefab.sizeDelta.x;
        for (int i = 0; i < maxHp; i++) {
            RectTransform item = Instantiate(prefab);
            item.SetParent(bound, false);
            item.anchoredPosition = new Vector2(i * width, item.anchoredPosition.y);
            if ((i + 1) > curHp) {
                item.GetChild(0).gameObject.SetActive(false);
            }
            items.Add(item);
        }
        for (int i = items.Count - 1; i >= 0; i--) {
            RectTransform item = items[i];
            if (item.GetChild(0).gameObject.activeSelf) {
                index = i;
                break;
            }
        }
    }

    /// <summary>
    /// 改变血条
    /// </summary>
    /// <param name="value">正值增加，负值降低</param>
    public void ChangeHp(int value) {
        int previousHp = curHp;
        curHp += value;
        curHp = Mathf.Clamp(curHp, 0, maxHp);
        int lastHp = curHp;
        if (previousHp != lastHp) {
            StartCoroutine(Change(previousHp, lastHp));
        }
    }

    private IEnumerator Change(int previousHp, int lastHp) {
        int delta = lastHp - previousHp;
        int count = Mathf.Abs(delta);
        hpText.DOCounter(previousHp, lastHp, duration * count * 1.5f);
        if (delta < 0) {
            while (count > 0) {
                items[index--].GetChild(0).gameObject.SetActive(false);
                yield return new WaitForSeconds(duration);
                count--;
            }
        } else {
            while (count > 0) {
                items[++index].GetChild(0).gameObject.SetActive(true);
                yield return new WaitForSeconds(duration);
                count--;
            }
        }
    }
}
