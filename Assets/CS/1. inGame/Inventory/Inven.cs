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


        if (InventoryDB.IV.������.Length > 24 )
        {
            // 24�� ���� UpSize�� 0�� �Ǳ淡 21�� ���� ����ϵ��� ������
            int UpSize = Mathf.CeilToInt((InventoryDB.IV.������.Length - 21) / 3);

            RectTransform rectTran = gameObject.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width + (280 * UpSize));
        }
        else
        {
            RectTransform rectTran = gameObject.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        }

        Inven_Index = 0;

        for (int i = 0; i < InventoryDB.IV.������.Length; i++)
        {
            if(InventoryDB.IV.������[Inven_Index].ItemAmount > 0)
            {
                GameObject Item =  Instantiate(InventoryDB.IV.������[Inven_Index].ItemObject);
                Item.transform.SetParent(this.transform);
            }
            Inven_Index++;
        }
    }

    void Update()
    {
        
    }
}
