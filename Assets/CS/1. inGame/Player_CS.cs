using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_CS : MonoBehaviour
{
    bool GameOvercheck = false;
    bool HIT_check = false;
    [SerializeField] bool isJump = false;
    [SerializeField] bool isDoubleJump = false;
    [SerializeField] public static bool On_HIT = false;
    [SerializeField] float jumpHeight;
    static float MAX_LifeScore;
    [SerializeField] Image HP_Bar;
    public TextMeshProUGUI Score;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        GameManager.GM.CoinScore = 0;
        MAX_LifeScore = GameManager.GM.LifeScore;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpHeight = GameManager.GM.PlayerJumpValue;
    }

    void Update()
    {
        if(HIT_check == false)
        {
            if (On_HIT == true) 
            {
                HIT_check = true;
                StartCoroutine("HIT_Coroutine");
            }
        }

        HP_Bar.fillAmount = (GameManager.GM.LifeScore / MAX_LifeScore);
        Score.text = "점수 : " + (GameManager.GM.CoinScore == 0 ? 0 : CommaText(GameManager.GM.CoinScore).ToString());
        //Score.text = "점수 : " + GameManager.GM.CoinScore;
        if (GameManager.GM.LifeScore <= 0 && GameOvercheck == false)
        {
            Debug.Log("게임 오버");
            GameOvercheck = true;
        }
    }
    public void Jump()
    {
        if (isJump == true)
        {
            rigid.velocity = Vector2.up * jumpHeight;
            isJump = false;
            return;
        }

        if(isJump == false && isDoubleJump == true)
        {
            rigid.velocity = Vector2.up * jumpHeight;
            isDoubleJump = false;
            return;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isJump = true;
            isDoubleJump = true;
        }
    }
    public string CommaText(long Sccore) 
    { 
        return string.Format("{0:#,###}", Sccore);
    }
    IEnumerator HIT_Coroutine()
    {
        for (int i = 0; i < GameManager.GM.Invincibility_Time * 10; i++)
        {
            if (i % 2 == 0) spriteRenderer.color = new Color32(255, 255, 255, 90);
            else spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);
        yield return null;
    }
}
