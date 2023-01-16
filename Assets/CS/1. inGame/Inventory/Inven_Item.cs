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
        II.itemAmount.text = InventoryDB.IV.items[setItem].itemAmount.ToString() + " 개";
        II.itemInformation.text = "sadsadasdasdasd"; // 나중에 엑셀에서 불러오도록 수정할 예정
        II.icon.sprite = icon.sprite;
    }
}
