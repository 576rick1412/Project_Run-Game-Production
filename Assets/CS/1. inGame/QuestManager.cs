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
        public TextMeshProUGUI questDesText;   // 퀘스트 내용 전체 텍스트

        public string questDesFort;             // 퀘스트 앞 내용 텍스트
        public string questDesBack;             // 퀘스트 뒤 내용 텍스트

        [System.Serializable]
        public struct Point
        {
            public float questPoint;    // 퀘스트 달성 목표 점수
            public int reward;          // 퀘스트 보상 내용
            public bool isFree;         // 보상 내용 여부, 무료재화 or 유로재화
        }
        public Point[] point;

        public TextMeshProUGUI rewardText;      // 퀴스트 보상 텍스트

        public Image PointBar;                 // 퀘스트 달성률 바
        public TextMeshProUGUI PointText;      // 퀴스트 달성률 텍스트

        public bool isClear;                    // 퀘스트 클리어 여부
        public bool isRewardClear;              // 퀘스트 보상 획득 여부

        public GameObject questClear;           // 퀘스트 클리어 버튼
        public GameObject questEnd;             // 퀘스트 보상 받은 이후 가림판
    }

    public Quest nonObstacle;      // 장애물에 충돌하지 않고~~
    Quest questJump;        // 점프 ~~ 회
    Quest questDoubleJump;  // 더블점프 ~~회
    Quest questSlide;       // 슬라이드 ~~회

    /*
    // 퀘스트 앞 내용 텍스트
    public string[] questDesList = 
    { 
        "장애물에 충돌하지 않고", 
        "한 게임에서 점프",
        "한 게임에서 더블점프",   
        "한 게임에서 슬라이드",
        "한 게임에서",           
        "장애물에",
        "하드 모드",             
        "일반 모드",
        "아무 게임모드",         
        "퀘스트"
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
                "보상 : 코인 " + CommaText(nonObstacle.point[index].reward) + " 골드";
        else nonObstacle.rewardText.text = 
                "보상 : 유료재화 " + CommaText(nonObstacle.point[index].reward) + " 캐시";

        nonObstacle.PointBar.fillAmount = (30f / nonObstacle.point[index].questPoint);
        nonObstacle.PointText.text = "달성률 : " + CommaText(20f) + " / " + CommaText(nonObstacle.point[index].questPoint);
    }  

    void Update()
    {
        
    }

    string CommaText(float Score) { return string.Format("{0:#,###}", Score); }
}
