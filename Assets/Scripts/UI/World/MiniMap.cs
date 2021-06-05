using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMap : MonoBehaviour
{
    public static event Action<Vector2> MoveCamera;

    private void Update() {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject()) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, Input.mousePosition, null, out Vector2 localPoint);
            RectTransform rectTrans = GetComponent<RectTransform>();
            float halfWidth = rectTrans.sizeDelta.x * 0.5f;
            float halfHeight = rectTrans.sizeDelta.y * 0.5f;
            Vector2 relative = new Vector2(localPoint.x / halfWidth, localPoint.y / halfHeight);
            MoveCamera?.Invoke(relative);
        }
    }
}
