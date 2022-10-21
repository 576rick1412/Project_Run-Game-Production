using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvenItem : MonoBehaviour
{
    [SerializeField] int SetItem;
    [SerializeField] TextMeshProUGUI Amount;

    Inven_Information II;

    [SerializeField] Image Icon;
    void Start()
    {
        II = FindObjectOfType<Inven_Information>();

        Amount.text = InventoryDB.IV.아이템[SetItem].ItemAmount.ToString();
    }

    public void CheckItem()
    {
        II.Item_Name.text = InventoryDB.IV.아이템[SetItem].Itemname;
        II.Item_Amount.text = InventoryDB.IV.아이템[SetItem].ItemAmount.ToString() + " 개";
        II.Item_Information.text = "sadsadasdasdasd"; // 나중에 엑셀에서 불러오도록 수정할 예정
        II.Icon.sprite = Icon.sprite;
    }
}
