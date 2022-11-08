using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_CS : MonoBehaviour
{
    public static Player_CS PL;

     [HideInInspector]public bool Clear_Check;

     float jumpHeight;
     bool Jumping;
     bool DoubleJumping;
     bool Sliding;
     bool OnSlide; // ���߿��� �����̵� ��ư ������ ��

     bool IsFloor = false; // �ٴ� Ȯ��
     bool IsPlatform = false; // �÷��� Ȯ��

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D[] colliders;
    [HideInInspector] public Animator anime;


    bool HIT_check = false; // �ڷ�ƾ �ݺ� ������
    public bool On_HIT = false; // �ǰ� Ȯ�ο�
    bool inhit; // ���� �ǰ�

    void Start()
    {
        Jumping = false;
        DoubleJumping = false;
        Sliding = false;

        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.colliders = GetComponents<BoxCollider2D>();
        anime = GetComponent<Animator>();
        jumpHeight = GameManager.GM.Data.PlayerJumpValue;
    }

    void Update()
    {

        AnimeControl();
        if (GameManager.GM.Data.LifeScore <= 0 && GameManager.GM.Player_alive == false)
        {
            GameManager.GM.Data.Floor_SpeedValue = 0;
            GameManager.GM.Data.BGI_SpeedValue = 0;
            GameManager.GM.Player_alive = true;
        }

        //if(GameManager.GM.Data.Boss_DIE == false || Clear_Check == false) GameManager.GM.Data.LifeScore -= Time.deltaTime * 2;
        if(Clear_Check == false) GameManager.GM.Data.LifeScore -= Time.deltaTime * 2;
    }

    public void OnCoroutine() { if (HIT_check == false && On_HIT == true) // �ǰ� �ڷ�ƾ ȣ�� �Լ�
        { 
            inhit = true;
            GameManager.GM.Data.Floor_SpeedValue *= 0.9f;
            GameManager.GM.Data.BGI_SpeedValue *= 0.9f;
            HIT_check = true;
            Invoke("HIT_off", GameManager.GM.Data.Invincibility_Time);
            StartCoroutine("HIT_Coroutine"); 
        } 
    }

    void SetCollider()
    {
        if (GameManager.GM.Player_alive == false)
        {
            switch (Sliding)
            {
                case true: // �����̵� ��
                    colliders[0].enabled = false;
                    colliders[1].enabled = true;
                    break;
                case false: // �����̵� ��
                    colliders[0].enabled = true;
                    colliders[1].enabled = false;
                    break;
            }
        }
    }
    void AnimeControl()
    {
        if ((IsFloor && !Sliding) || (IsPlatform && !Sliding))
            anime.SetInteger                  ("Player_Value", 0);
        if (Sliding) anime.SetInteger         ("Player_Value", 1); SetCollider();
        if (Jumping) anime.SetInteger         ("Player_Value", 2);
        if (DoubleJumping) anime.SetInteger   ("Player_Value", 3);
        if (GameManager.GM.Player_alive) anime.SetInteger    ("Player_Value", 4);
        if (inhit) { anime.SetInteger         ("Player_Value", 5); Invoke("Hit_Speed", 0.1f); }
    }
    void Hit_Speed()
    {
        inhit = false;
        if (!GameManager.GM.Player_alive)
        {
            GameManager.GM.Data.Floor_SpeedValue = GameManager.GM.Data.Set_Floor_SpeedValue;
            GameManager.GM.Data.BGI_SpeedValue = GameManager.GM.Data.Set_BGI_SpeedValue;
        }
    }
    public void Jump()
    {
        if (GameManager.GM.Player_alive == false)
        {
            IsFloor = false; IsPlatform = false;
            if (Jumping == false && DoubleJumping == false) { rigid.velocity = Vector2.up * jumpHeight; Jumping = true; return; }
            if (Jumping == true && DoubleJumping == false) { rigid.velocity = Vector2.up * jumpHeight; DoubleJumping = true; return; }
        }
    }
    public void Slide_DAWN() 
    {
        if (GameManager.GM.Player_alive == false)
        {
            // �ٴ��̳� ���ǿ� ���� °�� �����̵� ��ư�� ���� �� �����̴� �ִϸ��̼��� ��������
            if ((Sliding == false && IsFloor) || (Sliding == false && IsPlatform)) Sliding = true;

            // ���������� �� ��ư�� ������ �ٴڿ� ���ڸ��� �����̵� �ϵ���
            if ((Sliding == false && !IsFloor) || (Sliding == false && IsPlatform)) OnSlide = true;
        }
    }
    public void Slide_UP()
    {
        if (GameManager.GM.Player_alive == false)
        {
            IsFloor = true; IsPlatform = true;

            // �����̵� �� ���� // ���߿��� �����̵� ��ư ���� ����
            Sliding = false; OnSlide = false;
            
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GM.Player_alive == false)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                if (OnSlide) { Sliding = true; OnSlide = false; }
                else { IsFloor = true; IsPlatform = true; }

                Jumping = false;
                DoubleJumping = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
    IEnumerator HIT_Coroutine()
    {
        if (GameManager.GM.Player_alive == false)
        {
            for (int i = 0; i < GameManager.GM.Data.Invincibility_Time * 10; i++)
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
    void HIT_off() { On_HIT = false; }
}