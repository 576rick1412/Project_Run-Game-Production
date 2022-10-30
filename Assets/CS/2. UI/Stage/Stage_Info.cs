using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Stage_Info : MonoBehaviour
{
    public TextMeshProUGUI Stage_Name;
    public TextMeshProUGUI Stage_Information;

    [Header("�ؽ�Ʈ")]
    public string Name;
    [TextArea(3, 5)] public string Information;
    [HideInInspector] public int Stage_Num;

    public GameObject Stage_Window;

    AsyncOperation op;
    void Start()
    {
        Stage_Name.text = "";
        Stage_Information.text = "";
        Stage_Window.SetActive(false);
    }

    void Update()
    {

    }
    public void OnStage_Info()
    {
        Stage_Window.SetActive(true);
    }
    public void OffStage_Info()
    {
        Stage_Window.SetActive(false);
    }

    public void GameStart()
    {
        GameManager.GM.Data.Floor_SpeedValue = 8f;
        GameManager.GM.Data.BGM_Value = 4f;
        switch (Stage_Num)
        {
            case 1: GameManager.GM.Data.GM_branch = 1; break;
            case 2: GameManager.GM.Data.GM_branch = 2; break;
            case 3: GameManager.GM.Data.GM_branch = 3; break;
            case 4: GameManager.GM.Data.GM_branch = 4; break;
        }
        Loading_Manager.LoadScene("Proto_InGame_Scene", Name, Information);
    }
}