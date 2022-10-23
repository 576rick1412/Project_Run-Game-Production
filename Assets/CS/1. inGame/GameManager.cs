using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    //���̵� �� �ƿ�
    [Header("���̵� �� �ƿ�")]
    public Image Panel;
    public float F_time = 0.5f;
    [HideInInspector] public float FM_time = 0f;

    // �ΰ��� ����
    [Header("�ΰ��� ����")]
    public float Floor_SpeedValue = 6;
    public float BGI_SpeedValue = 3;

    public float Set_LifeScore = 0;
    public float LifeScore = 0;
    public long CoinScore = 0;

    // �÷��̾� ����
    [Header("�÷��̾� ����")]
    public string PlayerType = "Player_1";
    public float PlayerJumpValue = 6;
    public float Invincibility_Time = 3f;

    // ���� ����
    [Header("���� ����")]
    public int Coin_Point = 100;

    // ��ֹ� ������ ����
    [Header("��ֹ� ������ ����")]
    public int Obstacle_Damage;

    // ���� ����
    [Header("���� ����")]
    public float Set_Boss_HP;
    public float Boss_HP;
    public int Boss_Damage;

    // ���� ������ ����
    [Header("���� ������ ����")]
    public int   Player_Damage;
    public float Attack_Speed;

    // �ؽ�Ʈ ����
    [Header("�ؽ�Ʈ ����")]
    public int GM_branch;
    public float GM_typingSpeed = 0.1f;

    void Awake(){GM = this;}

    public MainDB Data = new MainDB();
    void Start()
    {
        LoadData();

        Panel.gameObject.SetActive(false);
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }

    public void SavaData()
    {
        var save = JsonUtility.ToJson(Data);
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "/MainDB.json"), save);
    }   // Json ����
    public void LoadData()
    {
        var load = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "/MainDB.json"));
        Data = JsonUtility.FromJson<MainDB>(load);
    }   // Json �ε�

    public void Fade()
    {
        StartCoroutine(FadeEffect());
    }
    public void Fade(GameObject CurObj, GameObject NextObj)
    {
        StartCoroutine(FadeEffect(CurObj, NextObj));
    }
    public void Fade(AsyncOperation op)
    {
        StartCoroutine(FadeEffect(op));
    }

    // ===========================================================================

    IEnumerator FadeEffect()
    {
        Panel.gameObject.SetActive(true);
        FM_time = 0f;
        Color alpha = Panel.color;

        while (alpha.a < 1f)
        {
            FM_time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, FM_time);
            Panel.color = alpha;

            yield return null;
        }

        FM_time = 0f;
        yield return null;


        while (alpha.a > 0f)
        {
            FM_time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, FM_time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
    IEnumerator FadeEffect(GameObject CurObj, GameObject NextObj)
    {
        Panel.gameObject.SetActive(true);
        FM_time = 0f;
        Color alpha = Panel.color;

        while (alpha.a < 1f)
        {
            FM_time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, FM_time);
            Panel.color = alpha;

            yield return null;
        }

        FM_time = 0f;
        CurObj.SetActive(false);
        yield return null;  //yield return new WaitForSeconds(L_time);
        NextObj.SetActive(true);


        while (alpha.a > 0f)
        {
            FM_time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, FM_time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
    IEnumerator FadeEffect(AsyncOperation op)
    {
        Panel.gameObject.SetActive(true);
        FM_time = 0f;
        Color alpha = Panel.color;

        while (alpha.a < 1f)
        {
            FM_time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, FM_time);
            Panel.color = alpha;

            yield return null;
        }

        FM_time = 0f;
        op.allowSceneActivation = true;
        yield return null;   //yield return new WaitForSeconds(L_time);

        while (alpha.a > 0f)
        {
            FM_time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, FM_time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
[System.Serializable]
public class MainDB
{
    // ����UI
    [Header("���� UI")]
    public string GM_NickName = "";
    public int GM_Level;
    public int GM_Money;
    public int GM_Goods;
    public float GM_EXE;
    public float GM_MAX_EXE;

    //���� ����
    [Header("���� ����")]
    //public float All_Value;
    public float BGM_Value = 1;
    public float SFX_Value = 1;

}