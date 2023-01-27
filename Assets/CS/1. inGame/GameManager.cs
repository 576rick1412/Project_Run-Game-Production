using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;
using AesEncryptionNS.Con;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    [SerializeField] RunGame_EX RunGame_EX;

    //���̵� �� �ƿ�
    [Header("���̵� �� �ƿ�")]
    public Image panel;
    public float fTime = 0.2f;
    [HideInInspector] public float ftTime = 0f;

    [Header("�÷��� ����")]
    public bool playerAlive;
    public bool isNormal; // �ϵ��� ����

    [Header("�ε� ȭ��, �������� ����")]
    public string stageName;
    public string stageInformation;

    string filePath;
    void Awake(){GM = this; filePath = Application.persistentDataPath + "/MainDB.txt"; }

    public MainDB data;

    void Start()
    {
        Application.targetFrameRate = 60;

        Debug.Log(filePath);
        LoadData();

        StartCoroutine(AutoSave()); // 10�ʸ��� �ڵ�����
        
        panel.gameObject.SetActive(false);
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
    IEnumerator AutoSave()
    {   for (; ; ) { 
            SavaData();
            yield return new WaitForSeconds(10);
        }
    }
    public void SavaData()
    {
        string key = data.key;
        var save = JsonUtility.ToJson(data);

        save = Program.Encrypt(save, key);
        File.WriteAllText(filePath, save);
    }   // Json ����
    public void LoadData()
    {
        if(!File.Exists(filePath)) { ResetMainDB();  return; }

        string key = data.key;
        var load = File.ReadAllText(filePath);

        load = Program.Decrypt(load, key);
        data = JsonUtility.FromJson<MainDB>(load);
    }   // Json �ε�
    void ResetMainDB()
    {
        data = new MainDB();

        data.money_GM = 0;
        data.goods_GM = 0;
   
        // �������� ����
        data.gameWin = false;
        data.nomalMaxScore = 0;
        data.hardMaxScore = 0;

        data.setFloorSpeedValue = 8f;
        data.setBGSpeedValue = 3f;

        data.floorSpeedValue = data.setFloorSpeedValue;
        data.BGSpeedValue = data.setBGSpeedValue;

        data.constLifeScore = 7;
        data.setLifeScore = 7;
        data.lifeScore = 7;
        data.coinScore = 0;

        // �÷��̾� ����
        data.playerType = "Player_1";
        data.playerJumpValue = 18f;
        data.invincibilityTime = 2f;

        // ���� ����
        data.coinPoint = 100;

        // ��ֹ� ������ ����
        data.obstacleDamage = 2;

        // �ؽ�Ʈ ����
        data.branch_GM = 1;
        data.typingSpeed_GM = 0.1f;

        //�Ҹ� ����
        data.BGM_Value = 1f;
        data.SFX_Value = 1f;

        SavaData();
    }

    public void Fade()
    {
        StartCoroutine(FadeEffect());
    }
    public void Fade(GameObject curObj, GameObject nextObj)
    {
        StartCoroutine(FadeEffect(curObj, nextObj));
    }
    public void Fade(AsyncOperation op)
    {
        StartCoroutine(FadeEffect(op));
    }
    public void Fade(GameObject curObj,bool onOff)
    {
        StartCoroutine(FadeEffect(curObj, onOff));
    }
    // ===========================================================================

    IEnumerator FadeEffect()
    {
        panel.gameObject.SetActive(true);
        ftTime = 0f;
        Color alpha = panel.color;

        while (alpha.a < 1f)
        {
            ftTime += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(0, 1, ftTime);
            panel.color = alpha;

            yield return null;
        }

        ftTime = 0f;
        yield return null;


        while (alpha.a > 0f)
        {
            ftTime += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(1, 0, ftTime);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
    IEnumerator FadeEffect(GameObject curObj, GameObject nextObj)
    {
        panel.gameObject.SetActive(true);
        ftTime = 0f;
        Color alpha = panel.color;

        while (alpha.a < 1f)
        {
            ftTime += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(0, 1, ftTime);
            panel.color = alpha;

            yield return null;
        }

        ftTime = 0f;
        curObj.SetActive(false);
        yield return null;  //yield return new WaitForSeconds(L_time);
        nextObj.SetActive(true);


        while (alpha.a > 0f)
        {
            ftTime += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(1, 0, ftTime);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
    IEnumerator FadeEffect(AsyncOperation op)
    {
        panel.gameObject.SetActive(true);
        ftTime = 0f;
        Color alpha = panel.color;

        while (alpha.a < 1f)
        {
            ftTime += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(0, 1, ftTime);
            panel.color = alpha;

            yield return null;
        }

        ftTime = 0f;
        op.allowSceneActivation = true;
        yield return null;   //yield return new WaitForSeconds(L_time);

        while (alpha.a > 0f)
        {
            ftTime += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(1, 0, ftTime);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
    IEnumerator FadeEffect(GameObject curObj, bool onOff) 
    {
        panel.gameObject.SetActive(true);
        ftTime = 0f;
        Color alpha = panel.color;

        while (alpha.a < 1f)
        {
            ftTime += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(0, 1, ftTime);
            panel.color = alpha;

            yield return null;
        }

        ftTime = 0f;

        if (onOff) Instantiate(curObj);
        if(!onOff) Destroy(curObj);

        yield return null;  //yield return new WaitForSeconds(L_time);


        while (alpha.a > 0f)
        {
            ftTime += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(1, 0, ftTime);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }

    public void Stage_Move(string SceneName, string stageName, string stageDes)
    {
        Loading_Manager.LoadScene(SceneName, stageName, stageDes);
    }

    public string STG_Excel()
    {
        int branch = UnityEngine.Random.Range(1, RunGame_EX.StartSheet.Count + 1);

        for (int i = 0; i < RunGame_EX.StartSheet.Count; ++i)
        {
            if (RunGame_EX.StartSheet[i].STR_branch == branch)
            {
                return RunGame_EX.StartSheet[i].STR_description;
            }
        }

        Debug.Log("���� �������� ���� ��ȯ�� ����!");
        return "Null";
    }
}

[Serializable]
public class MainDB
{
    // AES ��ȣȭ Ű
    [HideInInspector] public string 
        key = "g6hk83da6f2f183fkhj3p5b5n13gh";

    // ����UI
    [Header("���� UI")]
    public int money_GM = 0;
    public int goods_GM = 0;

    // �������� ����
    [Header("�������� ����")]
    public bool gameWin; // �й�Ȯ�� ���϶� �¸�, �����϶� �й�
    public int nomalMaxScore;
    public int hardMaxScore;

    // �ΰ��� ����
    [Header("�ΰ��� ����")]
    public float setFloorSpeedValue = 0;
    public float setBGSpeedValue = 0;

    public float floorSpeedValue = 0;
    public float BGSpeedValue = 0;

    public float constLifeScore = 0;
    public float setLifeScore = 0;
    public float lifeScore = 0;
    public int coinScore = 0;

    public float runRatio = 0f;
    public float curRunRatio = 0f;

    // �÷��̾� ����
    [Header("�÷��̾� ����")]
    public string playerType = "Player_1";
    public float playerJumpValue = 0;
    public float invincibilityTime = 0f;

    // ���� ����
    [Header("���� ����")]
    public int coinPoint = 100;

    // ��ֹ� ������ ����
    [Header("��ֹ� ������ ����")]
    public int obstacleDamage;

    // �ؽ�Ʈ ����
    [Header("�ؽ�Ʈ ����")]
    public int branch_GM;
    public float typingSpeed_GM = 0.1f;

    //�Ҹ� ����
    [Header("�Ҹ� ����")]
    //public float All_Value;
    public float BGM_Value = 1;
    public float SFX_Value = 1;

}

namespace AesEncryptionNS.Con
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
