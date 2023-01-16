using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager SM;

    [SerializeField] GameObject buttonSound; 
    [SerializeField] GameObject coinSound; 
    [SerializeField] GameObject wrongSound;

    private IObjectPool<Sound_Object> soundPool;
    void Awake() 
    {
        SM = this;
        soundPool = new ObjectPool<Sound_Object>(Coin_1_Creat, Coin_1_Get, Coin_1_Releas, Coin_1_Destroy, maxSize: 30);
    }
    private Sound_Object Coin_1_Creat()
    {
        Sound_Object sound = Instantiate(coinSound).GetComponent<Sound_Object>();
        sound.SetSoundPool(soundPool);
        return sound;
    }                    // (풀링) 코인 1 생성
    private void Coin_1_Get(Sound_Object sound)
    {
        sound.gameObject.SetActive(true);
    }           // (풀링) 코인 활성화
    private void Coin_1_Releas(Sound_Object sound)
    {
        sound.gameObject.SetActive(false);
    }        // (풀링) 코인 비활성화
    private void Coin_1_Destroy(Sound_Object sound)
    {
        Destroy(sound.gameObject);
    }       // (풀링) 코인 삭제

    public void Button() { GameObject sound = Instantiate(buttonSound); Destroy(sound.gameObject, 1f); }
    public void Coin() { soundPool.Get(); }
    public void Wrong()  { GameObject sound = Instantiate(wrongSound);  Destroy(sound.gameObject, 1f); }
}
