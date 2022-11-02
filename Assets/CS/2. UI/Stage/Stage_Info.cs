using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Stage_Info : MonoBehaviour
{
    public TextMeshProUGUI Stage_Name;
    public TextMeshProUGUI Stage_MaxScore;
    public TextMeshProUGUI Stage_Information;

    [HideInInspector] public int Stage_Num;

    public GameObject Stage_Window;
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
        GameManager.GM.Data.Floor_SpeedValue = GameManager.GM.Data.Set_Floor_SpeedValue;
        GameManager.GM.Data.BGM_Value = GameManager.GM.Data.Set_BGI_SpeedValue;
        GameManager.GM.Data.GM_branch = Stage_Num;

        Loading_Manager.LoadScene("Proto_InGame_Scene", Stage_Name, Stage_Information);
    }
}