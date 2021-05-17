using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpEffect : MonoBehaviour
{
    [SerializeField] private Text mainValueText = default;
    [SerializeField] private Image arrowImg = default;
    [SerializeField] private Image numberImg = default;
    [SerializeField] private Sprite[] numberSprites = default;

    public IEnumerator PlayEffect(int add) {
        int previousValue = Convert.ToInt32(mainValueText.text);
        int afterValue = previousValue + add;
        mainValueText.text = afterValue.ToString();
        arrowImg.gameObject.SetActive(true);
        numberImg.sprite = numberSprites[add - 1];
        yield return new WaitForSeconds(0.2f);
        numberImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }
}
