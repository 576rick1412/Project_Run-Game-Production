using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Singleton : MonoBehaviour
{
    [HideInInspector]   public IObjectPool<Coin_CS> Pool;
    [HideInInspector]   public Transform InCoinPos;
    [SerializeField]    private GameObject _CoinPrefab;



    void Start()
    {
        Pool = new ObjectPool<Coin_CS>(CreatCoin, onGetCoin, onReleaseCoin, OnDestroyCoin, maxSize: 6);

        var obj = FindObjectsOfType<Singleton>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }

    Coin_CS CreatCoin()
    {
        Coin_CS coin = Instantiate(_CoinPrefab, InCoinPos.transform.position, Quaternion.identity).GetComponent<Coin_CS>();
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
}
