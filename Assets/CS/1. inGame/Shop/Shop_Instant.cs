using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Instant : MonoBehaviour
{
    int shopIndex;
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (InventoryDB.IV.items.Length > 12)
        {
            // 24�� ���� UpSize�� 0�� �Ǳ淡 21�� ���� ����ϵ��� ������
            int upSize = Mathf.CeilToInt((InventoryDB.IV.items.Length - 12) / 3);

            RectTransform rectTran = gameObject.GetComponent<RectTransform>();

            int reWidth = ((Screen.width - 1000) + (260 * upSize + 400));

            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, reWidth);
            rectTransform.position = new Vector2((reWidth), (Screen.height / 2) - 50);
        }
        else
        {
            RectTransform rectTran = gameObject.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width - 1000);
        }

        shopIndex = 0;

        for (int i = 0; i < InventoryDB.IV.items.Length; i++)
        {
            if (InventoryDB.IV.items[shopIndex].itemAmount > 0)
            {
                GameObject item = Instantiate(InventoryDB.IV.items[shopIndex].itemObject);
                item.transform.SetParent(this.transform);
            }
            shopIndex++;
        }
    }

    void Update()
    {

    }
}
