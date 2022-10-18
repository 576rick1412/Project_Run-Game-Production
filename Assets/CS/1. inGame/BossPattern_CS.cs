using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern_CS : MonoBehaviour
{
    [SerializeField] private string Type;
    private int PatternDamage;
    Rigidbody2D rb;

    void Start()
    {
        switch (Type)
        {
            case "Lazer": PatternDamage = GameManager.GM.Boss_Lazer_Pattern; break;
            case "Vertical": PatternDamage = GameManager.GM.Get_Obstacle_Damage_2; break;
            case "Bounce": PatternDamage = GameManager.GM.Get_Obstacle_Damage_3; Invoke("Bounce", 2f); break;
        }
        Destroy(this.gameObject, 10f);
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Player_CS.On_HIT == false)
        {
            GameManager.GM.LifeScore -= PatternDamage;
            Player_CS.On_HIT = true;
            Invoke("HIT_off", GameManager.GM.Invincibility_Time);

            if (Type == "Bounce") Destroy(this.gameObject);
        }

        // 수직공격 물체에 닿으면 지워지도록
        if (Type == "Vertical") Destroy(this.gameObject);
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
