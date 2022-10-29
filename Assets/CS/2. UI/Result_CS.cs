using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Result_CS : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Max_Score;
    [SerializeField] TextMeshProUGUI Cur_Score;
    [SerializeField] TextMeshProUGUI Cur_Window_GoUp;

    [SerializeField] GameObject Score_Window;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;

    // 엑셀용
    int branch;
    [SerializeField] RunGame_EX RunGame_EX;
    [SerializeField] string Stage_Des;
    void Awake()
    {
        branch = Random.Range(1, RunGame_EX.StartSheet.Count + 1); STG_Excel();

        if (GameManager.GM.Game_Fail == false) Lose.SetActive(false);
        else { Win.SetActive(false); }

        Max_Score.text = "최대 점수 : " + (GameManager.GM.stage_Max_Score[GameManager.GM.GM_branch] == 0 ? 0 : 
            CommaText(GameManager.GM.stage_Max_Score[GameManager.GM.GM_branch]).ToString());

        Cur_Score.text = "현재 점수 : " + (GameManager.GM.CoinScore == 0 ? 0 : CommaText(GameManager.GM.CoinScore).ToString());

        Change_Score();
    }
    void Update()
    {

    }
    string CommaText(long Sccore)
    {
        return string.Format("{0:#,###}", Sccore);
    }

    void Change_Score()
    {
        if(GameManager.GM.stage_Max_Score[GameManager.GM.GM_branch] < GameManager.GM.CoinScore)
        {
            Debug.Log("신기록 달성!!!!");
            Invoke("NewRecord", 2f);
        }
    }
    void NewRecord()
    {
        Destroy(Max_Score);
        TextMeshProUGUI GoUp =  Instantiate(Cur_Window_GoUp, Cur_Score.transform.position,Quaternion.identity);
        GoUp.transform.SetParent(Score_Window.transform);
        GoUp.text = "최대 점수 : " + (GameManager.GM.CoinScore == 0 ? 0 : CommaText(GameManager.GM.CoinScore).ToString());
        GameManager.GM.stage_Max_Score[GameManager.GM.GM_branch] = GameManager.GM.CoinScore;
    }

    public void GoLoby() { Loading_Manager.LoadScene("Mian_Loby_Scene", Stage_Des); }
    public void GoStage() 
    { 
        switch (GameManager.GM.GM_branch)
        {
            case <= 10: Loading_Manager.LoadScene("Stage1_Hub", Stage_Des); break;
            case <= 20: Loading_Manager.LoadScene("Stage2_Hub", Stage_Des); break;
            case <= 30: Loading_Manager.LoadScene("Stage3_Hub", Stage_Des); break;
            case <= 40: Loading_Manager.LoadScene("Stage4_Hub", Stage_Des); break;
            case <= 50: Loading_Manager.LoadScene("Stage5_Hub", Stage_Des); break;
            case <= 60: Loading_Manager.LoadScene("Stage6_Hub", Stage_Des); break;
        }
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
