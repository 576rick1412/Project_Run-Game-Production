using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_CS : MonoBehaviour
{
    [SerializeField]private string Map_str;
    [SerializeField]private float startPos;
    [SerializeField]private float endPos;
    void Update()
    {
        switch(Map_str)
        {
            case "Floor":
                transform.Translate(-1 * GameManager.GM.Data.Floor_SpeedValue * Time.deltaTime, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0);
                break;
            case "BGI_1":
                transform.Translate(-1 * GameManager.GM.Data.BGI_SpeedValue * Time.deltaTime, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0);
                break;
            case "BGI_2":
                transform.Translate(-1 * GameManager.GM.Data.BGI_SpeedValue * Time.deltaTime * 0.5f, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0);
                break;
            case "BGI_3":
                transform.Translate(-1 * GameManager.GM.Data.BGI_SpeedValue * Time.deltaTime * 0.2f, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0);
                break;
            case "Obstacle":
                transform.Translate(-1 * GameManager.GM.Data.Floor_SpeedValue * Time.deltaTime, 0, 0);
                break;
            case "Platform":
                transform.Translate(-1 * GameManager.GM.Data.Floor_SpeedValue * Time.deltaTime, 0, 0);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Border") && Map_str == "Obstacle")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("End_Border") &&  Map_str == "Platform")
        {
            Destroy(this.gameObject);
        }
    }
}
