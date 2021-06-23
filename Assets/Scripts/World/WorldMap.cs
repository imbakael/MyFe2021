using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMap : MonoBehaviour
{
    [SerializeField] private WorldMapUnit player = default;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f)) {
                if (hitInfo.collider != null) {
                    Vector3 target = hitInfo.collider.transform.GetChild(0).transform.position;
                    player.MoveTo(target);
                }
            }
            Debug.DrawRay(ray.origin, ray.direction, Color.green, 0.5f);
        }
    }

    
}
