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

    public Transform BossEntryPos;

    [Header("일시정지 버튼")]
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

        Pause_Image.SetActive(false);
        IsPause = false;
        GameManager.GM.Data.LifeScore = GameManager.GM.Data.Set_LifeScore;
        GameManager.GM.Data.CoinScore = 0;
        GameManager.GM.Player_alive = false;


        Game_End = false;
        Boss_On = false;
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
        Score.text = "점수 : " + (GameManager.GM.Data.CoinScore == 0 ? 0 : CommaText(GameManager.GM.Data.CoinScore).ToString());

        if (GameManager.GM.Data.LifeScore <= 0 && GameOvercheck == false && Game_End == false) 
        { GameManager.GM.Data.Game_WIN = false; Game_OverUI(); GameManager.GM.SavaData(); Invoke("Game_Result", 3f); Game_End = true; }
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

    public void Result_Spawn() { Invoke("Game_Result", 1f); }
    void Game_Result() { GameManager.GM.Fade(Result,true); Player_CS.PL.Clear_Check = true; }
    public void Game_ClearUI() 
    {
        GameObject clear = Instantiate(ClearUI, BossEntryPos.position, Quaternion.identity);
        clear.transform.SetParent(BossEntryPos);
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
