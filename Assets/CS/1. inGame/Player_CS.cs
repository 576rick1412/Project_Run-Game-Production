using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CS : MonoBehaviour
{
    public bool isJump = false;
    public bool isDoubleJump = false;
    [SerializeField]    float jumpHeight;

    Rigidbody2D rigid;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
    public void Jump()
    {
        if (isJump == true)
        {
            rigid.velocity = Vector2.up * jumpHeight;
            isJump = false;
            return;
        }

        if(isJump == false && isDoubleJump == true)
        {
            rigid.velocity = Vector2.up * jumpHeight;
            isDoubleJump = false;
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isJump = true;
            isDoubleJump = true;
        }
    }
}
