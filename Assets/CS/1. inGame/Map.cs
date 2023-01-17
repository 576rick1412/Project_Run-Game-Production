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
        switch (mapStr)
        {
            case "Floor": MoveMap(1f); break;
            case "BGI_1": MoveMap(1f); break;
            case "BGI_2": MoveMap(0.5f); break;
            case "BGI_3": MoveMap(0.2f); break;
        }

        if (transform.position.x <= endPos)
        {
            if (mapStr == "Floor") transform.Translate(-1 * (endPos - startPos), 0, 0);

            else // ¸ÊÀÌ È­¸é ¹þ¾î³ª¸é »èÁ¦
            {
                GameObject.Find("Map_Proto").GetComponent<Object_Instantiate>().InstanceMap();
                Destroy(this.gameObject);
            }
        }
    }

    void MoveMap(float mult)
    {
        transform.Translate(-1 * GameManager.GM.data.floorSpeedValue * Time.deltaTime * mult, 0, 0);
    }
}
