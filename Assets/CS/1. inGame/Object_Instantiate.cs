using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Instantiate : MonoBehaviour
{
    [SerializeField] float Late_Time;

    [SerializeField] Chapter_1 Chapter_EX;

    [SerializeField] GameObject[] Coin_Object;
    [SerializeField] GameObject[] Obstacle_Object;

    [SerializeField] Transform[] Instan_Pos;
    [SerializeField] GameObject Instan_Object;

    [SerializeField] int Index = 0;
    [SerializeField] int PosNum = 0;
    [SerializeField] int Amount = 0;

    [SerializeField] bool CoinSkip = false; // 코인 생성 x 확인용
    [SerializeField] bool isMaker = false; // 코인 생성 x 확인용
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Chapter_EX.Stage_1[Index].END == false) if (isMaker == false) StartCoroutine("Coin_Maker");
    }
    IEnumerator Coin_Maker()
    {
        isMaker = true;

        switch (Chapter_EX.Stage_1[Index].CoinType) // 코인 종류 따라 생성
        {
            case "None": CoinSkip = true; break;
            case "coin_1": Instan_Object = Coin_Object[0]; break;
            case "coin_2": Instan_Object = Coin_Object[1]; break;
            case "coin_3": Instan_Object = Coin_Object[2]; break;
            case "coin_4": Instan_Object = Coin_Object[3]; break;
        }


        switch (Chapter_EX.Stage_1[Index].CoinPos) // 코인 종류 따라 생성
        {
            case 0: PosNum = 0; break;
            case 1: PosNum = 1; break;
            case 2: PosNum = 2; break;
            case 3: PosNum = 3; break;
            case 4: PosNum = 4; break;
            case 5: PosNum = 5; break;
            case 6: PosNum = 6; break;
            case 7: PosNum = 7; break;
        }

        Amount = Chapter_EX.Stage_1[Index].CoinAmount;
        for (int i = 0; i < Amount; i++) // 코인 개수만큼 반복
        {
            if (CoinSkip == true) { yield return new WaitForSeconds(Late_Time); continue; }

            GameObject coin = Instantiate(Instan_Object);
            coin.transform.position = Instan_Pos[PosNum].position;

            yield return new WaitForSeconds(Late_Time);
        }

        Index++;
        isMaker = false;
        yield return null;
    }
}
