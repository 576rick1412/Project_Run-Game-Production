using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Coin_CS : MonoBehaviour
{
    [SerializeField] string SetObject;
    [SerializeField] bool SetPrefabs; // 프리팹 안에 있는 오브젝트라면 체크
    bool gameClear;
    [SerializeField] bool Magnet;
    Vector2 speed = new Vector2(40f, 30f);

    private IObjectPool<Coin_CS> _CoinPool_1;
    private IObjectPool<Coin_CS> _CoinPool_2;
    private IObjectPool<Coin_CS> _CoinPool_3;
    public void Set_CoinPool_1(IObjectPool<Coin_CS> pool_1) { _CoinPool_1 = pool_1; }
    public void Set_CoinPool_2(IObjectPool<Coin_CS> pool_2) { _CoinPool_2 = pool_2; }
    public void Set_CoinPool_3(IObjectPool<Coin_CS> pool_3) { _CoinPool_3 = pool_3; }

    private void Awake() { Magnet = false; }
    void Update() 
    {
        Vector2 Player_Pos = Player_CS.PL.Player_Pos.position;
        if (Magnet) { transform.position = Vector2.SmoothDamp(transform.position, Player_Pos, ref speed, 0.1f); return; }

        if (SetPrefabs) return;
        transform.Translate(-1 * GameManager.GM.Data.Floor_SpeedValue * Time.deltaTime, 0, 0);
    }

    public void DestroyCoin_1() { _CoinPool_1.Release(this); return; }
    public void DestroyCoin_2() { _CoinPool_2.Release(this); return; }
    public void DestroyCoin_3() { _CoinPool_3.Release(this); return; }

    private void Destroy()
    {
        switch (SetObject)
        {
            case "Nomal_Ice":   DestroyCoin_1(); break;               // 일반 얼음 * 1
            case "Hard_Ice":    DestroyCoin_2(); break;               // 단단한 얼음 * 5
            case "Special_Ice": DestroyCoin_3(); break;               // 테마 얼음 * 50
            case "Hub"          : Destroy(this.gameObject); break;      // 허브
            case "Obstacle"     : Destroy(this.gameObject); break;      // 장애물
            case "Prefab_Ice"   : Destroy(this.gameObject); break;      // 오브젝트 풀링 적용 안 되도록 따로 격리
            case "Platform"     : Destroy(this.gameObject); break;      // 발판
            case "LastPoint"    : Destroy(this.gameObject); break;      // 발판
        }
    }
    void Get_Coin(int point, int multiply)
    {
        // 코인 획득 시 내부 변수 활성화
        Magnet = false; 
        speed = new Vector2(80f, 50f);

        // 코인 획득 시 생명력 증가
        GameManager.GM.Data.LifeScore += point;
        if (GameManager.GM.Data.LifeScore >= GameManager.GM.Data.Set_LifeScore)
        { GameManager.GM.Data.LifeScore = GameManager.GM.Data.Set_LifeScore; }

        // 코인 획득 시 점수 증가
        GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point * multiply; 
        Sound_Manager.SM.Coin();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 코인이 플레이어 뒤로 가서 안 지워지는 버그 땜빵 / 플레이어 뒤에 콜라이더를 만들어 해결함
        if (collision.gameObject.CompareTag("Coin_Base"))
        {
            switch (SetObject)
            {
                case "Nomal_Ice": Get_Coin(1, 1); Destroy(); break;
                case "Hard_Ice": Get_Coin(2, 5); Destroy(); break;
                case "Special_Ice": Get_Coin(1, 50); Destroy(); break;
                case "Prefab_Ice": Get_Coin(1, 1); Destroy(); break;
            }
        }

        if (collision.gameObject.CompareTag("End_Border")) { Destroy(); }

        if (collision.gameObject.CompareTag("Start_Border")) { Magnet = false; }

        if (collision.gameObject.CompareTag("Magnet_Borber")) { if (SetObject == "LastPoint" || SetObject == "Obstacle" || SetObject == "Hub") return; Magnet = true; }

        if (collision.gameObject.CompareTag("Player"))
        {
            Magnet = false; speed = new Vector2(40f, 30f);

            switch (SetObject)
            {
                case "Nomal_Ice": Get_Coin(1, 1); Destroy(); break;
                case "Hard_Ice": Get_Coin(2, 5); Destroy(); break;
                case "Special_Ice": Get_Coin(1, 50); Destroy(); break;
                case "Prefab_Ice": Get_Coin(1, 1); Destroy(); break; 
                case "LastPoint": Player_CS.PL.Clear_Check = true; if (!gameClear) StartCoroutine(Game_Control.GC.EndGame(true)); break;
                case "Obstacle": if (!Player_CS.PL.On_HIT) _Obstacle(); break;
            }
        }
    }
    void _Obstacle()
    {
        GameManager.GM.Data.LifeScore -= GameManager.GM.Data.Obstacle_Damage;
        Player_CS.PL.On_HIT = true;
        Player_CS.PL.OnCoroutine();
    }
}
