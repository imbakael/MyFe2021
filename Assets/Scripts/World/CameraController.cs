using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer map = default;

    public float moveSpeed = 0.5f;
    public float maxLimitX = 2.8f;
    public float maxLimitY = 2.1f;

    private Vector3 previousMousePosition;
    private Vector3 currentMousePosition;

    private void Start() {
        MiniMap.MoveCamera -= SetPosition;
        MiniMap.MoveCamera += SetPosition;
    }

    private void Update() {
        if (!Input.GetMouseButton(0)) {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        Vector3 pos = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        previousMousePosition = Input.GetMouseButtonDown(0) ? mousePos : currentMousePosition;
        currentMousePosition = mousePos;
        pos.x += (currentMousePosition.x - previousMousePosition.x) * moveSpeed;
        pos.y += (currentMousePosition.y - previousMousePosition.y) * moveSpeed;

        pos.x = Mathf.Clamp(pos.x, -maxLimitX, maxLimitX);
        pos.y = Mathf.Clamp(pos.y, -maxLimitY, maxLimitY);

        transform.position = pos;
    }

    public void SetPosition(Vector2 relative) {
        float x = relative.x * map.bounds.size.x * 0.5f;
        float y = relative.y * map.bounds.size.y * 0.5f;
        x = Mathf.Clamp(x, -maxLimitX, maxLimitX);
        y = Mathf.Clamp(y, -maxLimitY, maxLimitY);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
