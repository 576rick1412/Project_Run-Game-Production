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

    void Start()
    {
        Max_Score.text = "최대 점수 : " + (GameManager.GM.stage_Max_Score[GameManager.GM.GM_branch] == 0 ? 0 : 
            CommaText(GameManager.GM.stage_Max_Score[GameManager.GM.GM_branch]).ToString());

        Cur_Score.text = "현재 점수 : " + (GameManager.GM.CoinScore == 0 ? 0 : CommaText(GameManager.GM.CoinScore).ToString());

        Change_Score();
    }
    void Update()
    {

    }
    public string CommaText(long Sccore)
    {
        return string.Format("{0:#,###}", Sccore);
    }

    public void Change_Score()
    {
        if(GameManager.GM.stage_Max_Score[GameManager.GM.GM_branch] < GameManager.GM.CoinScore)
        {
            Debug.Log("신기록 달성!!!!");
            Invoke("NewRecord", 2f);
        }
    }
    public void NewRecord()
    {
        Destroy(Max_Score);
        TextMeshProUGUI GoUp =  Instantiate(Cur_Window_GoUp, Cur_Score.transform.position,Quaternion.identity);
        GoUp.transform.SetParent(Score_Window.transform);
        GoUp.text = "최대 점수 : " + (GameManager.GM.CoinScore == 0 ? 0 : CommaText(GameManager.GM.CoinScore).ToString());
        GameManager.GM.stage_Max_Score[GameManager.GM.GM_branch] = (int)GameManager.GM.CoinScore;
    }

}
