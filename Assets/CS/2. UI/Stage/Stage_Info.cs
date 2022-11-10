using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_Info : MonoBehaviour
{
    public TextMeshProUGUI Stage_Name;
    public TextMeshProUGUI Stage_MaxScore;
    public TextMeshProUGUI Stage_Information;

    [HideInInspector] public int Stage_Num;
    public Image Target_Sprite;
    public Sprite[] Info_Sprite;
    [SerializeField] GameObject Stage_Window;
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

        GameManager.GM.Stage_Name = Stage_Name.text;
        GameManager.GM.Stage_Information = Stage_Information.text;

        GameManager.GM.Stage_Move();
    }
}