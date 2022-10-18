using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Game_Control : MonoBehaviour
{
    [SerializeField] Transform Spawn_Pos;
    [SerializeField] GameObject[] Player;

    [SerializeField] Image HP_Bar;
    [SerializeField] GameObject Pause_Image;
    public TextMeshProUGUI Score;
    bool GameOvercheck = false;
    bool IsPause; // �Ͻ�����
    void Awake()
    {
        Type();

        //GameManager.GM.Player = GameObject.FindWithTag("Player");
        Pause_Image.SetActive(false);
        IsPause = false;
        GameManager.GM.LifeScore = GameManager.GM.Set_LifeScore;
        GameManager.GM.CoinScore = 0;
        GameManager.GM.Boss_HP = 0;
    }

    void Type()
    {
        switch (GameManager.GM.PlayerType)
        {
            case "Player_1": Instantiate(Player[0], Spawn_Pos.position, Quaternion.identity); break;
        }
    }

    void Update()
    {
        GameManager.GM.LifeScore -= Time.deltaTime * 2;

        HP_Bar.fillAmount = (GameManager.GM.LifeScore / GameManager.GM.Set_LifeScore);
        Score.text = "���� : " + (GameManager.GM.CoinScore == 0 ? 0 : CommaText(GameManager.GM.CoinScore).ToString());

        if (GameManager.GM.LifeScore <= 0 && GameOvercheck == false)
        {
            Debug.Log("���� ����");
            GameOvercheck = true;
        }
    }
    public string CommaText(long Sccore)
    {
        return string.Format("{0:#,###}", Sccore);
    }

    public void Pause_B()
    {
        /*�Ͻ�����?Ȱ��ȭ*/
        if (IsPause == false)
        {
            Time.timeScale = 0;
            Pause_Image.SetActive(true);
            IsPause = true; return;
        }

        /*�Ͻ�����?��Ȱ��ȭ*/
        if (IsPause == true)
        {
            Pause_Image.SetActive(false);
            Time.timeScale = 1;
            IsPause = false; return;
        }
    }
}