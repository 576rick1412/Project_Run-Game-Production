using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_CS : MonoBehaviour
{
    [SerializeField] private int SetPoint;
    private int CoinPoint;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(-1 * GameManager.GM.Floor_SpeedValue * Time.deltaTime, 0, 0);

        switch (SetPoint)
        {
            case 1: CoinPoint = GameManager.GM.Get_Coin_1; break;
            case 2: CoinPoint = GameManager.GM.Get_Coin_2; break;
            case 3: CoinPoint = GameManager.GM.Get_Coin_3; break;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Border"))
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.GM.CoinScore += CoinPoint;
            gameObject.SetActive(false);
        }

        /*Vector2 Obj_Pos = transform.position;
if (collision.gameObject.CompareTag("Start_Border"))
{
    Debug.Log("ÁÂÇ¥ Ãæµ¹");
    GameObject Coin =  Instantiate(Coin_Prefabs, Obj_Pos,Quaternion.identity);
    gameObject.SetActive(false);
}*/
    }
}
