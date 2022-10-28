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

    public GameObject BossEntry;
    public Transform BossEntryPos;
    public GameObject Boss;

    public bool BossAttack;
    [SerializeField] GameObject A_button;

    [Header("UI")]
    public GameObject ClearUI;
    [SerializeField] GameObject OverUI;

    bool GameOvercheck = false;
    bool IsPause; // 일시정지
    void Awake()
    {
        GC = this;
        PlayerType();

        ClearUI.SetActive(false);
        OverUI.SetActive(false);

        A_button.SetActive(false);
        Pause_Image.SetActive(false);
        IsPause = false;
        GameManager.GM.LifeScore = GameManager.GM.Set_LifeScore;
        GameManager.GM.CoinScore = 0;
        GameManager.GM.Boss_HP = 0;

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
        if (Input.GetKeyDown(KeyCode.F)) { BossHub(); BossAttack = true; }

        switch (BossAttack)
        {
            case true: A_button.SetActive(true); break;
            case false: A_button.SetActive(false); break;  
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
            IsPause = true; return;
        }

        /*일시정지?비활성화*/
        if (IsPause == true)
        {
            Pause_Image.SetActive(false);
            Time.timeScale = 1;
            IsPause = false; return;
        }
    }

    public void BossHub() { StartCoroutine(BossEnyryCoroutine()); }
    IEnumerator BossEnyryCoroutine()
    {
        for(int i = 0; i < 20; i++)
        {
            BossEnyryF();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(3f);
        SpawnBoss();
        yield return null;
    }
    void BossEnyryF()
    {
        GameObject Entry = Instantiate(BossEntry, BossEntryPos.position, Quaternion.identity);
        Entry.transform.SetParent(BossEntryPos.transform);
    }

    void SpawnBoss()
    {
        Instantiate(Boss);
    }
}
