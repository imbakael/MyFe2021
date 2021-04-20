using UnityEngine;
using UnityEngine.EventSystems;

// 长按组件 长按后可以弹出提示框or其他something
public class BaseLongTouchUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [SerializeField] private BaseTipsUI tips = default;

    private BaseTipsUI curTips;

    public void OnPointerDown(PointerEventData eventData) {
        // 按下弹出
        curTips = Instantiate(tips);
        curTips.transform.SetParent(transform);
        curTips.transform.localScale = Vector3.one;
        curTips.transform.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    }

    public void OnPointerUp(PointerEventData eventData) {
        // 抬起消失
        Destroy(curTips.gameObject);
    }
}
