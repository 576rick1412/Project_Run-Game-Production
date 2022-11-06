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

    [Range(1,6)] public int nowStage;
    //페이드 인 아웃
    [Header("페이드 인 아웃")]
    public Image Panel;
    public float F_time = 0.2f;
    [HideInInspector] public float FM_time = 0f;

   

    string FilePath;
    void Awake(){GM = this; FilePath = Application.persistentDataPath + "/MainDB.txt"; }
    public MainDB Data;

    void Start()
    {
        Debug.Log(FilePath);
        LoadData();

    StartCoroutine(AutoSave()); // 5초마다 자동저장
        
        Panel.gameObject.SetActive(false);
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
    IEnumerator AutoSave()
    {   for (; ; ) { 
            SavaData(); Debug.Log("자동저장!");
            yield return new WaitForSeconds(5);
        }
    }
    public void SavaData()
    {
        string key = Data.key;
        var save = JsonUtility.ToJson(Data);

        save = Program.Encrypt(save, key);
        File.WriteAllText(FilePath, save);
    }   // Json 저장
    public void LoadData()
    {
        if(!File.Exists(FilePath)) { ResetMainDB();  return; }

        string key = Data.key;
        var load = File.ReadAllText(FilePath);

        load = Program.Decrypt(load, key);
        Data = JsonUtility.FromJson<MainDB>(load);
    }   // Json 로딩
    public void ResetMainDB()
    {
        Data = new MainDB();

        Data.GM_NickName = "지스타2022";
        Data.GM_Level = 1;
        Data.GM_Money = 0;
        Data.GM_Goods = 0;
        Data.GM_EXE = 0;
        Data.GM_MAX_EXE = 1000;

        // 스테이지 정보
        Data.Game_Fail = false;
        Data.stage_clear_Num = 0;
        for (int i = 0; i < Data.stage_Max_Score.Length; i++) Data.stage_Max_Score[i] = 0;

        Data.Set_Floor_SpeedValue = 10f;
        Data.Set_BGI_SpeedValue = 3f;

        Data.Floor_SpeedValue = Data.Set_Floor_SpeedValue;
        Data.BGI_SpeedValue = Data.Set_BGI_SpeedValue;

        Data.Set_LifeScore = 100;
        Data.LifeScore = 100;
        Data.CoinScore = 0;

        // 플레이어 설정
        Data.PlayerType = "Player_1";
        Data.PlayerJumpValue = 15f;
        Data.Invincibility_Time = 3f;

        // 코인 설정
        Data.Coin_Point = 100;

        // 장애물 데미지 설정
        Data.Obstacle_Damage = 30;

        // 보스 설정
        Data.Set_Boss_HP = 3000;
        Data.Boss_Damage = 30;
        Data.Boss_DIE = false;

        // 공격 데미지 설정
        Data.Player_Damage = 30;
        Data.Attack_Speed = 1;

        // 텍스트 설정
        Data.GM_branch = 1;
        Data.GM_typingSpeed = 0.1f;

        //소리 설정
        Data.BGM_Value = 1f;
        Data.SFX_Value = 1f;


        SavaData();
        LoadData();
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
    public void Fade(GameObject CurObj)
    {
        StartCoroutine(FadeEffect(CurObj));
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
    IEnumerator FadeEffect(GameObject CurObj) 
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
        Destroy(CurObj);
        yield return null;  //yield return new WaitForSeconds(L_time);


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

    // 스테이지 정보
    [Header("스테이지 정보")]
    public bool Game_Fail;
    public int stage_clear_Num;
    public int[] stage_Max_Score = new int[60 + 1];


    // 인게임 설정
    [Header("인게임 설정")]
    public float Set_Floor_SpeedValue = 6;
    public float Set_BGI_SpeedValue = 3;

    public float Floor_SpeedValue = 6;
    public float BGI_SpeedValue = 3;

    public float Set_LifeScore = 0;
    public float LifeScore = 0;
    public int CoinScore = 0;

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
    public int Player_Damage;
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