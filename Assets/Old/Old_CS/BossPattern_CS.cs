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
        //PatternDamage = GameManager.GM.Data.Boss_Damage;
        Destroy(this.gameObject, 10f);
        
    }

    void Update()
    {
        //if (GameManager.GM.Data.Boss_HP <= 0 || (GameManager.GM.Player_alive == true && Player_CS.PL.On_HIT == false)) Destroy(DesObj.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("players") && Player.PL.onHit == false)
        {
            GameManager.GM.data.lifeScore -= PatternDamage;
            Player.PL.onHit = true;
            Player.PL.OnCoroutine();
            Invoke("HIT_off", GameManager.GM.data.invincibilityTime);
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
        Player.PL.onHit = false;
        Debug.Log("무적 종료");
    }
}
