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

    // ������
    int branch;
    [SerializeField] RunGame_EX RunGame_EX;
    string Stage_Des;
    void Awake()
    {
        Debug.Log("���Ӱ��â " + GameManager.GM.nowClearStar);

        Time.timeScale = 1;
        branch = Random.Range(1, RunGame_EX.StartSheet.Count + 1); STG_Excel();

        if (GameManager.GM.data.gameWin == false) { Win.SetActive(false); } else { Lose.SetActive(false); }

        if (GameManager.GM.nowClearStar == 0) GameManager.GM.data.coinScore = 0;
        StartCoroutine(OnStar(GameManager.GM.nowClearStar)); // Ŭ���� �� ���� �߰��ϴ� �ڷ�ƾ ȣ��

        Max_Score.text = "�ִ� ���� : " + (GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM] == 0 ? 0 : 
            CommaText(GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM]).ToString());

        Cur_Score.text = "���� ���� : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString());
        Change_Score();
    }
    IEnumerator OnStar(int Clear_Num) 
    {
        {
            // 0 : ����
            // 1 : 1�� Ŭ����
            // 2 : 2�� Ŭ����
            // 3 : 3�� Ŭ����

        } // ��� ���
 
        // ���� �� �߿��
        for (int i = 1; i < Success_icon.Length + 1; i++)
        {
            if (i <= Clear_Num) { Success_icon[i - 1].SetActive(true); continue; }
            else { Failed_icon[i - 1].SetActive(true); }
        }

        // �� ȹ�� ����
        for (int i = 0; i < per_Point.Length; i++) per_Point[i].text = CommaText(GameManager.GM.percentStars[i]).ToString();

        // ���� �� ����
        for (int i = 0; i < Clear_Num; i++)
        {
            Star[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }

        yield return null;
    } // Ŭ���� �� ���� �ߵ��� ��
    void Update()
    {

    }
    string CommaText(long Score) { return string.Format("{0:#,###}", Score); }

    void Change_Score()
    {
        if(GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM] < GameManager.GM.data.coinScore)
        {
            GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM] = GameManager.GM.data.coinScore;
            GameManager.GM.SavaData(); Debug.Log("�ż�����!");

            Debug.Log("�ű�� �޼�!!!!");
            Invoke("NewRecord", 1f);
        }
    }
    void NewRecord()
    {
        Destroy(Max_Score);
        TextMeshProUGUI GoUp =  Instantiate(Cur_Window_GoUp, Cur_Score.transform.position,Quaternion.identity);
        GoUp.transform.SetParent(Score_Window.transform);
        GoUp.text = "�ִ� ��� : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString());
        GameManager.GM.data.stageMaxScores[GameManager.GM.data.branch_GM] = GameManager.GM.data.coinScore;
    } // �ְ��� ��� �� �ְ��� ���� + ���� �ִϸ��̼�

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
