using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;
using AESWithJava.Con;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    //페이드 인 아웃
    [Header("페이드 인 아웃")]
    public Image Panel;
    public float F_time = 0.5f;
    [HideInInspector] public float FM_time = 0f;

    // 인게임 설정
    [Header("인게임 설정")]
    public float Floor_SpeedValue = 6;
    public float BGI_SpeedValue = 3;

    public float Set_LifeScore = 0;
    public float LifeScore = 0;
    public long CoinScore = 0;

    // 플레이어 설정
    [Header("플레이어 설정")]
    public string PlayerType = "Player_1";
    public float PlayerJumpValue = 6;
    public float Invincibility_Time = 3f;

    // 코인 설정
    [Header("코인 설정")]
    public int Coin_Point = 100;

    // 장애물 데미지 설정
    [Header("장애물 데미지 설정")]
    public int Obstacle_Damage;

    // 보스 설정
    [Header("보스 설정")]
    public float Set_Boss_HP;
    public float Boss_HP;
    public int Boss_Damage;
    public bool Boss_DIE;

    // 공격 데미지 설정
    [Header("공격 데미지 설정")]
    public int   Player_Damage;
    public float Attack_Speed;

    // 텍스트 설정
    [Header("텍스트 설정")]
    public int GM_branch;
    public float GM_typingSpeed = 0.1f;

    //소리 설정
    [Header("소리 설정")]
    //public float All_Value;
    public float BGM_Value = 1;
    public float SFX_Value = 1;

    void Awake(){GM = this;}
    public MainDB Data = new MainDB();
    void Start()
    {
        Data.GM_NickName = "소드리우스";
        Data.GM_Level = 1;
        Data.GM_Money = 0;
        Data.GM_Goods = 0;
        Data.GM_EXE = 0;
        Data.GM_MAX_EXE = 1000;
        
        //SavaData();
        //LoadData();

        Panel.gameObject.SetActive(false);
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }

    public void SavaData()
    {
        string key = Data.key;
        var save = JsonUtility.ToJson(Data);
        save = Program.Encrypt(save, key);
        
        if (Application.platform == RuntimePlatform.Android)    // AOS 용
            File.WriteAllText(Path.Combine("jar:file://" + Application.streamingAssetsPath, "/MainDB.json"), save);
        else File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "/MainDB.json"), save); // 에디터 용

    }   // Json 저장
    public void LoadData()
    {
        string key = Data.key;
        var Onload = "";

        if (Application.platform == RuntimePlatform.Android)    // AOS 용
        { var load = File.ReadAllText(Path.Combine("jar:file://" + Application.streamingAssetsPath, "/MainDB.json")); Onload = load; }

        else { var load = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "/MainDB.json")); Onload = load; } // 에디터 용

        Onload = Program.Decrypt(Onload, key);
        Data = JsonUtility.FromJson<MainDB>(Onload);
    }   // Json 로딩

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
[Serializable]
public class MainDB
{
    // AES 암호화 키
    [HideInInspector] public string key = "g6hk83da6f2f183fkhj3p5b5n13gh";

    // 메인UI
    [Header("메인 UI")]
    public string GM_NickName = "소드리우스";
    public int GM_Level = 1;
    public int GM_Money = 0;
    public int GM_Goods = 0;
    public float GM_EXE = 0;
    public float GM_MAX_EXE = 1000;
}

namespace AESWithJava.Con
{
    public class Program
    {
        public static string Decrypt(string textToDecrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 256;
            rijndaelCipher.BlockSize = 256;
            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[32];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) { len = keyBytes.Length; }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        public static string Encrypt(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 256;
            rijndaelCipher.BlockSize = 256;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[32];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) { len = keyBytes.Length; }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }
    }
}