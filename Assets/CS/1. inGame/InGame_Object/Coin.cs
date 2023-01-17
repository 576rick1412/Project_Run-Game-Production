using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] string setObject;
    bool magnet;
    Vector2 speed;

    void Start()
    { 
        magnet = false;
        speed = new Vector2(30f, 20f);

    }

    void Update() 
    {
        Vector2 Player_Pos = Player.PL.playerPos.position;
        if (magnet) { transform.position = Vector2.SmoothDamp(transform.position, Player_Pos, ref speed, 0.1f); return; }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnet_Borber")) { if (setObject == "obstacle") return; magnet = true; }

        // Coin_Base는 코인이 플레이어 뒤로 가서 안 지워지는 버그 땜빵 / 플레이어 뒤에 콜라이더를 만들어 해결함
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Coin_Base"))
        {
            magnet = false;
            switch (setObject)
            {
                case "Nomal_Ice":       GetCoin(1, 1);  Destroy(); break;
                case "Hard_Ice":        GetCoin(2, 5);  Destroy(); break;
                case "Special_Ice":     GetCoin(2, 50); Destroy(); break;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
            if (setObject == "obstacle" && !Player.PL.onHit) Obstacle();
    }

    private void Destroy() { Destroy(this.gameObject); }
    void GetCoin(int point, int multiply)
    {
        // 코인 획득 시 내부 변수 활성화
        magnet = false;

        // 코인 획득 시 생명력 증가
        GameManager.GM.data.lifeScore += point;
        if (GameManager.GM.data.lifeScore >= GameManager.GM.data.setLifeScore)
        { GameManager.GM.data.lifeScore = GameManager.GM.data.setLifeScore; }

        // 코인 획득 시 점수 증가
        GameManager.GM.data.coinScore += GameManager.GM.data.coinPoint * multiply;
        Sound_Manager.SM.Coin();
    }
    void Obstacle()
    {
        // 피격 시 2의 피해를 입고, 전체 체력이 영구적으로 1 줄어듬
        GameManager.GM.data.lifeScore -= GameManager.GM.data.obstacleDamage;
        GameManager.GM.data.setLifeScore -= 1f;
        Player.PL.onHit = true;
        Player.PL.OnCoroutine();
    }
}
