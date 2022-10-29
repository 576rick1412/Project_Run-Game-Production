using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Obstacle_CS : MonoBehaviour
{
    private int ObstacleDamage;

    private IObjectPool<Obstacle_CS> _ObstaclePool;
    public void Set_ObstaclePool(IObjectPool<Obstacle_CS> pool) {_ObstaclePool = pool;}
    public void DestroyObstacle() { _ObstaclePool.Release(this); }

    void Start() {  ObstacleDamage = GameManager.GM.Obstacle_Damage; }

    // Update is called once per frame
    void Update() { transform.Translate(-1 * GameManager.GM.Floor_SpeedValue * Time.deltaTime, 0, 0); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Border"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player") && Player_CS.On_HIT == false)
        {
            GameManager.GM.LifeScore -= ObstacleDamage;
            Debug.Log("adfsgd");
            Player_CS.On_HIT = true;
            Player_CS.PL.OnCoroutine();
            Invoke("HIT_off", GameManager.GM.Invincibility_Time);
        }
    }
    void HIT_off()
    {
        Player_CS.On_HIT = false;
        Debug.Log("무적 종료");
    }
}
