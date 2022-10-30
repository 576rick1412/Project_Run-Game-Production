using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_CS : MonoBehaviour
{
    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.Translate(-1 * GameManager.GM.Data.Floor_SpeedValue * Time.smoothDeltaTime, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Border"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.GM.Data.LifeScore += 50;
            if (GameManager.GM.Data.LifeScore >= 100) GameManager.GM.Data.LifeScore = 100;
            Destroy(gameObject);
        }
    }
}