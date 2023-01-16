using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_CS : MonoBehaviour
{
    [SerializeField] private BoxCollider2D[] colliders;
    void Start()
    {
        this.colliders = GetComponents<BoxCollider2D>();

        colliders[0].enabled = true;
        colliders[1].enabled = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("players"))
        {
            colliders[0].enabled = false;
            colliders[1].enabled = true;
        }
    }
}
