using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_CS : MonoBehaviour
{
    bool HIT_check = false;
    bool Foor_check = false;
    bool isJump = false;
    bool isDoubleJump = false;
    bool isSlide = false; // 슬라이드 콜라이더 조정
    bool OnSlide = false;   // 바닥에 붙어있는지 확인
    public static bool On_HIT = false;
    [SerializeField] float jumpHeight;

    
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D[] colliders;
    Animator anime;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.colliders = GetComponents<BoxCollider2D>();
        anime = GetComponent<Animator>();
        jumpHeight = GameManager.GM.PlayerJumpValue;
    }

    void Update()
    {
        SetCollider();

        if (HIT_check == false && On_HIT == true)
        {
            HIT_check = true;
            StartCoroutine("HIT_Coroutine");
        }
    }
    
    void SetCollider()
    {
        switch(isSlide)
        {
            case true: // 슬라이드 중
                colliders[0].enabled = false;
                colliders[1].enabled = true;
                break;
            case false: // 슬라이드 끝
                colliders[0].enabled = true;
                colliders[1].enabled = false; break;
        }
    }
    public void Jump()
    {
        OnSlide = false;
        Foor_check = true;
        if (isJump == true)
        {
            rigid.velocity = Vector2.up * jumpHeight;
            isJump = false;
            anime.SetInteger("Player_Value", 2);
            return;
        }

        if(isJump == false && isDoubleJump == true)
        {
            rigid.velocity = Vector2.up * jumpHeight;
            isDoubleJump = false;
            anime.SetInteger("Player_Value", 3);
            return;
        }
    }

    public void Slide_DAWN()
    {
        if (OnSlide == false) return;
        isSlide = true;
        anime.SetInteger("Player_Value", 1);
    }
    public void Slide_UP()
    {
        if (OnSlide == false) return;
        isSlide = false;
        anime.SetInteger("Player_Value", 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (Foor_check == true)
            {
                anime.SetInteger("Player_Value", 0);
                Foor_check = false;
            }
            isJump = true;
            isDoubleJump = true;
            OnSlide = true;
        }
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
        HIT_check = false;
        yield return null;
    }
}
