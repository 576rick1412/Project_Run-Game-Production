using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Coin_CS : MonoBehaviour
{
    [SerializeField] string SetObject;
    [SerializeField] bool SetPrefabs; // 프리팹 안에 있는 오브젝트라면 체크
    bool gameClear;


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
    void LifeUp(int point)
    {
        GameManager.GM.Data.LifeScore += point;
        if (GameManager.GM.Data.LifeScore >= GameManager.GM.Data.Set_LifeScore)
        { GameManager.GM.Data.LifeScore = GameManager.GM.Data.Set_LifeScore; }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Border")) { Destroy(); }

        if (collision.gameObject.CompareTag("Player") && SetObject == "Nomal_Ice") { LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Hard_Ice") { LifeUp(2); GameManager.GM.Data.CoinScore += (GameManager.GM.Data.Coin_Point * 5); Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Special_Ice") { LifeUp(1); GameManager.GM.Data.CoinScore += (GameManager.GM.Data.Coin_Point * 50); Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Prefab_Ice") { LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }

        if (collision.gameObject.CompareTag("Player") && SetObject == "LastPoint") { Player_CS.PL.Clear_Check = true; if(!gameClear) GameEnd(); }

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
        GameManager.GM.Data.Game_WIN = true;
        gameClear = true;
    }
}
