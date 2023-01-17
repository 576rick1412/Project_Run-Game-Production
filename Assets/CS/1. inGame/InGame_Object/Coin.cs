using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    [SerializeField] string setObject;
    [SerializeField] bool setPrefabs; // 프리팹 안에 있는 오브젝트라면 체크
    bool gameClear;
    bool magnet;
    bool onRelease;
    Vector2 speed = new Vector2(30f, 20f);

    private IObjectPool<Coin> coinPool_1;
    private IObjectPool<Coin> coinPool_2;
    private IObjectPool<Coin> coinPool_3;

    public void SetCoinPool_1(IObjectPool<Coin> pool_1) { coinPool_1 = pool_1; }
    public void SetCoinPool_2(IObjectPool<Coin> pool_2) { coinPool_2 = pool_2; }
    public void SetCoinPool_3(IObjectPool<Coin> pool_3) { coinPool_3 = pool_3; }

    private void Awake() { magnet = false; onRelease = true; }
    void Update() 
    {
        Vector2 Player_Pos = Player.PL.playerPos.position;
        if (magnet) { transform.position = Vector2.SmoothDamp(transform.position, Player_Pos, ref speed, 0.1f); return; }

        if (setPrefabs) return;
        transform.Translate(-1 * GameManager.GM.data.floorSpeedValue * Time.deltaTime, 0, 0);
    }

    public void DestroyCoin_1() { coinPool_1.Release(this); return; }
    public void DestroyCoin_2() { coinPool_2.Release(this); return; }
    public void DestroyCoin_3() { coinPool_3.Release(this); return; }

    private void Destroy()
    {
        transform.position = new Vector2(6f, 4f);

        if (setObject == "Nomal_Ice")   { DestroyCoin_1(); return; }
        if (setObject == "Hard_Ice")    { DestroyCoin_2(); return; }
        if (setObject == "Special_Ice") { DestroyCoin_3(); return; }

        Destroy(this.gameObject);
    }
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Border")) { Destroy(); }

        if (collision.gameObject.CompareTag("Start_Border")) { magnet = false; onRelease = true; speed = new Vector2(30f, 20f); }

        if (collision.gameObject.CompareTag("Magnet_Borber")) { if (setObject == "LastPoint" || setObject == "obstacle" || setObject == "Hub") return; magnet = true; }

        // 코인이 플레이어 뒤로 가서 안 지워지는 버그 땜빵 / 플레이어 뒤에 콜라이더를 만들어 해결함
        if (collision.gameObject.CompareTag("Coin_Base"))
        {
            if (onRelease)
            {
                switch (setObject)
                {
                    case "Nomal_Ice":       GetCoin(1, 1);     Destroy(); break;
                    case "Hard_Ice":        GetCoin(2, 5);     Destroy(); break;
                    case "Special_Ice":     GetCoin(2, 50);    Destroy(); break;

                    case "Prefab_Nomal":    GetCoin(1, 1);     Destroy(); break;
                    case "Prefab_Hard":     GetCoin(2, 5);     Destroy(); break;
                    case "Prefab_Special":  GetCoin(2, 50);    Destroy(); break;
                }
                onRelease = false;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (onRelease)
            {
                magnet = false;
                switch (setObject)
                {
                    case "Nomal_Ice":       GetCoin(1, 1);     Destroy(); break;
                    case "Hard_Ice":        GetCoin(2, 5);     Destroy(); break;
                    case "Special_Ice":     GetCoin(2, 50);    Destroy(); break;

                    case "Prefab_Nomal":    GetCoin(1, 1);     Destroy(); break;
                    case "Prefab_Hard":     GetCoin(2, 5);     Destroy(); break;
                    case "Prefab_Special":  GetCoin(2, 50);    Destroy(); break;

                    case "LastPoint": Player.PL.clearCheck = true; if (!gameClear) { StartCoroutine(Game_Control.GC.EndGame(true)); gameClear = true; } break;
                    case "obstacle": if (!Player.PL.onHit) Obstacle(); break;
                }
                onRelease = false;
            }
        }
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
