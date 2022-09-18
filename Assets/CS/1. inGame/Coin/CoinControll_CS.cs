using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinControll_CS : MonoBehaviour
{
    [SerializeField] private Transform[] CoinPos;

    //private IObjectPool<Coin_CS> Pool;
    void Start()
    {
        //Pool = new ObjectPool<Coin_CS>(CreatCoin, onGetCoin, onReleaseCoin, OnDestroyCoin, maxSize: 4);

        for(int i = 0; i < CoinPos.Length; i++)
        {
            Singleton oc = GameObject.Find("Hephaestus_Canvas").GetComponent<Singleton>();
            oc.InCoinPos = CoinPos[i];
            make_Coin();
        }
        
    }

    void Update()
    {

    }
    private void make_Coin()
    {
        Singleton oc = GameObject.Find("Hephaestus_Canvas").GetComponent<Singleton>();
        var coin = oc.Pool.Get();
        coin.DesCoin();
    }


}
