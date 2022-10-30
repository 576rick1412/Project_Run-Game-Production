using UnityEngine;
using UnityEngine.Pool;

public class Coin_CS : MonoBehaviour
{
    [SerializeField] int SetPoint;
    private int CoinPoint;
    [SerializeField] private bool OnRelease; // 스폰 시 릴리즈 초기화 / 스타트 보더 지나가야함 - 지우지마!


    void Awake() { OnRelease = false; CoinPoint = GameManager.GM.Data.Coin_Point; }

    private IObjectPool<Coin_CS> _CoinPool_1;
    private IObjectPool<Coin_CS> _CoinPool_2;
    private IObjectPool<Coin_CS> _CoinPool_3;
    public void Set_CoinPool_1(IObjectPool<Coin_CS> pool_1) { _CoinPool_1 = pool_1; }
    public void Set_CoinPool_2(IObjectPool<Coin_CS> pool_2) { _CoinPool_2 = pool_2; }
    public void Set_CoinPool_3(IObjectPool<Coin_CS> pool_3) { _CoinPool_3 = pool_3; }

    void FixedUpdate() { transform.Translate(-1 * GameManager.GM.Data.Floor_SpeedValue * Time.smoothDeltaTime, 0, 0); }

    public void DestroyCoin_1() { if (OnRelease == false) { _CoinPool_1.Release(this); OnRelease = true; } }
    public void DestroyCoin_2() { if (OnRelease == false) { _CoinPool_2.Release(this); OnRelease = true; } }
    public void DestroyCoin_3() { if (OnRelease == false) { _CoinPool_3.Release(this); OnRelease = true; } }

    private void Destroy()
    {
        switch (SetPoint)
        {
            case 1: DestroyCoin_1(); break;
            case 2: DestroyCoin_2(); break;
            case 3: DestroyCoin_3(); break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Start_Border")) OnRelease = false;
        
        if (collision.gameObject.CompareTag("End_Border"))
        {
            Destroy();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.GM.Data.CoinScore += CoinPoint;
            Destroy();
        }

        /*Vector2 Obj_Pos = transform.position;
if (collision.gameObject.CompareTag("Start_Border"))
{
    Debug.Log("좌표 충돌");
    GameObject Coin =  Instantiate(Coin_Prefabs, Obj_Pos,Quaternion.identity);
    gameObject.SetActive(false);
}*/
    }
}
