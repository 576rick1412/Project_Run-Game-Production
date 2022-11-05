using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Object_Instantiate : MonoBehaviour
{
    [Header("é�� ����")][SerializeField][Range(1, 6)] int Excel_Num;

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
    int Amount = 0;
    bool BossOn;

    bool CoinSkip = false; // ���� ��ŵ \ true -> ��ŵ
    bool isMaker = false; // �ڷ�ƾ ���ư��� �ִ��� Ȯ��

    private IObjectPool<Coin_CS> CoinPool_1;
    private IObjectPool<Coin_CS> CoinPool_2;
    private IObjectPool<Coin_CS> CoinPool_3;

    void Awake()
    {
        CoinPool_1 = new ObjectPool<Coin_CS>(Coin_1_Creat, Coin_1_Get, Coin_1_Releas, Coin_1_Destroy, maxSize: 30);
        CoinPool_2 = new ObjectPool<Coin_CS>(Coin_2_Creat, Coin_2_Get, Coin_2_Releas, Coin_2_Destroy, maxSize: 30);
        CoinPool_3 = new ObjectPool<Coin_CS>(Coin_3_Creat, Coin_3_Get, Coin_3_Releas, Coin_3_Destroy, maxSize: 30);
    }

    private Coin_CS Coin_1_Creat()
    {
        Coin_CS Coin_1 = Instantiate(Instan_Coin).GetComponent<Coin_CS>();
        Coin_1.Set_CoinPool_1(CoinPool_1);
        return Coin_1;
    }                    // (Ǯ��) ���� 1 ����
    private void Coin_1_Get(Coin_CS Coin_1)
    {
        Coin_1.gameObject.SetActive(true);
    }           // (Ǯ��) ���� Ȱ��ȭ
    private void Coin_1_Releas(Coin_CS Coin_1)
    {
        Coin_1.gameObject.SetActive(false);
    }        // (Ǯ��) ���� ��Ȱ��ȭ
    private void Coin_1_Destroy(Coin_CS Coin_1)
    {
        Destroy(Coin_1.gameObject);
    }       // (Ǯ��) ���� ����


    private Coin_CS Coin_2_Creat()
    {
        Coin_CS Coin = Instantiate(Instan_Coin).GetComponent<Coin_CS>();
        Coin.Set_CoinPool_2(CoinPool_2);
        return Coin;
    }                    // (Ǯ��) ���� 2 ����
    private void Coin_2_Get(Coin_CS Coin_2)
    {
        Coin_2.gameObject.SetActive(true);
    }           // (Ǯ��) ���� Ȱ��ȭ
    private void Coin_2_Releas(Coin_CS Coin_2)
    {
        Coin_2.gameObject.SetActive(false);
    }        // (Ǯ��) ���� ��Ȱ��ȭ
    private void Coin_2_Destroy(Coin_CS Coin_2)
    {
        Destroy(Coin_2.gameObject);
    }       // (Ǯ��) ���� ����


    private Coin_CS Coin_3_Creat()
    {
        Coin_CS Coin_3 = Instantiate(Instan_Coin).GetComponent<Coin_CS>();
        Coin_3.Set_CoinPool_3(CoinPool_3);
        return Coin_3;
    }                    // (Ǯ��) ���� 3 ����
    private void Coin_3_Get(Coin_CS Coin_3)
    {
        Coin_3.gameObject.SetActive(true);
    }           // (Ǯ��) ���� Ȱ��ȭ
    private void Coin_3_Releas(Coin_CS Coin_3)
    {
        Coin_3.gameObject.SetActive(false);
    }        // (Ǯ��) ���� ��Ȱ��ȭ
    private void Coin_3_Destroy(Coin_CS Coin_3)
    {
        Destroy(Coin_3.gameObject);
    }       // (Ǯ��) ���� ����

    void Start()
    {
        BossOn = false;
    }
    void GameEnd()
    {
        Game_Control.GC.BossAttack = false; Game_Control.GC.Game_ClearUI(); Game_Control.GC.Game_End = true; GameManager.GM.SavaData();
        GameManager.GM.Data.Game_Fail = true; Game_Control.GC.Result_Spawn();
    }
    // Update is called once per frame

    void Update()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (Excel_Num)
        {
            case 1:Chapter_EX = Chapter_EX_1; break;
            case 2:Chapter_EX = Chapter_EX_2; break;
            case 3:Chapter_EX = Chapter_EX_3; break;
            case 4:Chapter_EX = Chapter_EX_4; break;
            case 5:Chapter_EX = Chapter_EX_5; break;
            case 6:Chapter_EX = Chapter_EX_6; break;
        }

        switch (GameManager.GM.Data.GM_branch % 10)
        {
            case 1: if (Chapter_EX.Stage_1[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 2: if (Chapter_EX.Stage_2[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 3: if (Chapter_EX.Stage_3[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 4: if (Chapter_EX.Stage_4[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 5: if (Chapter_EX.Stage_5[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 6: if (Chapter_EX.Stage_6[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 7: if (Chapter_EX.Stage_7[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 8: if (Chapter_EX.Stage_8[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 9: if (Chapter_EX.Stage_9[Index].END == true && BossOn == false) { Invoke("GameEnd",4f); BossOn = true;} break;
            case 0:if (Chapter_EX.Stage_10[Index].END == true && BossOn == false)
                { Game_Control.GC.Boss_On = true; BossOn = true; } break;
        }
      
        switch (GameManager.GM.Data.GM_branch % 10)
        {
            case 1: if (Chapter_EX.Stage_1[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_1"); } break;
            case 2: if (Chapter_EX.Stage_2[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_2"); } break;
            case 3: if (Chapter_EX.Stage_3[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_3"); } break;
            case 4: if (Chapter_EX.Stage_4[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_4"); } break;
            case 5: if (Chapter_EX.Stage_5[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_5"); } break;
            case 6: if (Chapter_EX.Stage_6[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_6"); } break;
            case 7: if (Chapter_EX.Stage_7[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_7"); } break;
            case 8: if (Chapter_EX.Stage_7[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_8"); } break;
            case 9: if (Chapter_EX.Stage_7[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_9"); } break;
            case 0: if (Chapter_EX.Stage_7[Index].END == false) if (isMaker == false) {Debug.Log(Index); StartCoroutine("Coin_Maker_10"); }break;
        }
    }
    IEnumerator Coin_Maker_1()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_1[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP
            }

            switch (Chapter_EX.Stage_1[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_1[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            PosNum = Chapter_EX.Stage_1[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_1[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) 
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
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
        var Chapter_EX = Chapter_EX_1;
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_2[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_2[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_2[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_2[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_2[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
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
    IEnumerator Coin_Maker_3()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_3[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP":     Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_3[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_3[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_3[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_3[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_3[Index].CoinType)
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
                }
                if (Chapter_EX.Stage_3[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_3[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_3[Index].PlatformPos].position, Quaternion.identity);

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
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_4[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_4[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_4[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_4[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_4[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_4[Index].CoinType)
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
                }
                if (Chapter_EX.Stage_4[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_4[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_4[Index].PlatformPos].position, Quaternion.identity);

                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
    IEnumerator Coin_Maker_5()
    {
        var Chapter_EX = Chapter_EX_1;
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_5[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_5[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_5[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_5[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_5[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_5[Index].CoinType)
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
                }
                if (Chapter_EX.Stage_5[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_5[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_5[Index].PlatformPos].position, Quaternion.identity);

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
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_6[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_6[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_6[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_6[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_6[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_6[Index].CoinType)
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
                }
                if (Chapter_EX.Stage_6[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_6[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_6[Index].PlatformPos].position, Quaternion.identity);

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
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_7[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_7[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_7[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_7[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_7[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_7[Index].CoinType)
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
                }
                if (Chapter_EX.Stage_7[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_7[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_7[Index].PlatformPos].position, Quaternion.identity);

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
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_8[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_8[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_8[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_8[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_8[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_8[Index].CoinType)
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
                }
                if (Chapter_EX.Stage_8[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_8[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_8[Index].PlatformPos].position, Quaternion.identity);

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
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_9[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_9[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_9[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_9[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_9[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_9[Index].CoinType)
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
                }
                if (Chapter_EX.Stage_9[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_9[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_9[Index].PlatformPos].position, Quaternion.identity);

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
        switch (Excel_Num)
        {
            case 1: Chapter_EX = Chapter_EX_1; break;
            case 2: Chapter_EX = Chapter_EX_2; break;
            case 3: Chapter_EX = Chapter_EX_3; break;
            case 4: Chapter_EX = Chapter_EX_4; break;
            case 5: Chapter_EX = Chapter_EX_5; break;
            case 6: Chapter_EX = Chapter_EX_6; break;
        }

        if (Player_CS.PL.Player_alive == false)
        {
            isMaker = true;

            switch (Chapter_EX.Stage_10[Index].CoinType) // ���� ����
            {
                case "None": CoinSkip = true; break;                // ���� ���� ����
                case "coin_1": Instan_Coin = Coin_Object[0]; break; // ���� 1
                case "coin_2": Instan_Coin = Coin_Object[1]; break; // ���� 2
                case "coin_3": Instan_Coin = Coin_Object[2]; break; // ���� 3
                case "HP": Instan_Coin = Coin_Object[3]; break;    // HP ȸ��
                case "Double": Instan_Coin = Coin_Object[4]; break;    // �������� 
                case "Double_HP": Instan_Coin = Coin_Object[5]; break;    // �������� + HP

            }

            switch (Chapter_EX.Stage_10[Index].Obstacle) // ��ֹ� ����
            {
                case "Obstacle_1": Instan_Obstacle = Obstacle_Object[0]; break; // ���� ��ֹ�
                case "Obstacle_2": Instan_Obstacle = Obstacle_Object[1]; break; // �������� ��ֹ�
                case "Obstacle_3": Instan_Obstacle = Obstacle_Object[2]; break; // �����̵� ��ֹ�
            }

            switch (Chapter_EX.Stage_10[Index].Platform) // ���� ����
            {
                case "Platform_1": Instan_Platform = Platform_Object[0]; break; // ����
                case "Platform_2": Instan_Platform = Platform_Object[1]; break; // ����
                case "Platform_3": Instan_Platform = Platform_Object[2]; break; // ����
                case "Platform_4": Instan_Platform = Platform_Object[3]; break; // ����
            }

            /*if (Chapter_EX.Stage_1[Index].Platform != "None")
                Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_1[Index].PlatformPos].position, Quaternion.identity);*/

            PosNum = Chapter_EX.Stage_10[Index].CoinPos; // ���� ���̰� ����
            Amount = Chapter_EX.Stage_10[Index].CoinAmount;
            for (int i = 0; i < Amount; i++) // ���� ������ŭ �ݺ�
            {
                if (CoinSkip == false) // { yield return new WaitForSeconds(Late_Time); continue; }
                {
                    switch (Chapter_EX.Stage_10[Index].CoinType)
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
                        case "Double_HP": Instantiate(Instan_Coin, Instan_Pos[PosNum].position, Quaternion.identity); break;
                    }  // ���� ����
                }
                if (Chapter_EX.Stage_10[Index].Obstacle != "None") Instantiate(Instan_Obstacle, Instan_Pos[0].position, Quaternion.identity);
                if (Chapter_EX.Stage_10[Index].Platform != "None") Instantiate(Instan_Platform, Instan_Pos[Chapter_EX.Stage_10[Index].PlatformPos].position, Quaternion.identity);

                yield return new WaitForSeconds(Late_Time);
            }
            Index++;
            CoinSkip = false;
            isMaker = false;
            yield return null;
        }
    }
}