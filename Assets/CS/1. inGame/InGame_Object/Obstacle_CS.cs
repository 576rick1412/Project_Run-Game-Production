using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_CS : MonoBehaviour
{
    [SerializeField]private int SetDamageNum;
    private int ObstacleDamage;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(SetDamageNum)
        {
            case 1: ObstacleDamage = GameManager.GM.Get_Damage_1; break;
            case 2: ObstacleDamage = GameManager.GM.Get_Damage_2; break;
            case 3: ObstacleDamage = GameManager.GM.Get_Damage_3; break;
        }
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
            GameManager.GM.LifeScore -= ObstacleDamage;
            Player_CS.On_HIT = true;
            Invoke("HIT_off", GameManager.GM.Invincibility_Time);
            //gameObject.SetActive(false);
        }
    }
    void HIT_off()
    {
        Player_CS.On_HIT = false;
        Debug.Log("���� ����");
    }
}
