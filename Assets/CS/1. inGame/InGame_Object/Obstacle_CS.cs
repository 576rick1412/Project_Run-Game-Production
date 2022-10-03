using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_CS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Border"))
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player") && Player_CS.On_HIT == false)
        {
            GameManager.GM.LifeScore -= GameManager.GM.Get_Damage_1;
            Player_CS.On_HIT = true;
            Invoke("HIT_off", GameManager.GM.Invincibility_Time);
            //gameObject.SetActive(false);
        }
    }
    void HIT_off()
    {
        Player_CS.On_HIT = false;
        Debug.Log("무적 종료");
    }
}
