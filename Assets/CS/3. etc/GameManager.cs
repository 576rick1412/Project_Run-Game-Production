using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    // 메인UI
    [Header("메인 UI")]
    public string GM_NickName = "";
    public string GM_Level = "";
    public string GM_Money = "";
    public string GM_Goods = "";
    public float GM_EXE = 0f;
    public float GM_MAX_EXE = 0f;

    // 플레이어
    [Header("플레이어")]
    public float PlayerJumpValue;
    public float Invincibility_Time;

    // 코인 설정
    [Header("코인 설정")]
    public int Get_Coin_1;
    public int Get_Coin_2;
    public int Get_Coin_3;

    // 데미지 설정
    [Header("데미지 설정")]
    public int Get_Damage_1;
    public int Get_Damage_2;
    public int Get_Damage_3;

    // 인게임 설정
    [Header("인게임 설정")]
    public float Floor_SpeedValue;
    public float BGI_SpeedValue;
    public float LifeScore;
    public long CoinScore = 0;
    void Awake(){GM = this;}

    void Start()
    {
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }

    void Update()
    {
        LifeScore -= Time.deltaTime * 2;
    }
}
