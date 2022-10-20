using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inven : MonoBehaviour
{
    int Inven_Index;
    [SerializeField] Image Mask;
    void Start()
    {


        if (InventoryDB.IV.아이템.Length > 24 )
        {
            // 24를 빼면 UpSize가 0이 되길래 21을 빼서 계산하도록 수정함
            int UpSize = Mathf.CeilToInt((InventoryDB.IV.아이템.Length - 21) / 3);

            RectTransform rectTran = gameObject.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width + (280 * UpSize));
        }
        else
        {
            RectTransform rectTran = gameObject.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        }

        Inven_Index = 0;

        for (int i = 0; i < InventoryDB.IV.아이템.Length; i++)
        {
            if(InventoryDB.IV.아이템[Inven_Index].ItemAmount > 0)
            {
                GameObject Item =  Instantiate(InventoryDB.IV.아이템[Inven_Index].ItemObject);
                Item.transform.SetParent(this.transform);
            }
            Inven_Index++;
        }
    }

    void Update()
    {
        
    }
}
