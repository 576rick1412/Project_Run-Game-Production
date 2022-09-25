using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_CS : MonoBehaviour
{
    [SerializeField]private float startPos;
    [SerializeField]private float endPos;
    void FixedUpdate()
    {
        transform.Translate(-1 * GameManager.GM.MapSpeedValue * Time.deltaTime, 0, 0);
        if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0);
    }
}
