using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Control : MonoBehaviour
{
    public static Game_Control GC;

    [SerializeField] Transform Spawn_Pos;
    [SerializeField] GameObject[] Player;
    [SerializeField] GameObject SpanwPlayer;

    [SerializeField] Image HP_Bar;
    [SerializeField] GameObject Pause_Image;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Run_Ratio;

    [SerializeField] Transform BossEntryPos;

    [Header("�Ͻ����� ��ư")]
    public GameObject start_Icon;
    public GameObject pause_Icon;

    [Header("UI")]
    [HideInInspector] public bool Game_End;
    [SerializeField] Image BloodScreen;
    [SerializeField] GameObject Result;
    [SerializeField] GameObject ClearUI;
    [SerializeField] GameObject OverUI;

    [Header("�ǰ� ȿ��")]
    [SerializeField] float SetAlphaValue;
    [SerializeField] float SetEfeectTime;
    Vector3 CamPos;
    // ������
    int branch;
    [SerializeField] RunGame_EX RunGame_EX;
    string Stage_Des;
    bool IsPause; // �Ͻ�����
    void Awake()
    {
        GC = this;
        PlayerType();

        start_Icon.SetActive(false); pause_Icon.SetActive(true);
        branch = Random.Range(1, RunGame_EX.StartSheet.Count + 1); STG_Excel();

        Pause_Image.SetActive(false);
        IsPause = false;

        GameManager.GM.Data.Cur_Run_Ratio = 0; // ���� �޸� �Ÿ� �ʱ�ȭ

        GameManager.GM.Data.Floor_SpeedValue = GameManager.GM.Data.Set_Floor_SpeedValue; // ���� �ӵ� ������ �ʱⰪ���� �ʱ�ȭ
        GameManager.GM.Data.BGI_SpeedValue = GameManager.GM.Data.Set_BGI_SpeedValue;     // ��� �ӵ� ������ �ʱⰪ���� �ʱ�ȭ

        GameManager.GM.Data.LifeScore = GameManager.GM.Data.Set_LifeScore;  // HP ������ �ʱⰪ���� �ʱ�ȭ
        GameManager.GM.Data.CoinScore = 0;  // ���� �� ���� �ʱ�ȭ

        GameManager.GM.Player_alive = false; // �÷��̾� ���ó�� �ʱ�ȭ
       
        Game_End = false;

        CamPos = Camera.main.transform.position; // ���� ī�޶� ��ġ�� �־� �ʱ�ȭ
    }

    void PlayerType()
    {
        switch (GameManager.GM.Data.PlayerType)
        {
            case "Player_1": SpanwPlayer = Player[0]; break;
        }

        Instantiate(SpanwPlayer, Spawn_Pos.position, Quaternion.identity);
    }

    void Update()
    {

        HP_Bar.fillAmount = (GameManager.GM.Data.LifeScore / GameManager.GM.Data.Set_LifeScore);
        Score.text = "���� : " + (GameManager.GM.Data.CoinScore == 0 ? 0 : CommaText(GameManager.GM.Data.CoinScore).ToString());

        Run_Ratio.text = "�޸��Ÿ� " + System.Math.Truncate(GameManager.GM.Data.Cur_Run_Ratio / GameManager.GM.Data.Run_Ratio * 100).ToString() + " %";

        if (GameManager.GM.Data.LifeScore <= 0 && Game_End == false) StartCoroutine(EndGame(false));
    }
    public string CommaText(long Sccore) { return string.Format("{0:#,###}", Sccore); }

    public void Pause_B()
    {
        /*�Ͻ�����?��Ȱ��ȭ*/
        if (IsPause == false)
        {
            Time.timeScale = 0;
            Pause_Image.SetActive(true);
            start_Icon.SetActive(true); pause_Icon.SetActive(false);
            IsPause = true; return;
        }

        /*�Ͻ�����?Ȱ��ȭ*/
        if (IsPause == true)
        {
            Time.timeScale = 1;
            Pause_Image.SetActive(false);
            start_Icon.SetActive(false); pause_Icon.SetActive(true);
            IsPause = false; return;
        }
    }

    public IEnumerator EndGame(bool GameValue)
    {
        GameObject END_UI = GameValue == true ? ClearUI : OverUI; // ������ ��� UI ����

        // ��� UI ����
        GameObject UI = Instantiate(END_UI, BossEntryPos.position, Quaternion.identity);
        UI.transform.SetParent(BossEntryPos);

        Game_End = true;
        GameManager.GM.Data.Game_WIN = GameValue;
        GameManager.GM.SavaData();
       
        yield return new WaitForSeconds(3f);
        GameManager.GM.Fade(Result, true); Player_CS.PL.Clear_Check = true;
        yield return null;
    }


    public void ReStage()
    {
        Time.timeScale = 1; //  Ÿ�� ������ �ʱ�ȭ, ���� ���� ����
        switch (GameManager.GM.Data.GM_branch / 10)
        {
            case <= 10: Loading_Manager.LoadScene("Stage1_Hub", Stage_Des); break;
            case <= 20: Loading_Manager.LoadScene("Stage2_Hub", Stage_Des); break;
            case <= 30: Loading_Manager.LoadScene("Stage3_Hub", Stage_Des); break;
            case <= 40: Loading_Manager.LoadScene("Stage4_Hub", Stage_Des); break;
            case <= 50: Loading_Manager.LoadScene("Stage5_Hub", Stage_Des); break;
            case <= 60: Loading_Manager.LoadScene("Stage6_Hub", Stage_Des); break;
        }
    }
    public void GoRetry()
    {
        Time.timeScale = 1; //  Ÿ�� ������ �ʱ�ȭ, ���� ���� ����

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
    public IEnumerator CameraShake(float magnitude)
    {
        for (int i = 0;i < 10;i++)
        {
            Camera.main.transform.localPosition = Random.insideUnitSphere * magnitude + CamPos;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Camera.main.transform.localPosition = CamPos; // ���� ī�޶� ��ġ ����
        yield return null;
    }

    public IEnumerator ShowBloodScreen()
    {
        BloodScreen.color = new Color(1, 0, 0, SetAlphaValue);
        yield return new WaitForSeconds(SetEfeectTime);
        BloodScreen.color = Color.clear;
        yield return null;
    }
}
