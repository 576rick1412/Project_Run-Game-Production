using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    [Header("설명창")]
    [SerializeField] GameObject normalUI;
    [SerializeField] GameObject hardUI;

    [Header("최고점수 텍스트")]
    [SerializeField] TextMeshProUGUI normalText;
    [SerializeField] TextMeshProUGUI hardText;

    void Start()
    {
        if (GameManager.GM.data.nomalMaxScore == 0) { normalText.text = "최고점수 : 기록없음"; }
        else { normalText.text = "최고점수 : " + CommaText(GameManager.GM.data.nomalMaxScore) + " 점"; }

        if (GameManager.GM.data.hardMaxScore == 0) { hardText.text = "최고점수 : 기록없음"; }
        else { hardText.text = "최고점수 : " + CommaText(GameManager.GM.data.hardMaxScore) + " 점"; }
    }

    public void OnNormal()
    {
        GameManager.GM.isNormal = true;
    }

    public void OnHard()
    {
        GameManager.GM.isNormal = false;
    }
    string CommaText(long score)
    {
        return string.Format("{0:#,###}", score);
    }
}