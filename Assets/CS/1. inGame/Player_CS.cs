using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_CS : MonoBehaviour
{
    bool HIT_check = false;
    bool Foor_check = false;
    bool isJump = false;
    bool isDoubleJump = false;
    bool isSlide = false; // 슬라이드 콜라이더 조정
    bool OnSlide = false;   // 바닥에 붙어있는지 확인
    public static bool On_HIT = false;
    float jumpHeight;
    [SerializeField] GameObject AttackObject;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D[] colliders;
    Animator anime;

    private IObjectPool<Attack_CS> AttackPool;
    void Awake() { AttackPool = new ObjectPool<Attack_CS>(Attack_Creat, Attack_Get, Attack_Releas, Attack_Destroy, maxSize: 20); }

    private Attack_CS Attack_Creat()
    {
        Attack_CS Attack = Instantiate(AttackObject).GetComponent<Attack_CS>();
        Attack.Set_AttackPool(AttackPool);
        return Attack;
    }                       // (풀링) 장애물 생성
    private void Attack_Get(Attack_CS Attack)
    {
        Attack.gameObject.SetActive(true);
    }           // (풀링) 장애물 활성화
    private void Attack_Releas(Attack_CS Attack)
    {
        Attack.gameObject.SetActive(false);
    }        // (풀링) 장애물 비활성화
    private void Attack_Destroy(Attack_CS Attack)
    {
        Destroy(Attack.gameObject);
    }       // (풀링) 장애물 삭제


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
        switch (isSlide)
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

        if (isJump == false && isDoubleJump == true)
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
    public void Attack()
    {
        var Attack = AttackPool.Get();
        Attack.transform.position = this.gameObject.transform.position;
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
