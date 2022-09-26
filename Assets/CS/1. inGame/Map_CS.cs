using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_CS : MonoBehaviour
{
    [SerializeField]private string Map_str;
    [SerializeField]private float startPos;
    [SerializeField]private float endPos;
    void FixedUpdate()
    {
        switch(Map_str)
        {
            case "Floor":
                transform.Translate(-1 * GameManager.GM.Floor_SpeedValue * Time.deltaTime, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0);
                break;
            case "BGI":
                transform.Translate(-1 * GameManager.GM.BGI_SpeedValue * Time.deltaTime, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0);
                break;
            case "Obstacle":
                transform.Translate(-1 * GameManager.GM.Floor_SpeedValue * Time.deltaTime, 0, 0);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
