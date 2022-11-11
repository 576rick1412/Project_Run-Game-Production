using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager SM;

    [SerializeField] GameObject Button_Sound; 
    [SerializeField] GameObject Coin_Sound; 
    [SerializeField] GameObject Wrong_Sound;

    private IObjectPool<Sound_Object> SoundPool;
    void Awake() 
    {
        SM = this;
        SoundPool = new ObjectPool<Sound_Object>(Coin_1_Creat, Coin_1_Get, Coin_1_Releas, Coin_1_Destroy, maxSize: 30);
    }
    private Sound_Object Coin_1_Creat()
    {
        Sound_Object Coin_1 = Instantiate(Coin_Sound).GetComponent<Sound_Object>();
        Coin_1.Set_Sound_Pool(SoundPool);
        return Coin_1;
    }                    // (풀링) 코인 1 생성
    private void Coin_1_Get(Sound_Object Coin_1)
    {
        Coin_1.gameObject.SetActive(true);
    }           // (풀링) 코인 활성화
    private void Coin_1_Releas(Sound_Object Coin_1)
    {
        Coin_1.gameObject.SetActive(false);
    }        // (풀링) 코인 비활성화
    private void Coin_1_Destroy(Sound_Object Coin_1)
    {
        Destroy(Coin_1.gameObject);
    }       // (풀링) 코인 삭제

    public void Button() { GameObject Sound = Instantiate(Button_Sound); Destroy(Sound.gameObject, 1f); }
    public void Coin() { SoundPool.Get(); }
    public void Wrong()  { GameObject Sound = Instantiate(Wrong_Sound);  Destroy(Sound.gameObject, 1f); }
}