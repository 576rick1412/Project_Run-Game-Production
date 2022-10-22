using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern_CS : MonoBehaviour
{
    [SerializeField] private string Type;
    [SerializeField] private GameObject DesObj;
    private int PatternDamage;
    Rigidbody2D rb;

    void Start()
    {
        PatternDamage = GameManager.GM.Boss_Damage;
        Destroy(this.gameObject, 10f);
    }

    void Update()
    {
        if (GameManager.GM.Boss_HP <= 0) Destroy(DesObj.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Player_CS.On_HIT == false)
        {
            GameManager.GM.LifeScore -= PatternDamage;
            Player_CS.On_HIT = true;
            if (Type == "Bounce") Destroy(this.gameObject);
        }

        // 수직공격 : 물체에 닿으면 지워지도록
        if (Type == "Vertical" ) Destroy(this.gameObject);
    }

    void Bounce()
    {
        float speed = 10f;
        rb = GetComponent<Rigidbody2D>();

        Vector2 shot = new Vector2(-1, 1).normalized;
        rb.velocity = shot * speed;
    }

    void HIT_off()
    {
        Player_CS.On_HIT = false;
        Debug.Log("무적 종료");
    }
}
