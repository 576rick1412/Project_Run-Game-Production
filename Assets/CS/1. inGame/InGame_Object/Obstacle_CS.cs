using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Obstacle_CS : MonoBehaviour
{
    // 모든 애니메이션 실행 파라미터는 OnObstacle 로 통일
    [SerializeField] string Obstacle_Type;
    Animator anim;
    void Start() { anim = GetComponent<Animator>(); }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Scene_Border"))
        {
            switch(Obstacle_Type)
            {
                case "Cactus"   : anim.SetBool("OnObstacle", true); break;
                case "Eagle"    : anim.SetBool("OnObstacle", true); break;
            }
        }
    }
}
