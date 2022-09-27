using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_CS : MonoBehaviour
{
    //[SerializeField] string NameTag = "";
    void Start()
    {
        
    }

    // Update is called once per frame
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

        /*Vector2 Obj_Pos = transform.position;
if (collision.gameObject.CompareTag("Start_Border"))
{
    Debug.Log("ÁÂÇ¥ Ãæµ¹");
    GameObject Coin =  Instantiate(Coin_Prefabs, Obj_Pos,Quaternion.identity);
    gameObject.SetActive(false);
}*/
    }
}
