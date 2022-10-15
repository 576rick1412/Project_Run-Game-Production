using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Game_Control : MonoBehaviour
{
    [SerializeField] Image HP_Bar;
    [SerializeField] GameObject Pause_Image;
    public TextMeshProUGUI Score;
    bool GameOvercheck = false;
    bool IsPause; // 일시정지
    void Start()
    {
        //GameManager.GM.Player = GameObject.FindWithTag("Player");
        Pause_Image.SetActive(false);
        IsPause = false;
        GameManager.GM.LifeScore = GameManager.GM.Set_LifeScore;
        GameManager.GM.CoinScore = 0;
    }

    void Update()
    {
        GameManager.GM.LifeScore -= Time.deltaTime * 2;

        HP_Bar.fillAmount = (GameManager.GM.LifeScore / GameManager.GM.Set_LifeScore);
        Score.text = "점수 : " + (GameManager.GM.CoinScore == 0 ? 0 : CommaText(GameManager.GM.CoinScore).ToString());

        if (GameManager.GM.LifeScore <= 0 && GameOvercheck == false)
        {
            Debug.Log("게임 오버");
            GameOvercheck = true;
        }
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
}