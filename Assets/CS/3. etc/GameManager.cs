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

    // 인게임 설정
    [Header("인게임 설정")]
    public float MapSpeedValue;
    void Awake(){GM = this;}

    void Start()
    {
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
}
