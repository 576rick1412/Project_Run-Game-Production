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
    public GameObject ClearUI;
    [SerializeField] GameObject OverUI;

    bool GameOvercheck = false;
    bool IsPause; // 일시정지
    void Awake()
    {
        GC = this;
        PlayerType();

        start_Icon.SetActive(false); pause_Icon.SetActive(true);

        ClearUI.SetActive(false);
        OverUI.SetActive(false);

        A_button.SetActive(false);
        Pause_Image.SetActive(false);
        IsPause = false;
        GameManager.GM.LifeScore = GameManager.GM.Set_LifeScore;
        GameManager.GM.CoinScore = 0;
        GameManager.GM.Boss_HP = 0;

        Boss_On = false;
        BossAttack = false;
    }

    void PlayerType()
    {
        switch (GameManager.GM.PlayerType)
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

        HP_Bar.fillAmount = (GameManager.GM.LifeScore / GameManager.GM.Set_LifeScore);
        Score.text = "점수 : " + (GameManager.GM.CoinScore == 0 ? 0 : CommaText(GameManager.GM.CoinScore).ToString());

        if (GameManager.GM.LifeScore <= 0 && GameOvercheck == false) { OverUI.SetActive(true); }
    }
    public string CommaText(long Sccore)
    {
        return string.Format("{0:#,###}", Sccore);
    }

    public void Pause_B()
    {
        /*일시정지?활성화*/
        if (IsPause == false)
        {
            Time.timeScale = 0;
            Pause_Image.SetActive(true);
            start_Icon.SetActive(true); pause_Icon.SetActive(false);
            IsPause = true; return;
        }

        /*일시정지?비활성화*/
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
        for (int i = 0; i < 20; i++)
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

    public void Stage1_Hub()
    {

    }
}
