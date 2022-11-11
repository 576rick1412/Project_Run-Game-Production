using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Object_Instantiate : MonoBehaviour
{
    [SerializeField] float Late_Time;

    [SerializeField] Chapter_1 Chapter_EX_1;
    [SerializeField] Chapter_2 Chapter_EX_2;
    [SerializeField] Chapter_3 Chapter_EX_3;
    [SerializeField] Chapter_4 Chapter_EX_4;
    [SerializeField] Chapter_5 Chapter_EX_5;
    [SerializeField] Chapter_6 Chapter_EX_6;

    [SerializeField] GameObject[] Coin_Object;
    [SerializeField] GameObject[] Obstacle_Object;
    [SerializeField] GameObject[] Platform_Object;

    [SerializeField] Transform[] Instan_Pos;

    GameObject Instan_Coin;
    GameObject Instan_Obstacle;
    GameObject Instan_Platform;

    int Index = 0;
    int PosNum = 0;

    bool CoinSkip = false; // 코인 스킵 \ true -> 스킵
    bool isMaker = false; // 코루틴 돌아가고 있는지 확인

    private IObjectPool<Coin_CS> CoinPool_1;
    private IObjectPool<Coin_CS> CoinPool_2;
    private IObjectPool<Coin_CS> CoinPool_3;

    void Awake()
    {
        Run_ratio_reset(); // 게임 달린거리 전체 코인 수 구하는 함수

        CoinPool_1 = new ObjectPool<Coin_CS>(Coin_1_Creat, Coin_1_Get, Coin_1_Releas, Coin_1_Destroy, maxSize: 30);
        CoinPool_2 = new ObjectPool<Coin_CS>(Coin_2_Creat, Coin_2_Get, Coin_2_Releas, Coin_2_Destroy, maxSize: 30);
        CoinPool_3 = new ObjectPool<Coin_CS>(Coin_3_Creat, Coin_3_Get, Coin_3_Releas, Coin_3_Destroy, maxSize: 30);
    }

    private Coin_CS Coin_1_Creat()
    {
        Coin_CS Coin_1 = Instantiate(Instan_Coin).GetComponent<Coin_CS>();
        Coin_1.Set_CoinPool_1(CoinPool_1);
        return Coin_1;
    }                    // (풀링) 코인 1 생성
    private void Coin_1_Get(Coin_CS Coin_1)
    {
        Coin_1.gameObject.SetActive(true);
    }           // (풀링) 코인 활성화
    private void Coin_1_Releas(Coin_CS Coin_1)
    {
        Coin_1.gameObject.SetActive(false);
    }        // (풀링) 코인 비활성화
    private void Coin_1_Destroy(Coin_CS Coin_1)
    {
        Destroy(Coin_1.gameObject);
    }       // (풀링) 코인 삭제


    private Coin_CS Coin_2_Creat()
    {
        Coin_CS Coin = Instantiate(Instan_Coin).GetComponent<Coin_CS>();
        Coin.Set_CoinPool_2(CoinPool_2);
        return Coin;
    }                    // (풀링) 코인 2 생성
    private void Coin_2_Get(Coin_CS Coin_2)
    {
        Coin_2.gameObject.SetActive(true);
    }           // (풀링) 코인 활성화
    private void Coin_2_Releas(Coin_CS Coin_2)
    {
        Coin_2.gameObject.SetActive(false);
    }        // (풀링) 코인 비활성화
    private void Coin_2_Destroy(Coin_CS Coin_2)
    {
        Destroy(Coin_2.gameObject);
    }       // (풀링) 코인 삭제


    private Coin_CS Coin_3_Creat()
    {
        Coin_CS Coin_3 = Instantiate(Instan_Coin).GetComponent<Coin_CS>();
        Coin_3.Set_CoinPool_3(CoinPool_3);
        return Coin_3;
    }                    // (풀링) 코인 3 생성
    private void Coin_3_Get(Coin_CS Coin_3)
    {
        Coin_3.gameObject.SetActive(true);
    }           // (풀링) 코인 활성화
    private void Coin_3_Releas(Coin_CS Coin_3)
    {
        Coin_3.gameObject.SetActive(false);
    }        // (풀링) 코인 비활성화
    private void Coin_3_Destroy(Coin_CS Coin_3)
    {
        Destroy(Coin_3.gameObject);
    }       // (풀링) 코인 삭제

    void Run_ratio_reset()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        switch (GameManager.GM.Data.GM_branch % 10)
        {
            case 1: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_1[0].Coin_Sum; break;
            case 2: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_2[0].Coin_Sum; break;
            case 3: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_3[0].Coin_Sum; break;
            case 4: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_4[0].Coin_Sum; break;
            case 5: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_5[0].Coin_Sum; break;
            case 6: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_6[0].Coin_Sum; break;
            case 7: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_7[0].Coin_Sum; break;
            case 8: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_8[0].Coin_Sum; break;
            case 9: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_9[0].Coin_Sum; break;
            case 0: GameManager.GM.Data.Run_Ratio = Chapter_EX.Stage_10[0].Coin_Sum;break;
        }
    }

    void Update()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <=10: Chapter_EX = Chapter_EX_1; break;
            case <=20: Chapter_EX = Chapter_EX_2; break;
            case <=30: Chapter_EX = Chapter_EX_3; break;
            case <=40: Chapter_EX = Chapter_EX_4; break;
            case <=50: Chapter_EX = Chapter_EX_5; break;
            case <=60: Chapter_EX = Chapter_EX_6; break;
        }


        switch (GameManager.GM.Data.GM_branch % 10)
        {
            case 1: GameEndControl(Chapter_EX.Stage_1[Index].END); break;
            case 2: GameEndControl(Chapter_EX.Stage_2[Index].END); break;
            case 3: GameEndControl(Chapter_EX.Stage_3[Index].END); break;
            case 4: GameEndControl(Chapter_EX.Stage_4[Index].END); break;
            case 5: GameEndControl(Chapter_EX.Stage_5[Index].END); break;
            case 6: GameEndControl(Chapter_EX.Stage_6[Index].END); break;
            case 7: GameEndControl(Chapter_EX.Stage_7[Index].END); break;
            case 8: GameEndControl(Chapter_EX.Stage_8[Index].END); break;
            case 9: GameEndControl(Chapter_EX.Stage_9[Index].END); break;
            case 0: GameEndControl(Chapter_EX.Stage_10[Index].END);break;
        }
    }
    void GameEndControl(bool end)
    {
        if (!end && !isMaker)
        {
            int branch = GameManager.GM.Data.GM_branch;
            Debug.Log(Index);

            switch (branch)
            {
                case 1: StartCoroutine("Coin_Maker_1"); break;
                case 2: StartCoroutine("Coin_Maker_2"); break;
                case 3: StartCoroutine("Coin_Maker_3"); break;
                case 4: StartCoroutine("Coin_Maker_4"); break;
                case 5: StartCoroutine("Coin_Maker_5"); break;
                case 6: StartCoroutine("Coin_Maker_6"); break;
                case 7: StartCoroutine("Coin_Maker_7"); break;
                case 8: StartCoroutine("Coin_Maker_8"); break;
                case 9: StartCoroutine("Coin_Maker_9"); break;
                case 0: StartCoroutine("Coin_Maker_10"); break;
            }
        }
    }

    void CoinType(string Type)
    {
        switch (Type) // 코인 지정
        {
            case "None": CoinSkip = true; break;                // 코인 생성 없음
            case "coin_1": Instan_Coin = Coin_Object[0]; break; // 코인 1
            case "coin_2": Instan_Coin = Coin_Object[1]; break; // 코인 2
            case "coin_3": Instan_Coin = Coin_Object[2]; break; // 코인 3
            case "LastPoint": Instan_Coin = Coin_Object[3]; break;     // 마지막 지점
            case "type_1": Instan_Coin = Coin_Object[4]; break;    // 1번 기믹
            case "type_2": Instan_Coin = Coin_Object[5]; break;    // 2번 기믹
        }
    }
    void ObstacleType(string Type)
    {
        switch (Type) // 장애물 지정
        {
            case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // 점프 장애물
            case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // 더블점프 장애물
            case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // 슬라이드 장애물
        }
    }
    void PlatformType(string Type)
    {
        switch (Type) // 발판 지정
        {
            case "Platform_1": Instan_Platform = Platform_Object[0]; break; // 발판
            case "Platform_2": Instan_Platform = Platform_Object[1]; break; // 발판
            case "Platform_3": Instan_Platform = Platform_Object[2]; break; // 발판
            case "Platform_4": Instan_Platform = Platform_Object[3]; break; // 발판
        }
    }
    void CoinAmount(string Type, string Obstacle, string Platform, int PlatformPos)
    {
        if (CoinSkip == false)
        {
            switch (Type)
            {
                case "None": break;
                // ====================================================================
                case "coin_1":
                    var Coin_1 = CoinPool_1.Get();
                    Coin_1.transform.position = Instan_Pos[PosNum].position; break;
                // ====================================================================
                case "coin_2":
                    var Coin_2 = CoinPool_2.Get();
                    Coin_2.transform.position = Instan_Pos[PosNum].position; break;
                // ====================================================================
                case "coin_3":
                    var Coin_3 = CoinPool_3.Get();
                    Coin_3.transform.position = Instan_Pos[PosNum].position; break;
                // ====================================================================
                case "LastPoint": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                // ====================================================================
                case "type_1": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                // ====================================================================
                case "type_2": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
            }  // 코인 생성
        }
        if (Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
        if (Platform != "None") Instantiate(Instan_Platform, Instan_Pos[PlatformPos].position, Quaternion.identity);

        GameManager.GM.Data.Cur_Run_Ratio += 1;
    }

    IEnumerator Coin_Maker_1()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        isMaker = true;

        CoinType(Chapter_EX.Stage_1[Index].CoinType);
        ObstacleType(Chapter_EX.Stage_1[Index].Obstacle);
        PlatformType(Chapter_EX.Stage_1[Index].Platform);
        if (GameManager.GM.Player_alive == false)
        {
            PosNum = Chapter_EX.Stage_1[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_1[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_1[Index].CoinType, Chapter_EX.Stage_1[Index].Obstacle, Chapter_EX.Stage_1[Index].Platform, Chapter_EX.Stage_1[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
        }
        Index++;
        CoinSkip = false;
        isMaker = false;
        yield return null;

    }
    IEnumerator Coin_Maker_2()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_2[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_2[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_2[Index].Platform);

            PosNum = Chapter_EX.Stage_2[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_2[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_2[Index].CoinType, Chapter_EX.Stage_2[Index].Obstacle, Chapter_EX.Stage_2[Index].Platform, Chapter_EX.Stage_2[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_3()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_3[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_3[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_3[Index].Platform);

            PosNum = Chapter_EX.Stage_3[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_3[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_3[Index].CoinType, Chapter_EX.Stage_3[Index].Obstacle, Chapter_EX.Stage_3[Index].Platform, Chapter_EX.Stage_3[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_4()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_4[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_4[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_4[Index].Platform);

            PosNum = Chapter_EX.Stage_4[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_4[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    CoinAmount(Chapter_EX.Stage_4[Index].CoinType, Chapter_EX.Stage_4[Index].Obstacle, Chapter_EX.Stage_4[Index].Platform, Chapter_EX.Stage_4[Index].PlatformPos);
                    yield return new WaitForSeconds(Late_Time);
                }
                Index++;
                CoinSkip = false;
                isMaker = false;
                yield return null;
            }
        }
    }
    IEnumerator Coin_Maker_5()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_5[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_5[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_5[Index].Platform);

            PosNum = Chapter_EX.Stage_5[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_5[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_5[Index].CoinType, Chapter_EX.Stage_5[Index].Obstacle, Chapter_EX.Stage_5[Index].Platform, Chapter_EX.Stage_5[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_6()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_6[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_6[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_6[Index].Platform);

            PosNum = Chapter_EX.Stage_6[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_6[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_6[Index].CoinType, Chapter_EX.Stage_6[Index].Obstacle, Chapter_EX.Stage_6[Index].Platform, Chapter_EX.Stage_6[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_7()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_7[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_7[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_7[Index].Platform);

            PosNum = Chapter_EX.Stage_7[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_7[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_7[Index].CoinType, Chapter_EX.Stage_7[Index].Obstacle, Chapter_EX.Stage_7[Index].Platform, Chapter_EX.Stage_7[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_8()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_8[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_8[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_8[Index].Platform);

            PosNum = Chapter_EX.Stage_8[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_8[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_8[Index].CoinType, Chapter_EX.Stage_8[Index].Obstacle, Chapter_EX.Stage_8[Index].Platform, Chapter_EX.Stage_8[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_9()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_9[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_9[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_9[Index].Platform);

            PosNum = Chapter_EX.Stage_9[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_9[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_9[Index].CoinType, Chapter_EX.Stage_9[Index].Obstacle, Chapter_EX.Stage_9[Index].Platform, Chapter_EX.Stage_9[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_10()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (GameManager.GM.Data.GM_branch)
        {
            case <= 10: Chapter_EX = Chapter_EX_1; break;
            case <= 20: Chapter_EX = Chapter_EX_2; break;
            case <= 30: Chapter_EX = Chapter_EX_3; break;
            case <= 40: Chapter_EX = Chapter_EX_4; break;
            case <= 50: Chapter_EX = Chapter_EX_5; break;
            case <= 60: Chapter_EX = Chapter_EX_6; break;
        }

        if (GameManager.GM.Player_alive == false)
        {
            isMaker = true;

            CoinType(Chapter_EX.Stage_10[Index].CoinType);
            ObstacleType(Chapter_EX.Stage_10[Index].Obstacle);
            PlatformType(Chapter_EX.Stage_10[Index].Platform);

            PosNum = Chapter_EX.Stage_10[Index].CoinPos; // 코인 높이값 지정
            for (int i = 0; i < Chapter_EX.Stage_10[Index].CoinAmount; i++) // 코인 개수만큼 반복
            {
                CoinAmount(Chapter_EX.Stage_10[Index].CoinType, Chapter_EX.Stage_10[Index].Obstacle, Chapter_EX.Stage_10[Index].Platform, Chapter_EX.Stage_10[Index].PlatformPos);
                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
}