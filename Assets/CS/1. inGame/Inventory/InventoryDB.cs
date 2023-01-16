using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDB : MonoBehaviour
{
    public ItemData[] items;

    public static InventoryDB IV;
    void Awake() { IV = this; }

    [System.Serializable]
    public struct ItemData
    {
        public string itemName;         // 아이템 이름

        public GameObject itemObject;   // 아이템 본체
        public int itemAmount;          // 아이템 수
    }
}
