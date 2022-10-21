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

        Amount.text = InventoryDB.IV.������[SetItem].ItemAmount.ToString();
    }

    public void CheckItem()
    {
        II.Item_Name.text = InventoryDB.IV.������[SetItem].Itemname;
        II.Item_Amount.text = InventoryDB.IV.������[SetItem].ItemAmount.ToString() + " ��";
        II.Item_Information.text = "sadsadasdasdasd"; // ���߿� �������� �ҷ������� ������ ����
        II.Icon.sprite = Icon.sprite;
    }
}
