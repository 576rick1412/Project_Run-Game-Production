using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Singleton : MonoBehaviour
{
    [HideInInspector]   public IObjectPool<Coin_CS> Pool;
    [HideInInspector]   public Transform InCoinPos;
    [SerializeField]    private GameObject _CoinPrefab;
    private GameObject Coin_Hub;

    public GameObject CoinPrefabHub;
    public Transform CoinPrefabHub_2;

    public float MSpeed = 0;


    void Start()
    {
        Pool = new ObjectPool<Coin_CS>(CreatCoin, onGetCoin, onReleaseCoin, OnDestroyCoin, maxSize: 20);

        var obj = FindObjectsOfType<Singleton>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);

        Invoke("AAAA", 2f);
        return;
    }
    public void Get_CoinOBJ(GameObject input_coin)
    {
        Coin_Hub = input_coin;
    }

    Coin_CS CreatCoin()
    {
        Coin_CS coin = Instantiate(_CoinPrefab).GetComponent<Coin_CS>();
        coin.transform.parent = Coin_Hub.transform;

        coin.SetManagedPool(Pool);
        return coin;
    }

    void onGetCoin(Coin_CS coin)
    {
        coin.gameObject.SetActive(true);
    }
    void onReleaseCoin(Coin_CS coin)
    {
        coin.gameObject.SetActive(false);
    }
    void OnDestroyCoin(Coin_CS coin)
    {
        Destroy(coin.gameObject);
    }

    void AAAA()
    {
        Instantiate(CoinPrefabHub, CoinPrefabHub_2.transform.position, Quaternion.identity);
    }
}
