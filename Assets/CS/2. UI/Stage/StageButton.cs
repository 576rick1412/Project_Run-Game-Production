using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    [SerializeField] GameObject hubObject;
    [Header("설명창")]
    [SerializeField] GameObject normalUI;
    [SerializeField] GameObject hardUI;

    [Header("최고점수 텍스트")]
    [SerializeField] TextMeshProUGUI normalText;
    [SerializeField] TextMeshProUGUI hardText;

    void Start()
    {
        OnNormal();
        
        if (GameManager.GM.data.nomalMaxScore == 0) { normalText.text = "최고점수 : 기록없음"; }
        else { normalText.text = "최고점수 : " + "<color=yellow>" + CommaText(GameManager.GM.data.nomalMaxScore) + "</color>" + " 점"; }

        if (GameManager.GM.data.hardMaxScore == 0) { hardText.text = "최고점수 : 기록없음"; }
        else { hardText.text = "최고점수 : " + "<color=yellow>" + CommaText(GameManager.GM.data.hardMaxScore) + "</color>" + " 점"; }
    }

    public void OnNormal()
    {
        GameManager.GM.isNormal = true;

        normalUI.SetActive(true);
        hardUI.SetActive(false);
    }
    public void OnHard()
    {
        GameManager.GM.isNormal = false;
        
        hardUI.SetActive(true);
        normalUI.SetActive(false);
    }


    public void PlayButton()
    {
        string stageDes = GameManager.GM.STG_Excel();

        // 게임 플레이 횟수 카운터 + 저장
        if (QuestManager.QM.questDB.curPointQuestDB[4] < QuestManager.QM.quest[4].point.questPoint)
        {   QuestManager.QM.questDB.curPointQuestDB[4]++;QuestManager.QM.SavaData(); }

        if(GameManager.GM.isNormal) GameManager.GM.Stage_Move("GameScene", "일반모드", stageDes);
        else GameManager.GM.Stage_Move("GameScene", "하드모드", stageDes);
    }
    public void BackButton()
    {
        Destroy(hubObject);
    }

    string CommaText(long score)
    {
        return string.Format("{0:#,###}", score);
    }
}