using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryDB : MonoBehaviour
{
    public Item_Data[] 아이템;

    public static InventoryDB IV;
    void Awake() { IV = this; }

    [System.Serializable]
    public struct Item_Data
    {
        public string Itemname;         // 아이템 이름

        public GameObject ItemObject;   // 아이템 본체
        public int ItemAmount;          // 아이템 수
    }
}
