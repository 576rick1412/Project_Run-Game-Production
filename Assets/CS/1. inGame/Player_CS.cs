using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_CS : MonoBehaviour
{
    [SerializeField] bool isJump = false;
    [SerializeField] bool isDoubleJump = false;
    [SerializeField] float jumpHeight;
    static float MAX_LifeScore;
    [SerializeField] Image HP_Bar;
    Rigidbody2D rigid;
    void Start()
    {
        MAX_LifeScore = GameManager.GM.LifeScore;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        jumpHeight = GameManager.GM.PlayerJumpValue;
    }

    void Update()
    {
        HP_Bar.fillAmount = (GameManager.GM.LifeScore / MAX_LifeScore);
        if (GameManager.GM.LifeScore <= 0)
        {
            Debug.Log("게임 오버");
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
}
