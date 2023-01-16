using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Max_Score;
    [SerializeField] TextMeshProUGUI Cur_Score;
    [SerializeField] TextMeshProUGUI Cur_Window_GoUp;

    [SerializeField] GameObject Score_Window;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;

    [SerializeField] GameObject[] Star = new GameObject[3];
    [SerializeField] GameObject[] Success_icon = new GameObject[3];
    [SerializeField] GameObject[] Failed_icon = new GameObject[3];
    [SerializeField] TextMeshProUGUI[] per_Point = new TextMeshProUGUI[3];

    // 엑셀용
    int branch;
    [SerializeField] RunGame_EX RunGame_EX;
    string Stage_Des;
    void Awake()
    {
        Debug.Log("게임결과창 " + GameManager.GM.nowClearStar);

        Time.timeScale = 1;
        branch = Random.Range(1, RunGame_EX.StartSheet.Count + 1); STG_Excel();

        if (GameManager.GM.data.gameWin == false) { Win.SetActive(false); } else { Lose.SetActive(false); }

        if (GameManager.GM.nowClearStar == 0) GameManager.GM.data.coinScore = 0;
        StartCoroutine(OnStar(GameManager.GM.nowClearStar)); // 클리어 시 별이 뜨게하는 코루틴 호줄

        Max_Score.text = "최대 점수 : " + (GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM] == 0 ? 0 : 
            CommaText(GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM]).ToString());

        Cur_Score.text = "현재 점수 : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString());
        Change_Score();
    }
    IEnumerator OnStar(int Clear_Num) 
    {
        {
            // 0 : 실패
            // 1 : 1성 클리어
            // 2 : 2성 클리어
            // 3 : 3성 클리어

        } // 사용 요약
 
        // 서브 별 뜨우기
        for (int i = 1; i < Success_icon.Length + 1; i++)
        {
            if (i <= Clear_Num) { Success_icon[i - 1].SetActive(true); continue; }
            else { Failed_icon[i - 1].SetActive(true); }
        }

        // 별 획득 점수
        for (int i = 0; i < per_Point.Length; i++) per_Point[i].text = CommaText(GameManager.GM.percentStars[i]).ToString();

        // 메인 별 띄우기
        for (int i = 0; i < Clear_Num; i++)
        {
            Star[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }

        yield return null;
    } // 클리어 시 별이 뜨도록 함
    void Update()
    {

    }
    string CommaText(long Score) { return string.Format("{0:#,###}", Score); }

    void Change_Score()
    {
        if(GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM] < GameManager.GM.data.coinScore)
        {
            GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM] = GameManager.GM.data.coinScore;
            GameManager.GM.SavaData(); Debug.Log("신속저장!");

            Debug.Log("신기록 달성!!!!");
            Invoke("NewRecord", 1f);
        }
    }
    void NewRecord()
    {
        Destroy(Max_Score);
        TextMeshProUGUI GoUp =  Instantiate(Cur_Window_GoUp, Cur_Score.transform.position,Quaternion.identity);
        GoUp.transform.SetParent(Score_Window.transform);
        GoUp.text = "최대 기록 : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString());
        GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM] = GameManager.GM.data.coinScore;
    } // 최고점 기록 시 최고점 갱신 + 갱신 애니메이션

    public void GoLoby() { Loading_Manager.LoadScene("Mian_Loby_Scene", Stage_Des); }
    public void GoStage() 
    {
        switch (GameManager.GM.data.branch_GM / 10)
        {
            case <= 10 : Loading_Manager.LoadScene("Stage1_Hub", Stage_Des); break;
            case <= 20 : Loading_Manager.LoadScene("Stage2_Hub", Stage_Des); break;
            case <= 30 : Loading_Manager.LoadScene("Stage3_Hub", Stage_Des); break;
            case <= 40 : Loading_Manager.LoadScene("Stage4_Hub", Stage_Des); break;
            case <= 50 : Loading_Manager.LoadScene("Stage5_Hub", Stage_Des); break;
            case <= 60 : Loading_Manager.LoadScene("Stage6_Hub", Stage_Des); break;
        }
    }
    public void GoRetry()
    {
        Time.timeScale = 1;
        GameManager.GM.Stage_Move();
    }
    void STG_Excel()
    {
        for (int i = 0; i < RunGame_EX.StartSheet.Count; ++i)
        {
            if (RunGame_EX.StartSheet[i].STR_branch == branch)
            {
                Stage_Des = RunGame_EX.StartSheet[i].STR_description;
            }
        }
    }
}
