using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin_CS : MonoBehaviour
{
    private IObjectPool<Coin_CS> _ManagedPool;

    public void SetManagedPool(IObjectPool<Coin_CS> pool)
    {
        _ManagedPool = pool;
    }
    public void DestroyCoin()
    {
        _ManagedPool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ÄÚÀÎ È¹µæ!!");
            DestroyCoin();
        }
    }
}
