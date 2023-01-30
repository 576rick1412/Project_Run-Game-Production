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
        public TextMeshProUGUI rewardText;      // ����Ʈ ���� �ؽ�Ʈ

        public float questPoint;                // ����Ʈ �޼� ��ǥ ����

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
        public Panel panel;                     // ����Ʈ ���� ǥ��
    }

    [Serializable]
    public class QuestDB
    {
        // AES ��ȣȭ Ű
        [HideInInspector] public string 
            key = "sfaugb!@@Tgrts+d65ghsal";

        // ����Ʈ ��ϻ��� ���� / �Ϸ� �Ѿ�� ����Ʈ �ʱ�ȭ
        public int recordedTime = DateTime.Today.Day;

        public float[] curPointQuestDB = new float[6];  // ���� ����Ʈ �޼� ����
        public Check[] checkQuestDB = new Check[6];     // ����Ʈ ���� �۵� ���� (bool)
    }

    public Quest[] quest;                       // ����Ʈ ����ü (����)
    public QuestDB questDB;                     // ����Ʈ ���� ���� (����)

    /*
     *  0. ��ֹ��� �浹���� �ʰ� ~~ �� �޼�
     *  1. �� ���ӿ��� ���� ~~ȸ �޼�
     *  2. �� ���ӿ��� �������� ~~ȸ ����
     *  3. �� ���ӿ��� �����̵� ~~ȸ �޼�
     *  4. ���� ~~ȸ �÷���
     *  5. ���� ����Ʈ ���� Ŭ����
     */

    string filePath; // ���� ���
    void Awake() 
    { 
        QM = this; 
        filePath = Application.persistentDataPath + "/QuestDB.txt";
        
        LoadData();

        // ���ڰ� �Ѿ�� ����Ʈ �ʱ�ȭ
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
            // ����Ʈ ��ü �ؽ�Ʈ
            quest[i].content.questDesText.text = quest[i].content.questDesFort
                    + " <color=yellow>" + CommaText(quest[i].point.questPoint) + "</color> "
                    + quest[i].content.questDesBack;

            if (quest[i].point.isFree) quest[i].point.rewardText.text =
                    "���� : ���� " + CommaText(quest[i].point.reward) + " ���";
            else quest[i].point.rewardText.text =
                    "���� : ������ȭ " + CommaText(quest[i].point.reward) + " ĳ��";

            quest[i].attain.PointBar.fillAmount = (questDB.curPointQuestDB[i] / quest[i].point.questPoint);

            quest[i].attain.PointText.text =
                "�޼��� : " + CommaText(questDB.curPointQuestDB[i]) + " / " + CommaText(quest[i].point.questPoint);

            // ����Ʈ�� �޼����� ��� ����Ʈ Ŭ���� ���·� ����
            if (questDB.curPointQuestDB[i] >= quest[i].point.questPoint && !questDB.checkQuestDB[i].isRewardClear)
                questDB.checkQuestDB[i].isClear = true;

            // ����Ʈ �ǳ� - Ŭ���� Ȱ��ȭ
            if (questDB.checkQuestDB[i].isClear) 
            { 
                quest[i].panel.questClear.SetActive(true); 
                quest[i].panel.questEnd.SetActive(false); 
                continue; 
            }
            // // ����Ʈ �ǳ� - ������� Ȱ��ȭ
            else if (questDB.checkQuestDB[i].isRewardClear)
            {
                quest[i].panel.questClear.SetActive(false);
                quest[i].panel.questEnd.SetActive(true);

                SavaData(); // ���� �޶����Ƿ� ����
                continue;
            }
            else // ����Ʈ �ǳ� - ���� ��Ȱ��ȭ
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

        // ���� ����Ʈ ���� �Ϸ� �� �ʿ���
        if (i != 5) questDB.curPointQuestDB[5]++;
    }

    public void SavaData()
    {
        string key = questDB.key;
        var save = JsonUtility.ToJson(questDB);

        save = Program.Encrypt(save, key);
        File.WriteAllText(filePath, save);
    }   // Json ����
    public void LoadData()
    {
        if (!File.Exists(filePath)) { ResetMainDB(); return; }

        string key = questDB.key;
        var load = File.ReadAllText(filePath);

        load = Program.Decrypt(load, key);
        questDB = JsonUtility.FromJson<QuestDB>(load);
    }   // Json �ε�
    void ResetMainDB()
    {
        questDB = new QuestDB();

        // ���� ����Ʈ �޼��� �ʱ�ȭ
        questDB.curPointQuestDB = new float[6];
        for(int i = 0; i < questDB.curPointQuestDB.Length; i++)
            questDB.curPointQuestDB[i] = 0;

        // ���� Ŭ����, ���� ���� �ʱ�ȭ
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
