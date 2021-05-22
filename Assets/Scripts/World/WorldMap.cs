using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [SerializeField] private WorldMapUnit player = default;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.down);
            if (hit.collider != null) {
                Vector3 target = hit.collider.transform.GetChild(0).transform.position;
                player.MoveTo(target);
            }
        }
    }

    
}
