using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField]
    private Image curIn = default;
    [SerializeField]
    private Image middle = default;
    [SerializeField]
    private Image curOut = default;
    [SerializeField]
    private float hurtSpeed = default;
    [SerializeField]
    private Sprite[] sprites = default;

    private int maxLayers;
    private int leftLayers; // 血条剩余层数
    private int maxHpPerLayer; // 每层血量上限
    private int hpPerLayer; // 当前层血量
    private int spriteIndex;
    private bool isReadyDestroyed = false;

    public void MyStart(int maxHp, int maxLayers) {
        this.maxLayers = leftLayers = maxLayers;
        hpPerLayer = maxHpPerLayer = maxHp / maxLayers;
        spriteIndex = 1;
    }

    private void Update() {
        float rate = hpPerLayer <= 0 ? 0f : (hpPerLayer * 1f / maxHpPerLayer);
        curOut.fillAmount = rate;
        float middleAmount = middle.fillAmount;
        middle.fillAmount = (middleAmount > rate) ? (middleAmount - hurtSpeed) : rate;
        if (isReadyDestroyed) {
            DestroyWhenZeroHP();
        }
    }

    private void DestroyWhenZeroHP() {
        if (middle.fillAmount <= 0f) {
            Destroy(gameObject, 0.5f);
        }
    }

    public void TakeDamage(int damage) {
        if (isReadyDestroyed) {
            return;
        }
        hpPerLayer -= damage;
        TakeDamage();
    }

    private void TakeDamage() {
        if (leftLayers <= 1) {
            curIn.fillAmount = 0f;
        }
        while (hpPerLayer < 0 && leftLayers > 1) {
            SwapLayer();
            ChangeInsideImage();
            ChangeParams();
        }
        if (leftLayers <= 1 && hpPerLayer <= 0) {
            isReadyDestroyed = true;
        }
    }

    private void SwapLayer() {
        curIn.transform.SetSiblingIndex(2);
        curOut.transform.SetSiblingIndex(0);
        Image tempImg = curOut;
        curOut = curIn;
        curIn = tempImg;
    }

    private void ChangeInsideImage() {
        if (++spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }
        curIn.sprite = sprites[spriteIndex];
    }

    private void ChangeParams() {
        leftLayers -= 1;
        hpPerLayer += maxHpPerLayer;
        curIn.fillAmount = leftLayers <= 1 ? 0f : 1f;
        middle.fillAmount = 1f;
    }
}
