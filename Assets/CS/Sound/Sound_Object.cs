using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Sound_Object : MonoBehaviour
{
    bool Destroy_Check;
    private IObjectPool<Sound_Object> _SoundPool;
    public void Set_Sound_Pool(IObjectPool<Sound_Object> pool) { _SoundPool = pool; }

    void Start()
    {
        
    }
    void Update()
    {
        if (!Destroy_Check) { Invoke("DestroySound", 0.2f); Destroy_Check = true; }
    }

    void DestroySound() { _SoundPool.Release(this); Destroy_Check = false; }
}
