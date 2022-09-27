using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    // ����UI
    [Header("���� UI")]
    public string GM_NickName = "";
    public string GM_Level = "";
    public string GM_Money = "";
    public string GM_Goods = "";
    public float GM_EXE = 0f;
    public float GM_MAX_EXE = 0f;

    // �÷��̾�
    [Header("�÷��̾�")]
    public float PlayerJumpValue;

    // �ΰ��� ����
    [Header("�ΰ��� ����")]
    public float Floor_SpeedValue;
    public float BGI_SpeedValue;
    public float LifeScore;
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
