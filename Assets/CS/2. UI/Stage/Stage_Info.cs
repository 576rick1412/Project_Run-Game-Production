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

        switch (GameManager.GM.nowStage)
        {
            case 1: Loading_Manager.LoadScene("Stage1_Scene", Stage_Name, Stage_Information); break;
            case 2: Loading_Manager.LoadScene("Stage2_Scene", Stage_Name, Stage_Information); break;
            case 3: Loading_Manager.LoadScene("Stage3_Scene", Stage_Name, Stage_Information); break;
            case 4: Loading_Manager.LoadScene("Stage4_Scene", Stage_Name, Stage_Information); break;
            case 5: Loading_Manager.LoadScene("Stage5_Scene", Stage_Name, Stage_Information); break;
            case 6: Loading_Manager.LoadScene("Stage6_Scene", Stage_Name, Stage_Information); break;
        }
    }
}