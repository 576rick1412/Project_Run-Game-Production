using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inven_Item : MonoBehaviour
{
    [SerializeField] int setItem;
    [SerializeField] TextMeshProUGUI amount;

    Inven_Information II;

    [SerializeField] Image icon;
    void Start()
    {
        II = FindObjectOfType<Inven_Information>();

        amount.text = InventoryDB.IV.items[setItem].itemAmount.ToString();
    }

    public void CheckItem()
    {
        II.itemName.text = InventoryDB.IV.items[setItem].itemName;
        II.itemAmount.text = InventoryDB.IV.items[setItem].itemAmount.ToString() + " ��";
        II.itemInformation.text = "sadsadasdasdasd"; // ���߿� �������� �ҷ������� ������ ����
        II.icon.sprite = icon.sprite;
    }
}
