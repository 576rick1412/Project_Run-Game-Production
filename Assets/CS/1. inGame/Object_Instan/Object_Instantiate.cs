using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Object_Instantiate : MonoBehaviour
{
    [SerializeField] float late_Time;

    [SerializeField] Chapter_1 chapterEX_1;
    [SerializeField] Chapter_2 chapterEX_2;
    [SerializeField] Chapter_3 chapterEX_3;
    [SerializeField] Chapter_4 chapterEX_4;
    [SerializeField] Chapter_5 chapterEX_5;
    [SerializeField] Chapter_6 chapterEX_6;

    [SerializeField] GameObject[] coinObjects;
    [SerializeField] GameObject[] obstacleObjects;
    [SerializeField] GameObject[] platformObjects;

    [SerializeField] Transform[] instanPoss;

    GameObject instanCoin;
    GameObject instanObstacle;
    GameObject instanPlatform;

    [SerializeField] int index = 0;
    int posNum = 0;

    bool coinSkip = false; // ���� ��ŵ \ true -> ��ŵ
    bool isMaker = false; // �ڷ�ƾ ���ư��� �ִ��� Ȯ��

    private IObjectPool<Coin> coinPool_1;
    private IObjectPool<Coin> coinPool_2;
    private IObjectPool<Coin> coinPool_3;

    void Awake()
    {
        RunRatioReset(); // ���� �޸��Ÿ� ��ü ���� �� ���ϴ� �Լ�

        coinPool_1 = new ObjectPool<Coin>(Coin_1_Creat, Coin_1_Get, Coin_1_Releas, Coin_1_Destroy, maxSize: 30);
        coinPool_2 = new ObjectPool<Coin>(Coin_2_Creat, Coin_2_Get, Coin_2_Releas, Coin_2_Destroy, maxSize: 30);
        coinPool_3 = new ObjectPool<Coin>(Coin_3_Creat, Coin_3_Get, Coin_3_Releas, Coin_3_Destroy, maxSize: 30);
    }

    private Coin Coin_1_Creat()
    {
        Coin coin_1 = Instantiate(instanCoin).GetComponent<Coin>();
        coin_1.SetCoinPool_1(coinPool_1);
        return coin_1;
    }                    // (Ǯ��) ���� 1 ����
    private void Coin_1_Get(Coin coin_1)
    {
        coin_1.gameObject.SetActive(true);
    }           // (Ǯ��) ���� Ȱ��ȭ
    private void Coin_1_Releas(Coin coin_1)
    {
        coin_1.gameObject.SetActive(false);
    }        // (Ǯ��) ���� ��Ȱ��ȭ
    private void Coin_1_Destroy(Coin coin_1)
    {
        Destroy(coin_1.gameObject);
    }       // (Ǯ��) ���� ����


    private Coin Coin_2_Creat()
    {
        Coin coin_2 = Instantiate(instanCoin).GetComponent<Coin>();
        coin_2.SetCoinPool_2(coinPool_2);
        return coin_2;
    }                    // (Ǯ��) ���� 2 ����
    private void Coin_2_Get(Coin coin_2)
    {
        coin_2.gameObject.SetActive(true);
    }           // (Ǯ��) ���� Ȱ��ȭ
    private void Coin_2_Releas(Coin coin_2)
    {
        coin_2.gameObject.SetActive(false);
    }        // (Ǯ��) ���� ��Ȱ��ȭ
    private void Coin_2_Destroy(Coin coin_2)
    {
        Destroy(coin_2.gameObject);
    }       // (Ǯ��) ���� ����


    private Coin Coin_3_Creat()
    {
        Coin coin_3 = Instantiate(instanCoin).GetComponent<Coin>();
        coin_3.SetCoinPool_3(coinPool_3);
        return coin_3;
    }                    // (Ǯ��) ���� 3 ����
    private void Coin_3_Get(Coin coin_3)
    {
        coin_3.gameObject.SetActive(true);
    }           // (Ǯ��) ���� Ȱ��ȭ
    private void Coin_3_Releas(Coin coin_3)
    {
        coin_3.gameObject.SetActive(false);
    }        // (Ǯ��) ���� ��Ȱ��ȭ
    private void Coin_3_Destroy(Coin coin_3)
    {
        Destroy(coin_3.gameObject);
    }       // (Ǯ��) ���� ����

    void Update()
    {
        var Chapter_EX = chapterEX_1;
        switch (GameManager.GM.data.branch_GM)
        {
            case <= 10: Chapter_EX = chapterEX_1; break;
            case <= 20: Chapter_EX = chapterEX_2; break;
            case <= 30: Chapter_EX = chapterEX_3; break;
            case <= 40: Chapter_EX = chapterEX_4; break;
            case <= 50: Chapter_EX = chapterEX_5; break;
            case <= 60: Chapter_EX = chapterEX_6; break;
        }


        switch (GameManager.GM.data.branch_GM % 10)
        {
            case 1: GameEndControl(Chapter_EX.stage_1[index].end); break;
            case 2: GameEndControl(Chapter_EX.stage_2[index].end); break;
            case 3: GameEndControl(Chapter_EX.stage_3[index].end); break;
            case 4: GameEndControl(Chapter_EX.stage_4[index].end); break;
            case 5: GameEndControl(Chapter_EX.stage_5[index].end); break;
            case 6: GameEndControl(Chapter_EX.stage_6[index].end); break;
            case 7: GameEndControl(Chapter_EX.stage_7[index].end); break;
            case 8: GameEndControl(Chapter_EX.stage_8[index].end); break;
            case 9: GameEndControl(Chapter_EX.stage_9[index].end); break;
            case 0: GameEndControl(Chapter_EX.stage_10[index].end); break;
        }
    }
    void RunRatioReset()
    {
        var Chapter_EX = chapterEX_1;
        switch (GameManager.GM.data.branch_GM)
        {
            case <= 10: Chapter_EX = chapterEX_1; break;
            case <= 20: Chapter_EX = chapterEX_2; break;
            case <= 30: Chapter_EX = chapterEX_3; break;
            case <= 40: Chapter_EX = chapterEX_4; break;
            case <= 50: Chapter_EX = chapterEX_5; break;
            case <= 60: Chapter_EX = chapterEX_6; break;
        }

        switch (GameManager.GM.data.branch_GM % 10)
        {
            case 1: GameManager.GM.data.runRatio = Chapter_EX.stage_1[0].coinSum; break;
            case 2: GameManager.GM.data.runRatio = Chapter_EX.stage_2[0].coinSum; break;
            case 3: GameManager.GM.data.runRatio = Chapter_EX.stage_3[0].coinSum; break;
            case 4: GameManager.GM.data.runRatio = Chapter_EX.stage_4[0].coinSum; break;
            case 5: GameManager.GM.data.runRatio = Chapter_EX.stage_5[0].coinSum; break;
            case 6: GameManager.GM.data.runRatio = Chapter_EX.stage_6[0].coinSum; break;
            case 7: GameManager.GM.data.runRatio = Chapter_EX.stage_7[0].coinSum; break;
            case 8: GameManager.GM.data.runRatio = Chapter_EX.stage_8[0].coinSum; break;
            case 9: GameManager.GM.data.runRatio = Chapter_EX.stage_9[0].coinSum; break;
            case 0: GameManager.GM.data.runRatio = Chapter_EX.stage_10[0].coinSum; break;
        }
        
    }
    public void UpPointStar()
    {
        var Chapter_EX = chapterEX_1;
        switch (GameManager.GM.data.branch_GM)
        {
            case <= 10: Chapter_EX = chapterEX_1; break;
            case <= 20: Chapter_EX = chapterEX_2; break;
            case <= 30: Chapter_EX = chapterEX_3; break;
            case <= 40: Chapter_EX = chapterEX_4; break;
            case <= 50: Chapter_EX = chapterEX_5; break;
            case <= 60: Chapter_EX = chapterEX_6; break;
        }

        switch (GameManager.GM.data.branch_GM % 10)
        {
            case 1: Star(Chapter_EX.stage_1[1].coinSum,  Chapter_EX.stage_1[2].coinSum,  Chapter_EX.stage_1[3].coinSum); break;
            case 2: Star(Chapter_EX.stage_2[1].coinSum,  Chapter_EX.stage_2[2].coinSum,  Chapter_EX.stage_2[3].coinSum); break;
            case 3: Star(Chapter_EX.stage_3[1].coinSum,  Chapter_EX.stage_3[2].coinSum,  Chapter_EX.stage_3[3].coinSum); break;
            case 4: Star(Chapter_EX.stage_4[1].coinSum,  Chapter_EX.stage_4[2].coinSum,  Chapter_EX.stage_4[3].coinSum); break;
            case 5: Star(Chapter_EX.stage_5[1].coinSum,  Chapter_EX.stage_5[2].coinSum,  Chapter_EX.stage_5[3].coinSum); break;
            case 6: Star(Chapter_EX.stage_6[1].coinSum,  Chapter_EX.stage_6[2].coinSum,  Chapter_EX.stage_6[3].coinSum); break;
            case 7: Star(Chapter_EX.stage_7[1].coinSum,  Chapter_EX.stage_7[2].coinSum,  Chapter_EX.stage_7[3].coinSum); break;
            case 8: Star(Chapter_EX.stage_8[1].coinSum,  Chapter_EX.stage_8[2].coinSum,  Chapter_EX.stage_8[3].coinSum); break;
            case 9: Star(Chapter_EX.stage_9[1].coinSum,  Chapter_EX.stage_9[2].coinSum,  Chapter_EX.stage_9[3].coinSum); break;
            case 0: Star(Chapter_EX.stage_10[1].coinSum, Chapter_EX.stage_10[2].coinSum, Chapter_EX.stage_10[3].coinSum); break;
        }
    }

    void Star(int star_3, int star_2, int star_1)
    {
        int score = GameManager.GM.data.coinScore;
        int clearStar = GameManager.GM.data.stageClearStars[GameManager.GM.data.branch_GM];

        // ���Ӹ޴��� �� ���� ����Ʈ ����
        GameManager.GM.percentStars[0] = star_3;
        GameManager.GM.percentStars[1] = star_2;
        GameManager.GM.percentStars[2] = star_1;

        int nowStar;

        if (score >= star_3) { if (clearStar < 3) clearStar = 3; Debug.Log("3�� Ŭ����"); nowStar = 3; goto A; }
        if (score >= star_2) { if (clearStar < 2) clearStar = 2; Debug.Log("2�� Ŭ����"); nowStar = 2; goto A; }
        if (score >= star_1) { if (clearStar < 1) clearStar = 1; Debug.Log("1�� Ŭ����"); nowStar = 1; goto A; }
        else { nowStar = 0; Debug.Log("0�� Ŭ����"); }

        A:
        GameManager.GM.data.stageClearStars[GameManager.GM.data.branch_GM] = clearStar;
        GameManager.GM.nowClearStar = nowStar;
    }


    void GameEndControl(bool end)
    {
        if (!end && !isMaker)
        {
            int branch = GameManager.GM.data.branch_GM;
            Debug.Log(index);

            switch (branch)
            {
                case 1: StartCoroutine("CoinMaker_1"); break;
                case 2: StartCoroutine("CoinMaker_2"); break;
                case 3: StartCoroutine("CoinMaker_3"); break;
                case 4: StartCoroutine("CoinMaker_4"); break;
                case 5: StartCoroutine("CoinMaker_5"); break;
                case 6: StartCoroutine("CoinMaker_6"); break;
                case 7: StartCoroutine("CoinMaker_7"); break;
                case 8: StartCoroutine("CoinMaker_8"); break;
                case 9: StartCoroutine("CoinMaker_9"); break;
                case 0: StartCoroutine("CoinMaker_10");break;
            }
        }
    }

    void CoinType(string type)
    {
        switch (type) // ���� ����
        {
            case "None": coinSkip = true; break;               // ���� ���� ����
            case "coin_1": instanCoin = coinObjects[0]; break; // ���� 1
            case "coin_2": instanCoin = coinObjects[1]; break; // ���� 2
            case "coin_3": instanCoin = coinObjects[2]; break; // ���� 3
            case "LastPoint": instanCoin = coinObjects[3]; break;     // ������ ����
            case "type_1": instanCoin = coinObjects[4]; break;    // 1�� ���
            case "type_2": instanCoin = coinObjects[5]; break;    // 2�� ���
            case "type_3": instanCoin = coinObjects[6]; break;    // 3�� ���
        }
    }

    void ObstacleType(string type)
    {
        switch (type) // ��ֹ� ����
        {
            case "Obstacle_1": instanObstacle = obstacleObjects[0]; break; // ���� ��ֹ�
            case "Obstacle_2": instanObstacle = obstacleObjects[1]; break; // �������� ��ֹ�
            case "Obstacle_3": instanObstacle = obstacleObjects[2]; break; // �����̵� ��ֹ�
        }
    }
    void PlatformType(string type)
    {
        switch (type) // ���� ����
        {
            case "Platform_1": instanPlatform = platformObjects[0]; break; // ����
            case "Platform_2": instanPlatform = platformObjects[1]; break; // ����
            case "Platform_3": instanPlatform = platformObjects[2]; break; // ����
            case "Platform_4": instanPlatform = platformObjects[3]; break; // ����
        }
    }
    void CoinAmount(string type, string obstacle, string platform, int platformPos)
    {
        if (coinSkip == false)
        {
            switch (type)
            {
                case "None": break;
                // ====================================================================
                case "coin_1":
                    var Coin_1 = coinPool_1.Get();
                    Coin_1.transform.position = instanPoss[posNum].position; break;
                // ====================================================================
                case "coin_2":
                    var Coin_2 = coinPool_2.Get();
                    Coin_2.transform.position = instanPoss[posNum].position; break;
                // ====================================================================
                case "coin_3":
                    var Coin_3 = coinPool_3.Get();
                    Coin_3.transform.position = instanPoss[posNum].position; break;

                default: Instantiate(instanCoin, instanPoss[posNum].position, Quaternion.identity); break;
            }  // ���� ����
        }
        if (obstacle != "None") Instantiate(instanObstacle, instanPoss[0].position, Quaternion.identity);
        if (platform != "None") Instantiate(instanPlatform, instanPoss[platformPos].position, Quaternion.identity);

        GameManager.GM.data.curRunRatio += 1;
    }

    IEnumerator CoinMaker_1()
    {
        var chapter_EX = chapterEX_1;
        switch (GameManager.GM.data.branch_GM)
        {
            case <= 10: chapter_EX = chapterEX_1; break;
            case <= 20: chapter_EX = chapterEX_2; break;
            case <= 30: chapter_EX = chapterEX_3; break;
            case <= 40: chapter_EX = chapterEX_4; break;
            case <= 50: chapter_EX = chapterEX_5; break;
            case <= 60: chapter_EX = chapterEX_6; break;
        }

        isMaker = true;

        CoinType(chapter_EX.stage_1[index].coinType);
        ObstacleType(chapter_EX.stage_1[index].obstacle);
        PlatformType(chapter_EX.stage_1[index].platform);
        if (GameManager.GM.playerAlive == false)
        {
            posNum = chapter_EX.stage_1[index].coinPos; // ���� ���̰� ����
            for (int i = 0; i < chapter_EX.stage_1[index].coinAmount; i++) // ���� ������ŭ �ݺ�
            {
                CoinAmount(chapter_EX.stage_1[index].coinType, chapter_EX.stage_1[index].obstacle, chapter_EX.stage_1[index].platform, chapter_EX.stage_1[index].platformPos);
                yield return new WaitForSeconds(late_Time);
            }
        }
        index++;
        coinSkip = false;
        isMaker = false;
        yield return null;

    }
    IEnumerator CoinMaker_2()
    {
        var chapter_EX = chapterEX_1;
        switch (GameManager.GM.data.branch_GM)
        {
            case <= 10: chapter_EX = chapterEX_1; break;
            case <= 20: chapter_EX = chapterEX_2; break;
            case <= 30: chapter_EX = chapterEX_3; break;
            case <= 40: chapter_EX = chapterEX_4; break;
            case <= 50: chapter_EX = chapterEX_5; break;
            case <= 60: chapter_EX = chapterEX_6; break;
        }

        if (GameManager.GM.playerAlive == false)
        {
            isMaker = true;

            CoinType(chapter_EX.stage_2[index].coinType);
            ObstacleType(chapter_EX.stage_2[index].obstacle);
            PlatformType(chapter_EX.stage_2[index].platform);

            posNum = chapter_EX.stage_2[index].coinPos; // ���� ���̰� ����
            for (int i = 0; i < chapter_EX.stage_2[index].coinAmount; i++) // ���� ������ŭ �ݺ�
            {
                CoinAmount(chapter_EX.stage_2[index].coinType, chapter_EX.stage_2[index].obstacle, chapter_EX.stage_2[index].platform, chapter_EX.stage_2[index].platformPos);
                yield return new WaitForSeconds(late_Time);
            }
            index++;
            coinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
}
