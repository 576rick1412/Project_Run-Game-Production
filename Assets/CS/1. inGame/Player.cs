using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player PL;
    public Transform playerPos;
    [HideInInspector] public bool clearCheck;

    float setGravity;
    float jumpHeight;
    bool isJump;
    bool isDoubleJump;
    bool isSlid;

    bool isFloor = false; // �ٴ� Ȯ��

    // ����Ʈ ī����
    bool nonHit = true;
    public float[] questCount = { 0f, 0f, 0f, 0f };
    
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D[] colliders;
    [HideInInspector] public Animator anime;

    public bool onHit = false; // �ǰ� Ȯ�ο�
    bool inHit; // ���� �ǰ�
    void Awake() 
    { 
        PL = this;

        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.colliders = GetComponents<BoxCollider2D>();
        anime = GetComponent<Animator>();
    }

    void Start()
    {
        isJump = false;
        isDoubleJump = false;
        isSlid = false;

        setGravity =  rigid.gravityScale;
        playerPos = this.gameObject.transform;
        jumpHeight = GameManager.GM.data.playerJumpValue;
    }

    void Update()
    {
        QuestZero();

        AnimeControl();
        if (GameManager.GM.data.lifeScore <= 0 && GameManager.GM.playerAlive == false)
        {
            GameManager.GM.data.floorSpeedValue = 0;
            GameManager.GM.data.BGSpeedValue = 0;
            GameManager.GM.playerAlive = true;
        }

        // ���߿� Ű���� ���۹�
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKey(KeyCode.LeftShift)) Slide_DAWN();
        if (Input.GetKeyUp(KeyCode.LeftShift)) Slide_UP();

        if (clearCheck == false) GameManager.GM.data.lifeScore -= Time.deltaTime * 2.8f;

        // �÷��̾� ��� �� ����Ʈ ������ ����
        if (GameManager.GM.playerAlive)
        {
            for (int i = 0; i < 4; i++)
            {
                // ����Ʈ�� �̹� Ŭ���� ������ �� �ٷ� ��ȯ
                if (QuestManager.QM.questDB.checkQuestDB[i].isClear || QuestManager.QM.questDB.checkQuestDB[i].isRewardClear)
                    continue;

                // ���� ��� �������� ���� ������ �޼� �� ��� ���� ����
                if (questCount[i] > QuestManager.QM.questDB.curPointQuestDB[i])
                    QuestManager.QM.questDB.curPointQuestDB[i] = questCount[i];

                // ���� �޼����� ��ǥġ�� �ʰ����� ��� �޼����� ��ǥġ�� �ִ뿡 �°� ����
                if (QuestManager.QM.questDB.curPointQuestDB[i] > QuestManager.QM.quest[i].point.questPoint)
                    QuestManager.QM.questDB.curPointQuestDB[i] = QuestManager.QM.quest[i].point.questPoint;
            }
            QuestManager.QM.SavaData();
        }
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
                case true: // �����̵� ��
                    colliders[0].enabled = false;
                    colliders[1].enabled = true;

                    rigid.gravityScale = 40f;
                    CountSum(3);
                    break;
                case false: // �����̵� ��
                    colliders[0].enabled = true;
                    colliders[1].enabled = false;

                    rigid.gravityScale = setGravity;
                    break;
            }
        }
    }

    void AnimeControl()
    {
        // �ִϸ��̼��� ������ ������ �߻��ؼ� ���� ����ִ°�
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
            if (isJump == false && isDoubleJump == false) 
            {
                rigid.velocity = Vector2.up * jumpHeight;
                isJump = true;
                CountSum(1);
                return; 
            }
            if (isJump == true && isDoubleJump == false) 
            {
                rigid.velocity = Vector2.up * jumpHeight; 
                isDoubleJump = true;
                CountSum(2);
                return; 
            }
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
        nonHit = false;

        if (GameManager.GM.playerAlive == false)
        {
            StartCoroutine(Game_Control.GC.ShowBloodScreen());
            StartCoroutine(Game_Control.GC.CameraShake(0.5f));
            Handheld.Vibrate(); // ����� ���� ���

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

    void CountSum(int i)
    {
        // ����Ʈ�� �̹� Ŭ���� ������ �� �ٷ� ��ȯ
        if (QuestManager.QM.questDB.checkQuestDB[i].isClear || QuestManager.QM.questDB.checkQuestDB[i].isRewardClear)
            return;

        // ����Ʈ�� �������� �� ī��Ʈ 1 ���ؼ� ��ȯ
        questCount[i]++;
    }

    void QuestZero()
    {
        if (nonHit)
        {
            // ����Ʈ�� �̹� Ŭ���� ������ �� �ٷ� ��ȯ
            if (QuestManager.QM.questDB.checkQuestDB[0].isClear || QuestManager.QM.questDB.checkQuestDB[0].isRewardClear)
                return;

            // ���� ������ ��ǥ �������� ���� �� ���� ���� ����
            questCount[0] = GameManager.GM.data.coinScore;

        } // ����Ʈ 0��
    }
}
