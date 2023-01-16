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
        public string itemName;         // ������ �̸�

        public GameObject itemObject;   // ������ ��ü
        public int itemAmount;          // ������ ��
    }
}
