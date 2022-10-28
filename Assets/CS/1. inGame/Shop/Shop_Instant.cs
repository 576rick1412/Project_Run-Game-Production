using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Instant : MonoBehaviour
{
    int Shop_Index;
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (InventoryDB.IV.������.Length > 12)
        {
            // 24�� ���� UpSize�� 0�� �Ǳ淡 21�� ���� ����ϵ��� ������
            int UpSize = Mathf.CeilToInt((InventoryDB.IV.������.Length - 12) / 3);

            RectTransform rectTran = gameObject.GetComponent<RectTransform>();

            int newwidth = ((Screen.width - 1000) + (260 * UpSize + 400));

            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newwidth);
            rectTransform.position = new Vector2((newwidth), (Screen.height / 2) - 50);
        }
        else
        {
            RectTransform rectTran = gameObject.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width - 1000);
        }

        Shop_Index = 0;

        for (int i = 0; i < InventoryDB.IV.������.Length; i++)
        {
            if (InventoryDB.IV.������[Shop_Index].ItemAmount > 0)
            {
                GameObject Item = Instantiate(InventoryDB.IV.������[Shop_Index].ItemObject);
                Item.transform.SetParent(this.transform);
            }
            Shop_Index++;
        }
    }

    void Update()
    {

    }
}
