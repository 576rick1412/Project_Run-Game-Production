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

        if (GameManager.GM.isNormal) GameManager.GM.Stage_Move("GameScene", "�Ϲݸ��", stageDes);
        else GameManager.GM.Stage_Move("GameScene", "�ϵ���", stageDes);
    }
}
