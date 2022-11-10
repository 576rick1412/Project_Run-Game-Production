using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Coin_CS : MonoBehaviour
{
    [SerializeField] string SetObject;
    [SerializeField] bool SetPrefabs; // 프리팹 안에 있는 오브젝트라면 체크
    private bool OnRelease; // 스폰 시 릴리즈 초기화 / 스타트 보더 지나가야함 - 지우지마!


    void Awake() { OnRelease = false; }

    private IObjectPool<Coin_CS> _CoinPool_1;
    private IObjectPool<Coin_CS> _CoinPool_2;
    private IObjectPool<Coin_CS> _CoinPool_3;
    public void Set_CoinPool_1(IObjectPool<Coin_CS> pool_1) { _CoinPool_1 = pool_1; }
    public void Set_CoinPool_2(IObjectPool<Coin_CS> pool_2) { _CoinPool_2 = pool_2; }
    public void Set_CoinPool_3(IObjectPool<Coin_CS> pool_3) { _CoinPool_3 = pool_3; }

    void Update() 
    {
        if (SetPrefabs) return;
        transform.Translate(-1 * GameManager.GM.Data.Floor_SpeedValue * Time.deltaTime, 0, 0);
    }

    public void DestroyCoin_1() { if (OnRelease == false) { _CoinPool_1.Release(this); OnRelease = true; } }
    public void DestroyCoin_2() { if (OnRelease == false) { _CoinPool_2.Release(this); OnRelease = true; } }
    public void DestroyCoin_3() { if (OnRelease == false) { _CoinPool_3.Release(this); OnRelease = true; } }

    private void Destroy()
    {
        switch (SetObject)
        {
            case "Coin_1"       : DestroyCoin_1(); break;               // 코인 1
            case "Coin_2"       : DestroyCoin_2(); break;               // 코인 2
            case "Coin_3"       : DestroyCoin_3(); break;               // 코인 3
            case "HP"           : Destroy(this.gameObject); break;      // HP 
            case "Hub"          : Destroy(this.gameObject); break;      // 허브
            case "Obstacle"     : Destroy(this.gameObject); break;      // 장애물
            case "Prefab_Coin"  : Destroy(this.gameObject); break;      // 오브젝트 풀링 적용 안 되도록 따로 격리
            case "Platform"     : Destroy(this.gameObject); break;      // 발판
            case "LastPoint"     : Destroy(this.gameObject); break;      // 발판
        }
    }
    void LifeUp(int point)
    {
        GameManager.GM.Data.LifeScore += point;
        if (GameManager.GM.Data.LifeScore >= GameManager.GM.Data.Set_LifeScore)
        { GameManager.GM.Data.LifeScore = GameManager.GM.Data.Set_LifeScore; }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Start_Border")) OnRelease = false;

        if (collision.gameObject.CompareTag("End_Border")) { Destroy(); }

        if (collision.gameObject.CompareTag("Player") && SetObject == "Coin_1") { LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Coin_2") { LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Coin_3") { LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Prefab_Coin") { LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }

        if (collision.gameObject.CompareTag("Player") && SetObject == "LastPoint") { Player_CS.PL.Clear_Check = true; GameEnd(); }

        if (collision.gameObject.CompareTag("Player") && SetObject == "Obstacle" && Player_CS.PL.On_HIT == false) 
        { 
            GameManager.GM.Data.LifeScore -= GameManager.GM.Data.Obstacle_Damage;
            Player_CS.PL.On_HIT = true; 
            Player_CS.PL.OnCoroutine();
            Player_CS.PL.anime.SetInteger("Player_Value", 5);
        }
    }
    void GameEnd()
    {
        Game_Control.GC.Result_Spawn(); GameManager.GM.SavaData(); Debug.Log("게임 클리어");
        GameManager.GM.Data.Game_WIN = true; Game_Control.GC.Result_Spawn();
    }
}
