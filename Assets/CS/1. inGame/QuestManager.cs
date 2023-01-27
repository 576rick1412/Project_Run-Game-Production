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
        public TextMeshProUGUI rewardText;      // 퀴스트 보상 텍스트

        public float questPoint;                // 퀘스트 달성 목표 점수
        public float curPoint;                  // 현재 퀘스트 달성 점수

        public int reward;                      // 퀘스트 보상 내용
        public bool isFree;                     // 보상 내용 여부, 무료재화 or 유로재화
    }

    [Serializable]
    public struct Content
    {
        public TextMeshProUGUI questDesText;    // 퀘스트 내용 전체 텍스트

        public string questDesFort;             // 퀘스트 앞 내용 텍스트
        public string questDesBack;             // 퀘스트 뒤 내용 텍스트
    }

    [Serializable]
    public struct Attain
    {
        public Image PointBar;                  // 퀘스트 달성률 바
        public TextMeshProUGUI PointText;       // 퀴스트 달성률 텍스트
    }

    [Serializable]
    public struct Check
    {
        public bool isClear;                    // 퀘스트 클리어 여부
        public bool isRewardClear;              // 퀘스트 보상 획득 여부
    }

    [Serializable]
    public struct Panel
    {
        public GameObject questClear;           // 퀘스트 클리어 버튼
        public GameObject questEnd;             // 퀘스트 보상 받은 이후 가림판
    }

    [Serializable]
    public struct Quest
    {
        public Content content;                 // 퀘스트 내용 텍스트
        public Point point;                     // 퀘스트 목표,보상,보상종류 설정
        public Attain attain;                   // 퀘스트 달성률
        public Check check;                     // 퀘스트 내부 작동 변수 (bool)
        public Panel panel;                     // 퀘스트 상태 표시
    }
    public Quest[] quest;

    /*
     *  0. 장애물에 충돌하지 않고 ~~ 점 달성
     *  1. 한 게임에서 점프 ~~회 달성
     *  2. 한 게임에서 더블점프 ~~회 달정
     *  3. 한 게임에서 슬라이드 ~~회 달성
     *  5. 게임 ~~회 플레이
     *  6. 일일 퀘스트 전부 클리어
     */
    void Start()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            // 퀘스트 전체 텍스트
            quest[i].content.questDesText.text = quest[i].content.questDesFort
                    + " <color=yellow>" + CommaText(quest[i].point.questPoint) + "</color> "
                    + quest[i].content.questDesBack;

            if (quest[i].point.isFree) quest[i].point.rewardText.text =
                    "보상 : 코인 " + CommaText(quest[i].point.reward) + " 골드";
            else quest[i].point.rewardText.text =
                    "보상 : 유료재화 " + CommaText(quest[i].point.reward) + " 캐시";

            quest[i].attain.PointBar.fillAmount = (quest[i].point.curPoint / quest[i].point.questPoint);

            quest[i].attain.PointText.text =
                "달성률 : " + CommaText(quest[i].point.curPoint) + " / " + CommaText(quest[i].point.questPoint);

            // 퀘스트 판넬
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