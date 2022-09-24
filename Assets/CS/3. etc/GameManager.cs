using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public string GM_NickName = "";
    public string GM_Level = "";
    public string GM_Money = "";
    public string GM_Goods = "";

    public float GM_EXE = 0f;
    public float GM_MAX_EXE = 0f;

    void Awake(){GM = this;}

    void Start()
    {
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
}
