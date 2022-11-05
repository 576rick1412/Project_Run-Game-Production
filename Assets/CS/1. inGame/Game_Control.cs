using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Game_Control : MonoBehaviour
{
    public static Game_Control GC;

    [HideInInspector] public bool Boss_On;

    [SerializeField] Transform Spawn_Pos;
    [SerializeField] GameObject[] Player;
    [SerializeField] GameObject SpanwPlayer;

    [SerializeField] Image HP_Bar;
    [SerializeField] GameObject Pause_Image;
    public TextMeshProUGUI Score;

    public GameObject BossEntry;
    public Transform BossEntryPos;
    public GameObject Boss;

    public bool BossAttack;
    [SerializeField] GameObject A_button;
    [SerializeField] GameObject S_button;
    public GameObject start_Icon;
    public GameObject pause_Icon;

    [Header("UI")]
    public bool Game_End;
    [SerializeField] GameObject Result;
    public GameObject ClearUI;
    [SerializeField] GameObject OverUI;

    // 엑셀용
    int branch;
    [SerializeField] RunGame_EX RunGame_EX;
    string Stage_Des;

    bool GameOvercheck = false;
    bool IsPause; // 일시정지
    void Awake()
    {
        GC = this;
        PlayerType();

        start_Icon.SetActive(false); pause_Icon.SetActive(true);
        branch = Random.Range(1, RunGame_EX.StartSheet.Count + 1); STG_Excel();

        A_button.SetActive(false);
        Pause_Image.SetActive(false);
        IsPause = false;
        GameManager.GM.Data.LifeScore = GameManager.GM.Data.Set_LifeScore;
        GameManager.GM.Data.CoinScore = 0;
        GameManager.GM.Data.Boss_HP = 0;

        Game_End = false;
        Boss_On = false;
        BossAttack = false;
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
        if (Input.GetKeyDown(KeyCode.F)) { BossHub(); }
        if (Boss_On == true) { Invoke("BossHub", 5f); Boss_On = false; }

        switch (BossAttack)
        {
            case true: A_button.SetActive(true); S_button.SetActive(false); break;
            case false: A_button.SetActive(false); S_button.SetActive(true); break;
        }

        HP_Bar.fillAmount = (GameManager.GM.Data.LifeScore / GameManager.GM.Data.Set_LifeScore);
        Score.text = "점수 : " + (GameManager.GM.Data.CoinScore == 0 ? 0 : CommaText(GameManager.GM.Data.CoinScore).ToString());

        if (GameManager.GM.Data.LifeScore <= 0 && GameOvercheck == false && Game_End == false) 
        { GameManager.GM.Data.Game_Fail = false; Game_OverUI(); GameManager.GM.SavaData(); Result_Spawn(); Game_End = true; }
    }
    public string CommaText(long Sccore) { return string.Format("{0:#,###}", Sccore); }

    public void Pause_B()
    {
        /*일시정지?비활성화*/
        if (IsPause == false)
        {
            Time.timeScale = 0;
            Pause_Image.SetActive(true);
            start_Icon.SetActive(true); pause_Icon.SetActive(false);
            IsPause = true; return;
        }

        /*일시정지?활성화*/
        if (IsPause == true)
        {
            Time.timeScale = 1;
            Pause_Image.SetActive(false);
            start_Icon.SetActive(false); pause_Icon.SetActive(true);
            IsPause = false; return;
        }
    }

    public void BossHub() { StartCoroutine(BossEnyryCoroutine()); }
    IEnumerator BossEnyryCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            BossEnyryF();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(3f);
        Instantiate(Boss);
        BossAttack = true;
        Player_CS.PL.Slide_UP();
        yield return null;
    }
    void BossEnyryF()
    {
        GameObject Entry = Instantiate(BossEntry, BossEntryPos.position, Quaternion.identity);
        Entry.transform.SetParent(BossEntryPos.transform);
    }

    public void Result_Spawn() { Invoke("Game_Result", 3f); }
    void Game_Result() { Instantiate(Result); }
    public void Game_ClearUI() 
    {
        GameObject Over = Instantiate(ClearUI, BossEntryPos.position, Quaternion.identity);
        Over.transform.SetParent(BossEntryPos);
    }
    public void Game_OverUI() 
    {
        GameObject Over = Instantiate(OverUI, BossEntryPos.position, Quaternion.identity);
        Over.transform.SetParent(BossEntryPos);
    }
    public void ReStage()
    {
        Time.timeScale = 1;
        switch (GameManager.GM.nowStage)
        {
            case 1: Loading_Manager.LoadScene("Stage1_Hub", Stage_Des); break;
            case 2: Loading_Manager.LoadScene("Stage2_Hub", Stage_Des); break;
            case 3: Loading_Manager.LoadScene("Stage3_Hub", Stage_Des); break;
            case 4: Loading_Manager.LoadScene("Stage4_Hub", Stage_Des); break;
            case 5: Loading_Manager.LoadScene("Stage5_Hub", Stage_Des); break;
            case 6: Loading_Manager.LoadScene("Stage6_Hub", Stage_Des); break;
        }
    }
    public void GoRetry()
    {
        GameManager.GM.Data.Floor_SpeedValue = GameManager.GM.Data.Set_Floor_SpeedValue;
        GameManager.GM.Data.BGI_SpeedValue = GameManager.GM.Data.Set_BGI_SpeedValue;

        GameManager.GM.Data.CoinScore = 0;
        Time.timeScale = 1;

        switch (GameManager.GM.nowStage)
        {
            case 1: Loading_Manager.LoadScene("Stage1_Scene", Stage_Des); break;
            case 2: Loading_Manager.LoadScene("Stage2_Scene", Stage_Des); break;
            case 3: Loading_Manager.LoadScene("Stage3_Scene", Stage_Des); break;
            case 4: Loading_Manager.LoadScene("Stage4_Scene", Stage_Des); break;
            case 5: Loading_Manager.LoadScene("Stage5_Scene", Stage_Des); break;
            case 6: Loading_Manager.LoadScene("Stage6_Scene", Stage_Des); break;
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
