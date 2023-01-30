using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Result : MonoBehaviour
{
    [Header("���Ӹ�� UI")]
    [SerializeField] GameObject normalUI;
    [SerializeField] GameObject hardUI;


    [Header("�ְ����� �ؽ�Ʈ")]
    [SerializeField] TextMeshProUGUI Max_Score;
    [SerializeField] TextMeshProUGUI Cur_Score;
    [SerializeField] TextMeshProUGUI Cur_Window_GoUp;

    [SerializeField] GameObject Score_Window;

    [Header("���� ī��Ʈ �ؽ�Ʈ")]
    public TextMeshProUGUI normalCoinCountText;
    public TextMeshProUGUI hardCoinCountText;
    public TextMeshProUGUI specialCoinCountText;
    public TextMeshProUGUI obstacleCollisionCountText;
    public TextMeshProUGUI RunRatioText;

    [Header("���� ����")]
    public int rewardMult; // ���� ����
    public TextMeshProUGUI rewardText;
    void Awake()
    {
        Time.timeScale = 1;

        // ���Ӹ�� UI ����
        if (GameManager.GM.isNormal) normalUI.SetActive(true);
        else hardUI.SetActive(true);

        // ���� UI ����
        int maxScore = GameManager.GM.isNormal ? GameManager.GM.data.nomalMaxScore : GameManager.GM.data.hardMaxScore;
        Max_Score.text = "�ְ����� : " + (maxScore == 0 ? 0 : CommaText(maxScore).ToString()) + " ��";

        Cur_Score.text = "�������� : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString()) + " ��";

        // ���� ȹ�� ī��Ʈ
        normalCoinCountText.text = "<color=yellow>" + GameManager.GM.normalCoinCount + "</color> ��";
        hardCoinCountText.text = "<color=yellow>" + GameManager.GM.hardCoinCount + "</color> ��";
        specialCoinCountText.text = "<color=yellow>" + GameManager.GM.specialCoinCount + "</color> ��";
        obstacleCollisionCountText.text = "<color=yellow>" + GameManager.GM.obstacleCollisionCount + "</color> ȸ";
        RunRatioText.text = "<color=yellow>" + Mathf.RoundToInt(GameManager.GM.runTime) + "</color> ��";

        // ���� ���� / �ϵ����� ��� ���� 2�� 
        int reward = GameManager.GM.isNormal ? GameManager.GM.data.coinScore / rewardMult : GameManager.GM.data.coinScore / rewardMult * 2;

        rewardText.text = "< ���� : ����" + "<color=yellow> " + reward + "</color> " + "�� >";
        GameManager.GM.data.money_GM += reward;

        Change_Score();
    }

    void Update()
    {

    }
    string CommaText(long Score) { return string.Format("{0:#,###}", Score); }

    void Change_Score()
    {
        if (GameManager.GM.data.coinScore <= GameManager.GM.data.nomalMaxScore) return;
        if (GameManager.GM.data.coinScore <= GameManager.GM.data.hardMaxScore)  return;

        Invoke("NewRecord", 1f);
        if (GameManager.GM.isNormal) GameManager.GM.data.nomalMaxScore = GameManager.GM.data.coinScore;
        else GameManager.GM.data.hardMaxScore = GameManager.GM.data.coinScore;
        GameManager.GM.SavaData();
    }
    void NewRecord()
    {
        Destroy(Max_Score);
        TextMeshProUGUI GoUp =  Instantiate(Cur_Window_GoUp, Cur_Score.transform.position,Quaternion.identity);
        GoUp.transform.SetParent(Score_Window.transform);

        GoUp.text = "�ְ����� : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString()) + " ��";
    } // �ְ��� ��� �� �ְ��� ���� + ���� �ִϸ��̼�

    public void GoLoby() 
    {
        string stageDes = GameManager.GM.STG_Excel();
        Loading_Manager.LoadScene("Mian_Loby_Scene", stageDes); 
    }
    public void GoRetry()
    {
        Time.timeScale = 1;
        string stageDes = GameManager.GM.STG_Excel();

        // ���� �÷��� Ƚ�� ī���� + ����
        if (QuestManager.QM.questDB.curPointQuestDB[4] < QuestManager.QM.quest[4].point.questPoint)
        { QuestManager.QM.questDB.curPointQuestDB[4]++; QuestManager.QM.SavaData(); }

        if (GameManager.GM.isNormal) GameManager.GM.Stage_Move("GameScene", "�Ϲݸ��", stageDes);
        else GameManager.GM.Stage_Move("GameScene", "�ϵ���", stageDes);
    }
}
