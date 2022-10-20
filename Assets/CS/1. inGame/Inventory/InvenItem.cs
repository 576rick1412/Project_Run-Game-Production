using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvenItem : MonoBehaviour
{
    [SerializeField] int SetItem;
    [SerializeField] TextMeshProUGUI Amount;
    void Start()
    {
        Amount.text = InventoryDB.IV.æ∆¿Ã≈€[SetItem].ItemAmount.ToString();
    }
}
