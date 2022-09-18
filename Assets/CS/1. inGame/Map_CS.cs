using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_CS : MonoBehaviour
{

    private void Update()
    {
        Singleton oc = GameObject.Find("Hephaestus_Canvas").GetComponent<Singleton>();

        Vector2 CurPos = transform.position;
        Vector2 NextPos = Vector2.left * oc.MSpeed * Time.deltaTime;
        transform.position = CurPos + NextPos;
    }
}
