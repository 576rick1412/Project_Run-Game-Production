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
        public float questPoint;    // 퀘스트 달성 목표 점수
        public int reward;          // 퀘스트 보상 내용
        public bool isFree;         // 보상 내용 여부, 무료재화 or 유로재화
    }

    [System.Serializable]
    public struct Quest
    {
        public TextMeshProUGUI questDesText;    // 퀘스트 내용 전체 텍스트

        public string questDesFort;             // 퀘스트 앞 내용 텍스트
        public string questDesBack;             // 퀘스트 뒤 내용 텍스트

        public Point[] point;                   // 퀘스트 목표,보상,보상종류 설정

        public TextMeshProUGUI rewardText;      // 퀴스트 보상 텍스트

        public Image PointBar;                  // 퀘스트 달성률 바
        public TextMeshProUGUI PointText;       // 퀴스트 달성률 텍스트

        public bool isClear;                    // 퀘스트 클리어 여부
        public bool isRewardClear;              // 퀘스트 보상 획득 여부

        public GameObject questClear;           // 퀘스트 클리어 버튼
        public GameObject questEnd;             // 퀘스트 보상 받은 이후 가림판
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
                    "보상 : 코인 " + CommaText(quest[i].point[index].reward) + " 골드";
            else quest[i].rewardText.text =
                    "보상 : 유료재화 " + CommaText(quest[i].point[index].reward) + " 캐시";

            quest[i].PointBar.fillAmount = (30f / quest[i].point[index].questPoint);
            quest[i].PointText.text = "달성률 : " + CommaText(20f) + " / " + CommaText(quest[i].point[index].questPoint);
        }       
    }  

    void Update()
    {
        
    }

    string CommaText(float Score) { return string.Format("{0:#,###}", Score); }
}
