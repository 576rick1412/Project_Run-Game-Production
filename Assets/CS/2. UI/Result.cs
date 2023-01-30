using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Result : MonoBehaviour
{
    [Header("게임모드 UI")]
    [SerializeField] GameObject normalUI;
    [SerializeField] GameObject hardUI;


    [Header("최고점수 텍스트")]
    [SerializeField] TextMeshProUGUI Max_Score;
    [SerializeField] TextMeshProUGUI Cur_Score;
    [SerializeField] TextMeshProUGUI Cur_Window_GoUp;

    [SerializeField] GameObject Score_Window;

    [Header("게임 카운트 텍스트")]
    public TextMeshProUGUI normalCoinCountText;
    public TextMeshProUGUI hardCoinCountText;
    public TextMeshProUGUI specialCoinCountText;
    public TextMeshProUGUI obstacleCollisionCountText;
    public TextMeshProUGUI RunRatioText;

    [Header("게임 보상")]
    public int rewardMult; // 보상 기준
    public TextMeshProUGUI rewardText;
    void Awake()
    {
        Time.timeScale = 1;

        // 게임모드 UI 띄우기
        if (GameManager.GM.isNormal) normalUI.SetActive(true);
        else hardUI.SetActive(true);

        // 점수 UI 띄우기
        int maxScore = GameManager.GM.isNormal ? GameManager.GM.data.nomalMaxScore : GameManager.GM.data.hardMaxScore;
        Max_Score.text = "최고점수 : " + (maxScore == 0 ? 0 : CommaText(maxScore).ToString()) + " 점";

        Cur_Score.text = "현재점수 : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString()) + " 점";

        // 게임 획득 카운트
        normalCoinCountText.text = "<color=yellow>" + GameManager.GM.normalCoinCount + "</color> 개";
        hardCoinCountText.text = "<color=yellow>" + GameManager.GM.hardCoinCount + "</color> 개";
        specialCoinCountText.text = "<color=yellow>" + GameManager.GM.specialCoinCount + "</color> 개";
        obstacleCollisionCountText.text = "<color=yellow>" + GameManager.GM.obstacleCollisionCount + "</color> 회";
        RunRatioText.text = "<color=yellow>" + Mathf.RoundToInt(GameManager.GM.runTime) + "</color> 초";

        // 게임 보상 / 하드모드일 경우 보상 2배 
        int reward = GameManager.GM.isNormal ? GameManager.GM.data.coinScore / rewardMult : GameManager.GM.data.coinScore / rewardMult * 2;

        rewardText.text = "< 보상 : 코인" + "<color=yellow> " + reward + "</color> " + "개 >";
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

        GoUp.text = "최고점수 : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString()) + " 점";
    } // 최고점 기록 시 최고점 갱신 + 갱신 애니메이션

    public void GoLoby() 
    {
        string stageDes = GameManager.GM.STG_Excel();
        Loading_Manager.LoadScene("Mian_Loby_Scene", stageDes); 
    }
    public void GoRetry()
    {
        Time.timeScale = 1;
        string stageDes = GameManager.GM.STG_Excel();

        // 게임 플레이 횟수 카운터 + 저장
        if (QuestManager.QM.questDB.curPointQuestDB[4] < QuestManager.QM.quest[4].point.questPoint)
        { QuestManager.QM.questDB.curPointQuestDB[4]++; QuestManager.QM.SavaData(); }

        if (GameManager.GM.isNormal) GameManager.GM.Stage_Move("GameScene", "일반모드", stageDes);
        else GameManager.GM.Stage_Move("GameScene", "하드모드", stageDes);
    }
}
