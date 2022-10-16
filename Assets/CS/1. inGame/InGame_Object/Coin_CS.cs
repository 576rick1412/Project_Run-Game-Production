using UnityEngine;
using UnityEngine.Pool;

public class Coin_CS : MonoBehaviour
{
    [SerializeField] private int SetPoint;
    private int CoinPoint;
    [SerializeField] private bool OnRelease;


    void Awake() { OnRelease = false; }

    private IObjectPool<Coin_CS> _CoinPool_1;
    private IObjectPool<Coin_CS> _CoinPool_2;
    private IObjectPool<Coin_CS> _CoinPool_3;
    public void Set_CoinPool_1(IObjectPool<Coin_CS> pool_1) { _CoinPool_1 = pool_1; }
    public void Set_CoinPool_2(IObjectPool<Coin_CS> pool_2) { _CoinPool_2 = pool_2; }
    public void Set_CoinPool_3(IObjectPool<Coin_CS> pool_3) { _CoinPool_3 = pool_3; }

    void Update()
    {
        transform.Translate(-1 * GameManager.GM.Floor_SpeedValue * Time.deltaTime, 0, 0);

        switch (SetPoint)
        {
            case 1: CoinPoint = GameManager.GM.Get_Coin_1; break;
            case 2: CoinPoint = GameManager.GM.Get_Coin_2; break;
            case 3: CoinPoint = GameManager.GM.Get_Coin_3; break;
        }
    }
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
            GameManager.GM.CoinScore += CoinPoint;
            Destroy();
        }

        /*Vector2 Obj_Pos = transform.position;
if (collision.gameObject.CompareTag("Start_Border"))
{
    Debug.Log("ÁÂÇ¥ Ãæµ¹");
    GameObject Coin =  Instantiate(Coin_Prefabs, Obj_Pos,Quaternion.identity);
    gameObject.SetActive(false);
}*/
    }
}
