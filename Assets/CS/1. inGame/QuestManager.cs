using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [Serializable]
    public struct Point
    {
        public TextMeshProUGUI rewardText;      // ����Ʈ ���� �ؽ�Ʈ

        public float questPoint;                // ����Ʈ �޼� ��ǥ ����
        public float curPoint;                  // ���� ����Ʈ �޼� ����

        public int reward;                      // ����Ʈ ���� ����
        public bool isFree;                     // ���� ���� ����, ������ȭ or ������ȭ
    }

    [Serializable]
    public struct Content
    {
        public TextMeshProUGUI questDesText;    // ����Ʈ ���� ��ü �ؽ�Ʈ

        public string questDesFort;             // ����Ʈ �� ���� �ؽ�Ʈ
        public string questDesBack;             // ����Ʈ �� ���� �ؽ�Ʈ
    }

    [Serializable]
    public struct Attain
    {
        public Image PointBar;                  // ����Ʈ �޼��� ��
        public TextMeshProUGUI PointText;       // ����Ʈ �޼��� �ؽ�Ʈ
    }

    [Serializable]
    public struct Check
    {
        public bool isClear;                    // ����Ʈ Ŭ���� ����
        public bool isRewardClear;              // ����Ʈ ���� ȹ�� ����
    }

    [Serializable]
    public struct Panel
    {
        public GameObject questClear;           // ����Ʈ Ŭ���� ��ư
        public GameObject questEnd;             // ����Ʈ ���� ���� ���� ������
    }

    [Serializable]
    public struct Quest
    {
        public Content content;                 // ����Ʈ ���� �ؽ�Ʈ
        public Point point;                     // ����Ʈ ��ǥ,����,�������� ����
        public Attain attain;                   // ����Ʈ �޼���
        public Check check;                     // ����Ʈ ���� �۵� ���� (bool)
        public Panel panel;                     // ����Ʈ ���� ǥ��
    }
    public Quest[] quest;

    /*
     *  0. ��ֹ��� �浹���� �ʰ� ~~ �� �޼�
     *  1. �� ���ӿ��� ���� ~~ȸ �޼�
     *  2. �� ���ӿ��� �������� ~~ȸ ����
     *  3. �� ���ӿ��� �����̵� ~~ȸ �޼�
     *  5. ���� ~~ȸ �÷���
     *  6. ���� ����Ʈ ���� Ŭ����
     */
    void Start()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            // ����Ʈ ��ü �ؽ�Ʈ
            quest[i].content.questDesText.text = quest[i].content.questDesFort
                    + " <color=yellow>" + CommaText(quest[i].point.questPoint) + "</color> "
                    + quest[i].content.questDesBack;

            if (quest[i].point.isFree) quest[i].point.rewardText.text =
                    "���� : ���� " + CommaText(quest[i].point.reward) + " ���";
            else quest[i].point.rewardText.text =
                    "���� : ������ȭ " + CommaText(quest[i].point.reward) + " ĳ��";

            quest[i].attain.PointBar.fillAmount = (quest[i].point.curPoint / quest[i].point.questPoint);

            quest[i].attain.PointText.text =
                "�޼��� : " + CommaText(quest[i].point.curPoint) + " / " + CommaText(quest[i].point.questPoint);

            // ����Ʈ �ǳ�
            if (quest[i].check.isClear) quest[i].panel.questClear.SetActive(true);
            if (quest[i].check.isRewardClear)
            {
                quest[i].panel.questClear.SetActive(false);
                quest[i].panel.questEnd.SetActive(true);
            }
        }
    }

    void Update()
    {
        
    }

    string CommaText(float Score) 
    {
        if (Score <= 0) return "0";

        return string.Format("{0:#,###}", Score); 
    }

   
}