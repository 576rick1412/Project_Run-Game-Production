using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

using System.IO;
using AesEncryptionNS.Con;

public class QuestManager : MonoBehaviour
{
    public static QuestManager QM;

    [Serializable]
    public struct Point
    {
        public TextMeshProUGUI rewardText;      // 퀴스트 보상 텍스트

        public float questPoint;                // 퀘스트 달성 목표 점수

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
        public Panel panel;                     // 퀘스트 상태 표시
    }

    [Serializable]
    public class QuestDB
    {
        // AES 암호화 키
        [HideInInspector] public string 
            key = "sfaugb!@@Tgrts+d65ghsal";

        // 퀘스트 기록상의 날자 / 하루 넘어가면 퀘스트 초기화
        public int recordedTime = DateTime.Today.Day;

        public float[] curPointQuestDB = new float[6];  // 현재 퀘스트 달성 점수
        public Check[] checkQuestDB = new Check[6];     // 퀘스트 내부 작동 변수 (bool)
    }

    public Quest[] quest;                       // 퀘스트 구조체 (메인)
    public QuestDB questDB;                     // 퀘스트 내용 저장 (서브)

    /*
     *  0. 장애물에 충돌하지 않고 ~~ 점 달성
     *  1. 한 게임에서 점프 ~~회 달성
     *  2. 한 게임에서 더블점프 ~~회 달정
     *  3. 한 게임에서 슬라이드 ~~회 달성
     *  4. 게임 ~~회 플레이
     *  5. 일일 퀘스트 전부 클리어
     */

    string filePath; // 저장 경로
    void Awake() 
    { 
        QM = this; 
        filePath = Application.persistentDataPath + "/QuestDB.txt";
        
        LoadData();

        // 날자가 넘어가면 퀘스트 초기화
        if (questDB.recordedTime != DateTime.Today.Day)
        {
            questDB = new QuestDB();
            questDB.recordedTime = DateTime.Today.Day;
        }

        SavaData();
    }

    void Start()
    {

    }

    void Update()
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

            quest[i].attain.PointBar.fillAmount = (questDB.curPointQuestDB[i] / quest[i].point.questPoint);

            quest[i].attain.PointText.text =
                "달성률 : " + CommaText(questDB.curPointQuestDB[i]) + " / " + CommaText(quest[i].point.questPoint);

            // 퀘스트를 달성했을 경우 퀘스트 클리어 상태로 변경
            if (questDB.curPointQuestDB[i] >= quest[i].point.questPoint && !questDB.checkQuestDB[i].isRewardClear)
                questDB.checkQuestDB[i].isClear = true;

            // 퀘스트 판넬 - 클리어 활성화
            if (questDB.checkQuestDB[i].isClear) 
            { 
                quest[i].panel.questClear.SetActive(true); 
                quest[i].panel.questEnd.SetActive(false); 
                continue; 
            }
            // // 퀘스트 판넬 - 보상받음 활성화
            else if (questDB.checkQuestDB[i].isRewardClear)
            {
                quest[i].panel.questClear.SetActive(false);
                quest[i].panel.questEnd.SetActive(true);

                SavaData(); // 값이 달라지므로 저장
                continue;
            }
            else // 퀘스트 판넬 - 전부 비활성화
            {
                quest[i].panel.questClear.SetActive(false);
                quest[i].panel.questEnd.SetActive(false);
            }
        }
    }

    public void DestroyWindow() { Destroy(this.gameObject); }

    public void ClearButton(int i) 
    {
        if (quest[i].point.isFree) GameManager.GM.data.money_GM += quest[i].point.reward;
        else GameManager.GM.data.goods_GM += quest[i].point.reward;

        questDB.checkQuestDB[i].isClear = false;
        questDB.checkQuestDB[i].isRewardClear = true;

        // 일일 퀘스트 전부 완료 시 필요함
        if (i != 5) questDB.curPointQuestDB[5]++;
    }

    public void SavaData()
    {
        string key = questDB.key;
        var save = JsonUtility.ToJson(questDB);

        save = Program.Encrypt(save, key);
        File.WriteAllText(filePath, save);
    }   // Json 저장
    public void LoadData()
    {
        if (!File.Exists(filePath)) { ResetMainDB(); return; }

        string key = questDB.key;
        var load = File.ReadAllText(filePath);

        load = Program.Decrypt(load, key);
        questDB = JsonUtility.FromJson<QuestDB>(load);
    }   // Json 로딩
    void ResetMainDB()
    {
        questDB = new QuestDB();

        // 현재 퀘스트 달성률 초기화
        questDB.curPointQuestDB = new float[6];
        for(int i = 0; i < questDB.curPointQuestDB.Length; i++)
            questDB.curPointQuestDB[i] = 0;

        // 현재 클리어, 보상 여부 초기화
        questDB.checkQuestDB = new Check[6];
        for (int i = 0; i < questDB.checkQuestDB.Length; i++)
        {
            questDB.checkQuestDB[i].isClear = false;
            questDB.checkQuestDB[i].isRewardClear = false;
        }
    }

    string CommaText(float Score) 
    {
        if (Score <= 0) return "0";

        return string.Format("{0:#,###}", Score); 
    }
}
