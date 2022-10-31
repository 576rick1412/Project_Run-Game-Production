using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageButton : MonoBehaviour
{
    [Header("텍스트")]
    public string Stage_Name;
    [TextArea(3, 5)] public string Stage_Information;

    [SerializeField] int Stage_Num;
    Stage_Info SI;
    private void Awake()
    {
        SI = FindObjectOfType<Stage_Info>();
    }

    public void OnWindow()
    {
        SI.Stage_Name.text = Stage_Name;
        SI.Stage_Information.text = Stage_Information;

        SI.Stage_Num = Stage_Num;

        SI.Name = Stage_Name;

        SI.Stage_MaxScore.text = "최대 점수 : " + (GameManager.GM.Data.stage_Max_Score[Stage_Num] == 0 ? 0 :
            CommaText(GameManager.GM.Data.stage_Max_Score[GameManager.GM.Data.GM_branch]).ToString()); ;

        SI.Information = Stage_Information;

        SI.OnStage_Info();
    }
    string CommaText(long Sccore)
    {
        return string.Format("{0:#,###}", Sccore);
    }
}