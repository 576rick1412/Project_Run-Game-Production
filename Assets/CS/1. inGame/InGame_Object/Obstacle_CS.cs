using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Obstacle_CS : MonoBehaviour
{
    [SerializeField]private int SetDamageNum;
    private int ObstacleDamage;

    private IObjectPool<Obstacle_CS> _ObstaclePool;
    public void Set_ObstaclePool(IObjectPool<Obstacle_CS> pool) {_ObstaclePool = pool;}
    public void DestroyObstacle() { _ObstaclePool.Release(this); }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1 * GameManager.GM.Floor_SpeedValue * Time.deltaTime, 0, 0);

        switch (SetDamageNum)
        {
            case 1: ObstacleDamage = GameManager.GM.Get_Obstacle_Damage_1; break;
            case 2: ObstacleDamage = GameManager.GM.Get_Obstacle_Damage_2; break;
            case 3: ObstacleDamage = GameManager.GM.Get_Obstacle_Damage_3; break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Border"))
        {
            DestroyObstacle();
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
        Debug.Log("무적 종료");
    }
}
