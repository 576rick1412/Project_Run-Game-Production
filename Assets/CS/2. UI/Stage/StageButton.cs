using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    [SerializeField] int Stage_Num;

    [Header("기타")]
    [SerializeField] Chapter_Info Info;
    [SerializeField] TextMeshProUGUI Stage_Text;


    Stage_Info SI;
    private void Awake()
    {
        SI = FindObjectOfType<Stage_Info>();
        Stage_Text.text = Info.Info[Stage_Num - 1].Stage_Num;
    }

    public void OnWindow()
    {
        for (int i = 0; i < Info.Info.Count; ++i)
        {
            if (Info.Info[i].Branch == Stage_Num)
            {
                SI.Stage_Name.text = Info.Info[i].Stage_Num;
                SI.Stage_Information.text = Info.Info[i].Stage_Description;
                SI.Stage_Num = Stage_Num;

                SI.Stage_MaxScore.text = "최대 점수 : " + (GameManager.GM.Data.stage_Max_Score[i + 1] == 0 ? "기록 없음" :
                    CommaText(GameManager.GM.Data.stage_Max_Score[i + 1]).ToString());

                SI.Target_Sprite.sprite = SI.Info_Sprite[Stage_Num % 10];

                SI.OnStage_Info();
            }
        }
    }
    string CommaText(long Sccore)
    {
        return string.Format("{0:#,###}", Sccore);
    }
}