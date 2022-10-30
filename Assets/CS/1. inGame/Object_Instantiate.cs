using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Object_Instantiate : MonoBehaviour
{
    [SerializeField] float Late_Time;

    [SerializeField] Chapter_1 Chapter_EX;

    [SerializeField] GameObject[] Coin_Object;
    [SerializeField] GameObject[] Obstacle_Object;
    [SerializeField] GameObject[] Platform_Object;

    [SerializeField] Transform[] Instan_Pos;

    [SerializeField] GameObject Instan_Coin;
    [SerializeField] GameObject Instan_Obstacle;
    [SerializeField] GameObject Instan_Platform;

    [SerializeField] int Index = 0;
    [SerializeField] int PosNum = 0;
    [SerializeField] int Amount = 0;
    bool BossOn;

    bool CoinSkip = false; // 코인 스킵 \ true -> 스킵
    [SerializeField] bool isMaker = false; // 코루틴 돌아가고 있는지 확인

    private IObjectPool<Obstacle_CS> ObstaclePool;
    private IObjectPool<Coin_CS> CoinPool_1;
    private IObjectPool<Coin_CS> CoinPool_2;
    private IObjectPool<Coin_CS> CoinPool_3;

    void Awake()
    {
        //ObstaclePool = new ObjectPool<Obstacle_CS>(Obstacle_Creat, Obstacle_Get, Obstacle_Releas, Obstacle_Destroy, maxSize: 10);
        CoinPool_1 = new ObjectPool<Coin_CS>(Coin_1_Creat, Coin_1_Get, Coin_1_Releas, Coin_1_Destroy, maxSize: 30);
        CoinPool_2 = new ObjectPool<Coin_CS>(Coin_2_Creat, Coin_2_Get, Coin_2_Releas, Coin_2_Destroy, maxSize: 30);
        CoinPool_3 = new ObjectPool<Coin_CS>(Coin_3_Creat, Coin_3_Get, Coin_3_Releas, Coin_3_Destroy, maxSize: 30);
    }
    private Obstacle_CS Obstacle_Creat()
    {
        Obstacle_CS Obstacle = Instantiate(Instan_Obstacle).GetComponent<Obstacle_CS>();
        Obstacle.Set_ObstaclePool(ObstaclePool);
        return Obstacle;
    }                       // (풀링) 장애물 생성
    private void Obstacle_Get(Obstacle_CS Obstacle)
    {
        Obstacle.gameObject.SetActive(true);
    }           // (풀링) 장애물 활성화
    private void Obstacle_Releas(Obstacle_CS Obstacle)
    {
        Obstacle.gameObject.SetActive(false);
    }        // (풀링) 장애물 비활성화
    private void Obstacle_Destroy(Obstacle_CS Obstacle)
    {
        Destroy(Obstacle.gameObject);
    }       // (풀링) 장애물 삭제


    private Coin_CS Coin_1_Creat()
    {
        Coin_CS Coin_1 = Instantiate(Instan_Coin).GetComponent<Coin_CS>();
        Coin_1.Set_CoinPool_1(CoinPool_1);
        return Coin_1;
    }                  // (풀링) 코인 1 생성
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
    }                  // (풀링) 코인 2 생성
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
    }                  // (풀링) 코인 3 생성
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




    void Start()
    {
        BossOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.GM.GM_branch)
        {
            case 1: if (Chapter_EX.Stage_1[Index].END == true && BossOn == false) { Game_Control.GC.Boss_On = true; BossOn = true; } break;
            case 2: if (Chapter_EX.Stage_2[Index].END == true && BossOn == false) { Game_Control.GC.Boss_On = true; BossOn = true; } break;
            case 3: if (Chapter_EX.Stage_3[Index].END == true && BossOn == false) { Game_Control.GC.Boss_On = true; BossOn = true; } break;
            case 4: if (Chapter_EX.Stage_4[Index].END == true && BossOn == false) { Game_Control.GC.Boss_On = true; BossOn = true; } break;
        }
      
    
        switch (GameManager.GM.GM_branch)
        {
            case 1: if (Chapter_EX.Stage_1[Index].END == false) if (isMaker == false) StartCoroutine("Coin_Maker_1"); break;
            case 2: if (Chapter_EX.Stage_2[Index].END == false) if (isMaker == false) StartCoroutine("Coin_Maker_2"); break;
            case 3: if (Chapter_EX.Stage_3[Index].END == false) if (isMaker == false) StartCoroutine("Coin_Maker_3"); break;
            case 4: if (Chapter_EX.Stage_4[Index].END == false) if (isMaker == false) StartCoroutine("Coin_Maker_4"); break;
        }
    }
    IEnumerator Coin_Maker_1()
    {
        if (Player_CS.Onalive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_1[Index].CoinType) // 코인 지정
            {
                case "None": CoinSkip = true; break;                // 코인 생성 없음
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // 코인 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // 코인 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // 코인 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP 회복
                case "Double": Instan_Coin = Coin_Object[4]; break;    // 더블코인 
            }

            switch (Chapter_EX.Stage_1[Index].Obstacle) // 장애물 지정
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // 점프 장애물
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // 더블점프 장애물
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // 슬라이드 장애물
            }

            switch (Chapter_EX.Stage_1[Index].Platform) // 발판 지정
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // 발판
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // 발판
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // 발판
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // 발판
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_1[Index].CoinPos; // 코인 높이값 지정
            Amount = Chapter_EX.Stage_1[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // 코인 개수만큼 반복
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_1[Index].CoinType)
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
                        case "HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                        // ====================================================================
                        case "Double": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                        // ====================================================================
                    }  // 코인 생성
                }
                if(Chapter_EX.Stage_1[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if(Chapter_EX.Stage_1[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);

                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_2()
    {
        if (Player_CS.Onalive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_2[Index].CoinType) // 코인 지정
            {
                case "None": CoinSkip = true; break;                // 코인 생성 없음
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // 코인 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // 코인 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // 코인 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP 회복
                case "Double": Instan_Coin = Coin_Object[4]; break;    // 더블코인 

            }

            switch (Chapter_EX.Stage_2[Index].Obstacle) // 장애물 지정
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // 점프 장애물
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // 더블점프 장애물
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // 슬라이드 장애물
            }

            switch (Chapter_EX.Stage_2[Index].Platform) // 발판 지정
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // 발판
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // 발판
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // 발판
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // 발판
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_2[Index].CoinPos; // 코인 높이값 지정
            Amount = Chapter_EX.Stage_2[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // 코인 개수만큼 반복
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_2[Index].CoinType)
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
                        case "HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                        // ====================================================================
                        case "Double": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                        // ====================================================================
                    }  // 코인 생성
                }
                if (Chapter_EX.Stage_2[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_2[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_2[Index].PlatformPos].position, Quaternion.identity);

                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
}
