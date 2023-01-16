using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MasicMainUI : MonoBehaviour
{
    public TextMeshProUGUI nickname;
    public TextMeshProUGUI level;
    public TextMeshProUGUI money;
    public TextMeshProUGUI goods;
    public Image exe;

    [Header("UI 띄우기")]
    public GameObject inventoryPref;
    public GameObject shopPref;
    public GameObject gamestartPref;
    public GameObject quitScenePref;

    private void Awake()
    {
        quitScenePref.SetActive(false);
    }
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
    public void OnInventory() { Instantiate(inventoryPref); }
    public void OnShop() { Instantiate(shopPref); }
    public void OnGamestart() { Instantiate(gamestartPref); }

    // 게임 종료
    public void OnQuit() { quitScenePref.SetActive(true); }
    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("게임 종료!"); 
    }
    public void QuitBack() { quitScenePref.SetActive(false); }

    IEnumerator UI_Update()
    {
        while (true)
        {
            nickname.text = GameManager.GM.data.nickname_GM;
            level.text = "Lv. " + GameManager.GM.data.level_GM;
            money.text = "" + GameManager.GM.data.money_GM;
            goods.text = "" + GameManager.GM.data.goods_GM;
            exe.fillAmount = (GameManager.GM.data.exe_GM / (GameManager.GM.data.maxExe_GM + (GameManager.GM.data.level_GM * 200)));
            // 기본 최종치( 1000 ) + ( 현재 레벨 * 200 ) = 최종 경험치 요구량
            // 만약 레벨이 5 일 경우 -> [ 1000 + ( 5 * 200 ) ] => [ 1000 + 1000 ] = 2000
            yield return new WaitForSeconds(1);
        }
    }
}