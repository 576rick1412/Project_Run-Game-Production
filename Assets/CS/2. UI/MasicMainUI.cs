using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MasicMainUI : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI goods;

    [Header("UI 띄우기")]
    public GameObject shopUI;
    public GameObject quitUI;
    public GameObject stageUI;
    public GameObject questUI;

    void Start()
    {

    }

    private void Update()
    {
        UI_Update();

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OnQuit();
        }
    }

    public void OnShop() { Instantiate(shopUI); }
    public void OnGameStart() { Instantiate(stageUI); }
    public void OnQuest() { Instantiate(questUI); }

    // 게임 종료
    public void OnQuit() { quitUI.SetActive(true); }
    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("게임 종료!");
        GameManager.GM.SavaData();
        QuestManager.QM.SavaData();
    }
    public void QuitBack() { quitUI.SetActive(false); }

    void UI_Update()
    {
        money.text = "" + GameManager.GM.data.money_GM;
        goods.text = "" + GameManager.GM.data.goods_GM;
    }
}
