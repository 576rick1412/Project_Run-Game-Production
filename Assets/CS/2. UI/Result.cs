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


    void Awake()
    {
        Time.timeScale = 1;

        Max_Score.text = "최대 점수 : " + (GameManager.GM.data.nomalMaxScore == 0 ? 0 : 
            CommaText(GameManager.GM.data.nomalMaxScore).ToString());

        Cur_Score.text = "현재 점수 : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString());
        Change_Score();
    }

    void Update()
    {

    }
    string CommaText(long Score) { return string.Format("{0:#,###}", Score); }

    void Change_Score()
    {
        if (GameManager.GM.data.coinScore <= GameManager.GM.data.nomalMaxScore ||
            GameManager.GM.data.coinScore <= GameManager.GM.data.hardMaxScore) return;

        if (GameManager.GM.isNormal) GameManager.GM.data.nomalMaxScore = GameManager.GM.data.coinScore;
        else GameManager.GM.data.hardMaxScore = GameManager.GM.data.coinScore;

        GameManager.GM.SavaData();
        Invoke("NewRecord", 1f);
    }
    void NewRecord()
    {
        Destroy(Max_Score);
        TextMeshProUGUI GoUp =  Instantiate(Cur_Window_GoUp, Cur_Score.transform.position,Quaternion.identity);
        GoUp.transform.SetParent(Score_Window.transform);
        GoUp.text = "최대 기록 : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString());
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
        GameManager.GM.Stage_Move("GameScene", "무한모드", stageDes);
    }
}
