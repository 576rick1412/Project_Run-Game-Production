using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Sound_Object : MonoBehaviour
{
    bool isDestroy;
    private IObjectPool<Sound_Object> soundPool;
    public void SetSoundPool(IObjectPool<Sound_Object> pool) { soundPool = pool; }

    void Start()
    {
        
    }
    void Update()
    {
        if (!isDestroy) { Invoke("DestroySound", 0.2f); isDestroy = true; }
    }

    void DestroySound() { soundPool.Release(this); isDestroy = false; }
}
