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

    [Header("UI ����")]
    public GameObject shopUI;
    public GameObject quitUI;

    void Start()
    {
        StartCoroutine(UI_Update());
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OnQuit();
        }
    }

    public void OnShop() { Instantiate(shopUI); }
    public void OnGameStart()
    {
        string stageDes = GameManager.GM.STG_Excel();
        GameManager.GM.Stage_Move("GameScene", "���Ѹ��", stageDes);
    }

    // ���� ����
    public void OnQuit() { quitUI.SetActive(true); }
    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("���� ����!"); 
    }
    public void QuitBack() { quitUI.SetActive(false); }

    IEnumerator UI_Update()
    {
        while (true)
        {
            money.text = "" + GameManager.GM.data.money_GM;
            goods.text = "" + GameManager.GM.data.goods_GM;
            yield return new WaitForSeconds(2);
        }
    }
}
