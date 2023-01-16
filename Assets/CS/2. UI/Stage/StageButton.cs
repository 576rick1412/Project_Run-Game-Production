using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    [SerializeField] int stageNum;

    [Header("기타")]
    [SerializeField] Chapter_Info info;
    [SerializeField] TextMeshProUGUI stageText;

    Stage_Info SI;
    [SerializeField] GameObject[] StarS = new GameObject[3];
    private void Awake()
    {
        SI = FindObjectOfType<Stage_Info>();
        stageText.text = info.Info[stageNum - 1].Stage_Num;

        switch (GameManager.GM.data.stageClearStars[stageNum])
        {
            case 0: break;
            case 1: StarS[0].SetActive(true); break;
            case 2: StarS[0].SetActive(true); StarS[1].SetActive(true); break;
            case 3: StarS[0].SetActive(true); StarS[1].SetActive(true); StarS[2].SetActive(true); break;
        }
    }

    public void OnWindow()
    {
        for (int i = 0; i < info.Info.Count; ++i)
        {
            if (info.Info[i].Branch == stageNum)
            {
                SI.stageName.text = info.Info[i].Stage_Num;
                SI.stageInformation.text = info.Info[i].Stage_Description;
                SI.stageNum = stageNum;

                SI.stageMaxScore.text = "최대 점수 : " + (GameManager.GM.data.stageMaxScores[i + 1] == 0 ? "기록 없음" :
                    CommaText(GameManager.GM.data.stageMaxScores[i + 1]).ToString());

                SI.targetSprite.sprite = SI.infoSprites[stageNum % 10];

                SI.OnStage_Info();
            }
        }
    }
    string CommaText(long score)
    {
        return string.Format("{0:#,###}", score);
    }
}