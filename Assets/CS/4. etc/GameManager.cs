using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    //페이드 인 아웃
    [Header("페이드 인 아웃")]
    public Image Panel;
    public float F_time = 0.5f;
    float FM_time = 0f;

    //게임 설정
    [Header("게임 설정")]
    //public float All_Value;
    public float BGM_Value;
    public float SFX_Value;

    // 메인UI
    [Header("메인 UI")]
    public string   GM_NickName = "";
    public string   GM_Level = "";
    public string   GM_Money = "";
    public string   GM_Goods = "";
    public float    GM_EXE = 0f;
    public float    GM_MAX_EXE = 0f;

    // 코인 설정
    [Header("코인 설정")]
    public int  Get_Coin_1;
    public int  Get_Coin_2;
    public int  Get_Coin_3;

    // 데미지 설정
    [Header("데미지 설정")]
    public int  Get_Damage_1;
    public int  Get_Damage_2;
    public int  Get_Damage_3;

    // 인게임 설정
    [Header("인게임 설정")]
    public float    Floor_SpeedValue;
    public float    BGI_SpeedValue;
    public float    Set_LifeScore;
    public float    LifeScore;
    public long     CoinScore = 0;

    // 플레이어
    [Header("플레이어")]
    public float PlayerJumpValue;
    public float Invincibility_Time;

    // 텍스트 설정
    [Header("텍스트 설정")]
    public int GM_branch;
    public float GM_typingSpeed;
    void Awake(){GM = this;}

    void Start()
    {
        Panel.gameObject.SetActive(false);

        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }

    void Update()
    {

    }

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
