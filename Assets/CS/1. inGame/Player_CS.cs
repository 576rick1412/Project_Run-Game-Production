using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_CS : MonoBehaviour
{
    public static Player_CS PL;

        public static bool Player_alive;
    float jumpHeight;
     bool Jumping;
     bool DoubleJumping;
     bool Sliding;
     bool OnSlide; // ���߿��� �����̵� ��ư ������ ��

     bool IsFloor = false; // �ٴ� Ȯ��
     bool IsPlatform = false; // �÷��� Ȯ��

    [SerializeField] GameObject AttackObject;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D[] colliders;
    Animator anime;


    bool HIT_check = false; // �ڷ�ƾ �ݺ� ������
    public static bool On_HIT = false; // �ǰ� Ȯ�ο�

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
        AnimeControl();
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
        if (Player_alive) anime.SetInteger    ("Player_Value", 4);
    }
    public void Jump()
    {
        if (Player_alive == false)
        {
            IsFloor = false; IsPlatform = false;
            if (Jumping == false && DoubleJumping == false) { rigid.velocity = Vector2.up * jumpHeight; Jumping = true; return; }
            if (Jumping == true && DoubleJumping == false) { rigid.velocity = Vector2.up * jumpHeight; DoubleJumping = true; return; }
        }
    }
    public void Slide_DAWN() 
    {
        if (Player_alive == false)
        {
            // �ٴ��̳� ���ǿ� ���� °�� �����̵� ��ư�� ���� �� �����̴� �ִϸ��̼��� ��������
            if ((Sliding == false && IsFloor) || (Sliding == false && IsPlatform)) Sliding = true;

            // ���������� �� ��ư�� ������ �ٴڿ� ���ڸ��� �����̵� �ϵ���
            if ((Sliding == false && !IsFloor) || (Sliding == false && IsPlatform)) OnSlide = true;
        }
    }
    public void Slide_UP()
    {
        if (Player_alive == false)
        {
            IsFloor = true; IsPlatform = true;

            // �����̵� �� ���� // ���߿��� �����̵� ��ư ���� ����
            Sliding = false; OnSlide = false;
            
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