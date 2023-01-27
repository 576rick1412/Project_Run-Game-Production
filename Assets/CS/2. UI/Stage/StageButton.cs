using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    [SerializeField] GameObject hubObject;
    [Header("����â")]
    [SerializeField] GameObject normalUI;
    [SerializeField] GameObject hardUI;

    [Header("�ְ����� �ؽ�Ʈ")]
    [SerializeField] TextMeshProUGUI normalText;
    [SerializeField] TextMeshProUGUI hardText;

    void Start()
    {
        OnNormal();
        
        if (GameManager.GM.data.nomalMaxScore == 0) { normalText.text = "�ְ����� : ��Ͼ���"; }
        else { normalText.text = "�ְ����� : " + "<color=yellow>" + CommaText(GameManager.GM.data.nomalMaxScore) + "</color>" + " ��"; }

        if (GameManager.GM.data.hardMaxScore == 0) { hardText.text = "�ְ����� : ��Ͼ���"; }
        else { hardText.text = "�ְ����� : " + "<color=yellow>" + CommaText(GameManager.GM.data.hardMaxScore) + "</color>" + " ��"; }
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

        // ���� �÷��� Ƚ�� ī���� + ����
        if (QuestManager.QM.questDB.curPointQuestDB[4] < QuestManager.QM.quest[4].point.questPoint)
        {   QuestManager.QM.questDB.curPointQuestDB[4]++;QuestManager.QM.SavaData(); }

        if(GameManager.GM.isNormal) GameManager.GM.Stage_Move("GameScene", "�Ϲݸ��", stageDes);
        else GameManager.GM.Stage_Move("GameScene", "�ϵ���", stageDes);
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