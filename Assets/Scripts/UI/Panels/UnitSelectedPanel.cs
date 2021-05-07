using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectedPanel : MonoBehaviour
{
    [SerializeField] private Button detailBtn = default;
    [SerializeField] private Text attackText = default;
    [SerializeField] private Text defendText = default;

    private void Awake() {
        detailBtn.onClick.AddListener(OnDetailBtnClick);
    }

    private void OnDetailBtnClick() {
        UIManager.Instance.CreateUnitDetailPanel(PickUpController.Instance.CurMapUnit);
    }

    public void Init(MapUnit unit) {
        // 初始化头像、攻击力、防御力等
        attackText.text = unit.GetHashCode().ToString();
    }

    private void OnDestroy() {
        detailBtn.onClick.RemoveAllListeners();
    }
}
