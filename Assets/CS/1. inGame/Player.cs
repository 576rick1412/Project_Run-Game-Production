using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    public static Player PL;
    public Transform playerPos;
    [HideInInspector] public bool clearCheck;

    float jumpHeight;
    bool isJump;
    bool isDoubleJump;
    bool isSlid;

    bool isFloor = false; // 바닥 확인

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D[] colliders;
    [HideInInspector] public Animator anime;

    public bool onHit = false; // 피격 확인용
    bool inHit; // 내부 피격
    void Awake() { PL = this; }
    void Start()
    {
        isJump = false;
        isDoubleJump = false;
        isSlid = false;

        playerPos = this.gameObject.transform;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.colliders = GetComponents<BoxCollider2D>();
        anime = GetComponent<Animator>();
        jumpHeight = GameManager.GM.data.playerJumpValue;
    }

    void Update()
    {

        AnimeControl();
        if (GameManager.GM.data.lifeScore <= 0 && GameManager.GM.playerAlive == false)
        {
            GameManager.GM.data.floorSpeedValue = 0;
            GameManager.GM.data.BGSpeedValue = 0;
            GameManager.GM.playerAlive = true;
        }

        if (clearCheck == false) GameManager.GM.data.lifeScore -= Time.deltaTime * 2.8f;
    }

    public void OnCoroutine()
    {
        inHit = true;
        GameManager.GM.data.floorSpeedValue *= 0.7f;
        GameManager.GM.data.BGSpeedValue *= 0.7f;
        Invoke("HIT_off", GameManager.GM.data.invincibilityTime);
        StartCoroutine("HIT_Coroutine");
    }

    void SetCollider()
    {
        if (GameManager.GM.playerAlive == false)
        {
            switch (isSlid)
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
        if (GameManager.GM.playerAlive) { anime.SetInteger("Player_Value", 4); return; }
        if (inHit) { anime.SetInteger("Player_Value", 5); Invoke("Hit_Speed", 0.1f); return; }

        if (isSlid) { anime.SetInteger             ("Player_Value", 1); return; }
        if (isDoubleJump) { anime.SetInteger       ("Player_Value", 3); return; }
        if (isJump) { anime.SetInteger             ("Player_Value", 2); return; }

        if (isFloor) anime.SetInteger("Player_Value", 0);
    }
    void Hit_Speed()
    {
        inHit = false;
        if (!GameManager.GM.playerAlive)
        {
            GameManager.GM.data.floorSpeedValue = GameManager.GM.data.setFloorSpeedValue;
            GameManager.GM.data.BGSpeedValue = GameManager.GM.data.setBGSpeedValue;
        }
    }
    public void Jump()
    {
        if (GameManager.GM.playerAlive == false)
        {
            isFloor = false;
            if (isJump == false && isDoubleJump == false) { rigid.velocity = Vector2.up * jumpHeight; isJump = true; return; }
            if (isJump == true && isDoubleJump == false) { rigid.velocity = Vector2.up * jumpHeight; isDoubleJump = true; return; }
        }
    }
    public void Slide_DAWN() { if (GameManager.GM.playerAlive == false) { isSlid = true; SetCollider(); } }
    public void Slide_UP() { if (GameManager.GM.playerAlive == false) { isSlid = false; SetCollider(); } }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GM.playerAlive == false)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                isFloor = true;
                isJump = false;
                isDoubleJump = false;
            }
        }
    }
    
    IEnumerator HIT_Coroutine()
    {
        if (GameManager.GM.playerAlive == false)
        {
            StartCoroutine(Game_Control.GC.ShowBloodScreen());
            StartCoroutine(Game_Control.GC.CameraShake(0.5f));
            Handheld.Vibrate(); // 모바일 진동 기능

            for (int i = 0; i < GameManager.GM.data.invincibilityTime * 10; i++)
            {
                if (i % 2 == 0) spriteRenderer.color = new Color32(255, 255, 255, 90);
                else spriteRenderer.color = new Color32(255, 255, 255, 180);

                yield return new WaitForSeconds(0.1f);
            }
            spriteRenderer.color = new Color32(255, 255, 255, 255);
            yield return null;
        }
    }
    void HIT_off() { onHit = false; }
}