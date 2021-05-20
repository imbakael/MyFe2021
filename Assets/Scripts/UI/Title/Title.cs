using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Button startBtn = default;
    [SerializeField] private Button continueBtn = default;
    [SerializeField] private Button deleteBtn = default;

    private void Start() {
        startBtn.onClick.AddListener(() => {
            // 删除存档
            GameDataManager.DeleteFile();
            Loader.LoadScene(Loader.Scene.GameScene);
        });
        continueBtn.onClick.AddListener(() => Loader.LoadScene(Loader.Scene.GameScene));
        SetBtnGray(continueBtn);
        SetBtnGray(deleteBtn);
        deleteBtn.onClick.AddListener(() => {
            GameDataManager.DeleteFile();
            SetBtnGray(continueBtn);
            SetBtnGray(deleteBtn);
        });
    }

    private void SetBtnGray(Button button) {
        if (!GameDataManager.ExistFile()) {
            button.enabled = false;
            button.GetComponentInChildren<Text>().color = new Color(190f / 255f, 190f / 255f, 190f / 255f);
        }
    }
}
