using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public struct Quest
    {
        public TextMeshProUGUI questDesText;   // ����Ʈ ���� ��ü �ؽ�Ʈ

        public string questDesFort;             // ����Ʈ �� ���� �ؽ�Ʈ
        public string questDesBack;             // ����Ʈ �� ���� �ؽ�Ʈ

        [System.Serializable]
        public struct Point
        {
            public float questPoint;    // ����Ʈ �޼� ��ǥ ����
            public int reward;          // ����Ʈ ���� ����
            public bool isFree;         // ���� ���� ����, ������ȭ or ������ȭ
        }
        public Point[] point;

        public TextMeshProUGUI rewardText;      // ����Ʈ ���� �ؽ�Ʈ

        public Image PointBar;                 // ����Ʈ �޼��� ��
        public TextMeshProUGUI PointText;      // ����Ʈ �޼��� �ؽ�Ʈ

        public bool isClear;                    // ����Ʈ Ŭ���� ����
        public bool isRewardClear;              // ����Ʈ ���� ȹ�� ����

        public GameObject questClear;           // ����Ʈ Ŭ���� ��ư
        public GameObject questEnd;             // ����Ʈ ���� ���� ���� ������
    }

    public Quest nonObstacle;      // ��ֹ��� �浹���� �ʰ�~~
    Quest questJump;        // ���� ~~ ȸ
    Quest questDoubleJump;  // �������� ~~ȸ
    Quest questSlide;       // �����̵� ~~ȸ

    /*
    // ����Ʈ �� ���� �ؽ�Ʈ
    public string[] questDesList = 
    { 
        "��ֹ��� �浹���� �ʰ�", 
        "�� ���ӿ��� ����",
        "�� ���ӿ��� ��������",   
        "�� ���ӿ��� �����̵�",
        "�� ���ӿ���",           
        "��ֹ���",
        "�ϵ� ���",             
        "�Ϲ� ���",
        "�ƹ� ���Ӹ��",         
        "����Ʈ"
    };

    public int[] normalPointList = { 10, 20, 30, 40, 50, 60 };
    public int[] MeterPointList = { 10, 20, 30, 40, 50, 60 };
    */

    void Start()
    {
        int index = Random.Range(0, nonObstacle.point.Length);
        nonObstacle.questDesText.text = nonObstacle.questDesFort 
            + " <color=yellow>" + CommaText(nonObstacle.point[index].questPoint) + "</color> " 
            + nonObstacle.questDesBack;

        if(nonObstacle.point[index].isFree) nonObstacle.rewardText.text = 
                "���� : ���� " + CommaText(nonObstacle.point[index].reward) + " ���";
        else nonObstacle.rewardText.text = 
                "���� : ������ȭ " + CommaText(nonObstacle.point[index].reward) + " ĳ��";

        nonObstacle.PointBar.fillAmount = (30f / nonObstacle.point[index].questPoint);
        nonObstacle.PointText.text = "�޼��� : " + CommaText(20f) + " / " + CommaText(nonObstacle.point[index].questPoint);
    }  

    void Update()
    {
        
    }

    string CommaText(float Score) { return string.Format("{0:#,###}", Score); }
}
