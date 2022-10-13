using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_CS : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
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
            GameManager.GM.LifeScore += 50;
            gameObject.SetActive(false);
        }
    }
}
