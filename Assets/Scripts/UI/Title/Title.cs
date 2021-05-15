using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Button startBtn = default;

    private void Start() {
        startBtn.onClick.AddListener(() => Loader.LoadScene(Loader.Scene.GameScene));
    }
}
