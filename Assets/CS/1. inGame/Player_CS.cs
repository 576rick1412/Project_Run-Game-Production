using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

public class Player_CS : MonoBehaviour
{
    public static Player_CS PL;
    public Transform Player_Pos;
    [HideInInspector] public bool Clear_Check;

    float jumpHeight;
    bool Jumping;
    bool DoubleJumping;
    bool Sliding;

    bool IsFloor = false; // 바닥 확인

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D[] colliders;
    [HideInInspector] public Animator anime;

    public bool On_HIT = false; // 피격 확인용
    bool inhit; // 내부 피격
    void Awake() { PL = this; }
    void Start()
    {
        Jumping = false;
        DoubleJumping = false;
        Sliding = false;

        Player_Pos = this.gameObject.transform;
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

        if (Clear_Check == false) GameManager.GM.Data.LifeScore -= Time.deltaTime * 2.8f;
    }

    public void OnCoroutine()
    {
        inhit = true;
        GameManager.GM.Data.Floor_SpeedValue *= 0.7f;
        GameManager.GM.Data.BGI_SpeedValue *= 0.7f;
        Invoke("HIT_off", GameManager.GM.Data.Invincibility_Time);
        StartCoroutine("HIT_Coroutine");
    }

    void SetCollider()
    {
        if (GameManager.GM.Player_alive == false)
        {
            switch (Sliding)
            {
                case true: // 슬라이드 중
                    colliders[0].enabled = false;
                    colliders[1].enabled = true;
                    break;
                case false: // 슬라이드 끝
                    colliders[0].enabled = true;
                    colliders[1].enabled = false;
                    break;
            }
        }
    }
    void AnimeControl()
    {
        // 애니메이션이 씹히는 현상이 발생해서 리턴 들어있는거
        if (GameManager.GM.Player_alive) { anime.SetInteger("Player_Value", 4); return; }
        if (inhit) { anime.SetInteger("Player_Value", 5); Invoke("Hit_Speed", 0.1f); return; }

        if (Sliding) { anime.SetInteger             ("Player_Value", 1); return; }
        if (DoubleJumping) { anime.SetInteger       ("Player_Value", 3); return; }
        if (Jumping) { anime.SetInteger             ("Player_Value", 2); return; }

        if (IsFloor) anime.SetInteger("Player_Value", 0);
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
            IsFloor = false;
            if (Jumping == false && DoubleJumping == false) { rigid.velocity = Vector2.up * jumpHeight; Jumping = true; return; }
            if (Jumping == true && DoubleJumping == false) { rigid.velocity = Vector2.up * jumpHeight; DoubleJumping = true; return; }
        }
    }
    public void Slide_DAWN() { if (GameManager.GM.Player_alive == false) { Sliding = true; SetCollider(); } }
    public void Slide_UP() { if (GameManager.GM.Player_alive == false) { Sliding = false; SetCollider(); } }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GM.Player_alive == false)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                IsFloor = true;
                Jumping = false;
                DoubleJumping = false;
            }
        }
    }
    
    IEnumerator HIT_Coroutine()
    {
        if (GameManager.GM.Player_alive == false)
        {
            StartCoroutine(Game_Control.GC.ShowBloodScreen());
            StartCoroutine(Game_Control.GC.CameraShake(0.5f));
            Handheld.Vibrate(); // 모바일 진동 기능

            for (int i = 0; i < GameManager.GM.Data.Invincibility_Time * 10; i++)
            {
                if (i % 2 == 0) spriteRenderer.color = new Color32(255, 255, 255, 90);
                else spriteRenderer.color = new Color32(255, 255, 255, 180);

                yield return new WaitForSeconds(0.1f);
            }
            spriteRenderer.color = new Color32(255, 255, 255, 255);
            yield return null;
        }
    }
    void HIT_off() { On_HIT = false; }
}