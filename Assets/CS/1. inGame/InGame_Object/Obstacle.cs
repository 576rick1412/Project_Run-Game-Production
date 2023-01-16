using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Obstacle : MonoBehaviour
{
    // 모든 애니메이션 실행 파라미터는 OnObstacle 로 통일
    [SerializeField] string obstacleType;
    Animator anim;
    void Start() { anim = GetComponent<Animator>(); }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Scene_Border"))
        {
            switch(obstacleType)
            {
                case "Slide": anim.SetBool("OnObstacle", true); break;
                case "Eagle"    : anim.SetBool("OnObstacle", true); break;
            }
        }
    }
}
