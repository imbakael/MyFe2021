using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float maxLimitX = 2.8f;
    public float maxLimitY = 2.1f;

    private Vector3 previousMousePosition;
    private Vector3 currentMousePosition;

    private void Update() {
        Vector3 pos = transform.position;

        if (!Input.GetMouseButton(0)) {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        previousMousePosition = Input.GetMouseButtonDown(0) ? mousePos : currentMousePosition;
        currentMousePosition = mousePos;
        pos.x += (currentMousePosition.x - previousMousePosition.x) * 0.5f;
        pos.y += (currentMousePosition.y - previousMousePosition.y) * 0.5f;

        pos.x = Mathf.Clamp(pos.x, -maxLimitX, maxLimitX);
        pos.y = Mathf.Clamp(pos.y, -maxLimitY, maxLimitY);

        transform.position = pos;
    }
}
