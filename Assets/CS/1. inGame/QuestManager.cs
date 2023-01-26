using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public struct Point
    {
        public float questPoint;    // ����Ʈ �޼� ��ǥ ����
        public int reward;          // ����Ʈ ���� ����
        public bool isFree;         // ���� ���� ����, ������ȭ or ������ȭ
    }

    [System.Serializable]
    public struct Quest
    {
        public TextMeshProUGUI questDesText;    // ����Ʈ ���� ��ü �ؽ�Ʈ

        public string questDesFort;             // ����Ʈ �� ���� �ؽ�Ʈ
        public string questDesBack;             // ����Ʈ �� ���� �ؽ�Ʈ

        public Point[] point;                   // ����Ʈ ��ǥ,����,�������� ����

        public TextMeshProUGUI rewardText;      // ����Ʈ ���� �ؽ�Ʈ

        public Image PointBar;                  // ����Ʈ �޼��� ��
        public TextMeshProUGUI PointText;       // ����Ʈ �޼��� �ؽ�Ʈ

        public bool isClear;                    // ����Ʈ Ŭ���� ����
        public bool isRewardClear;              // ����Ʈ ���� ȹ�� ����

        public GameObject questClear;           // ����Ʈ Ŭ���� ��ư
        public GameObject questEnd;             // ����Ʈ ���� ���� ���� ������
    }

    public Quest[] quest;


    void Start()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            int index = Random.Range(0, quest[i].point.Length);
            quest[i].questDesText.text = quest[i].questDesFort
                + " <color=yellow>" + CommaText(quest[i].point[index].questPoint) + "</color> "
                + quest[i].questDesBack;

            if (quest[i].point[index].isFree) quest[i].rewardText.text =
                    "���� : ���� " + CommaText(quest[i].point[index].reward) + " ���";
            else quest[i].rewardText.text =
                    "���� : ������ȭ " + CommaText(quest[i].point[index].reward) + " ĳ��";

            quest[i].PointBar.fillAmount = (30f / quest[i].point[index].questPoint);
            quest[i].PointText.text = "�޼��� : " + CommaText(20f) + " / " + CommaText(quest[i].point[index].questPoint);
        }       
    }  

    void Update()
    {
        
    }

    string CommaText(float Score) { return string.Format("{0:#,###}", Score); }
}
