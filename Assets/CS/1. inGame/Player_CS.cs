using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_CS : MonoBehaviour
{
    public static Player_CS PL;

    [SerializeField] bool Jumping;
    [SerializeField] bool DoubleJumping;
    [SerializeField] bool Sliding;

    [SerializeField] bool �ٴںپ����� = false; // �ٴ� Ȯ��
    [SerializeField] bool ���Ǻپ����� = false; // �÷��� Ȯ��

    [SerializeField] GameObject AttackObject;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D[] colliders;
    Animator anime;
    /*
    [SerializeField] bool Foor_check = false; // �ٴ� Ȯ��
    [SerializeField] bool Platform_check = false; // �÷��� Ȯ��
    */
    public static bool Player_alive;
    bool HIT_check = false;

    float jumpHeight;

    // ========= �� ���ƹ��� �� ===================
    [SerializeField] bool isJump = false;
    [SerializeField] bool isDoubleJump = false;
    [SerializeField] bool isSlide = false; // �����̵� �ݶ��̴� ����

    bool OnSlide = false;   // �ٴڿ� �پ��ִ��� Ȯ��
    public static bool On_HIT = false;
    
    //public static bool Onalive;

    private IObjectPool<Attack_CS> AttackPool;
    void Awake()
    {
        PL = this; Player_alive = false;
        AttackPool = new ObjectPool<Attack_CS>(Attack_Creat, Attack_Get, Attack_Releas, Attack_Destroy, maxSize: 20);
    }

    private Attack_CS Attack_Creat()
    {
        Attack_CS Attack = Instantiate(AttackObject).GetComponent<Attack_CS>();
        Attack.Set_AttackPool(AttackPool);
        return Attack;
    }                    // (Ǯ��) ��ֹ� ����
    private void Attack_Get(Attack_CS Attack)
    {
        Attack.gameObject.SetActive(true);
    }           // (Ǯ��) ��ֹ� Ȱ��ȭ
    private void Attack_Releas(Attack_CS Attack)
    {
        Attack.gameObject.SetActive(false);
    }        // (Ǯ��) ��ֹ� ��Ȱ��ȭ
    private void Attack_Destroy(Attack_CS Attack)
    {
        Destroy(Attack.gameObject);
    }       // (Ǯ��) ��ֹ� ����


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
        SetCollider();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if(Input.GetKeyDown(KeyCode.A)) Attack();
        if (Input.GetKeyDown(KeyCode.LeftShift)) Slide_DAWN();
        if (Input.GetKeyUp(KeyCode.LeftShift)) Slide_UP();

        if (GameManager.GM.Data.LifeScore <= 0 && Player_alive == false)
        {
            GameManager.GM.Data.Floor_SpeedValue = 0;
            GameManager.GM.Data.BGI_SpeedValue = 0;
            Player_alive = true;
        }
        if(GameManager.GM.Data.Boss_DIE == false && Player_alive == false) GameManager.GM.Data.LifeScore -= Time.deltaTime * 2;
    }

    public void OnCoroutine() { if (HIT_check == false && On_HIT == true) { HIT_check = true; StartCoroutine("HIT_Coroutine"); } }

    void SetCollider()
    {
        if (Player_alive == false)
        {
            switch (isSlide)
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
    public void Jump()
    {
        if (Player_alive == false)
        {
            OnSlide = false;
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
    }

    public void Slide_DAWN()
    {
        if (Player_alive == false)
        {
            if (OnSlide == false) { isSlide = true; return; }
            isSlide = true;
            anime.SetInteger("Player_Value", 1);
        }
    }
    public void Slide_UP()
    {
        if (Player_alive == false)
        {
            if (OnSlide == false) { isSlide = false; return; }
            isSlide = false;
            anime.SetInteger("Player_Value", 0);
        }
    }
    public void Attack()
    {
        if (Player_alive == false)
        {
            var Attack = AttackPool.Get();
            Attack.transform.position = this.gameObject.transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player_alive == false)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {

            }

            if (collision.gameObject.CompareTag("Platform"))
            {

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Player_alive == false)
        {
            if (collision.gameObject.CompareTag("Floor")) 
            { 
                
            }

            if (collision.gameObject.CompareTag("Platform")) 
            { 

            }
        }
    }
    IEnumerator HIT_Coroutine()
    {
        if (Player_alive == false)
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
}