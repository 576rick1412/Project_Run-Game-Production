using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]private string mapStr;
    [SerializeField]private float startPos;
    [SerializeField]private float endPos;
    void Update()
    {
        switch(mapStr)
        {
            case "Floor": transform.Translate(-1 * GameManager.GM.data.floorSpeedValue * Time.deltaTime, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0); break;

            case "BGI_1": transform.Translate(-1 * GameManager.GM.data.BGSpeedValue * Time.deltaTime, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0); break;

            case "BGI_2": transform.Translate(-1 * GameManager.GM.data.BGSpeedValue * Time.deltaTime * 0.5f, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0); break;

            case "BGI_3": transform.Translate(-1 * GameManager.GM.data.BGSpeedValue * Time.deltaTime * 0.2f, 0, 0);
                if (transform.position.x <= endPos) transform.Translate(-1 * (endPos - startPos), 0, 0); break;
        }
    }
}
