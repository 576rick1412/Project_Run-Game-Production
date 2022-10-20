using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryDB : MonoBehaviour
{
    public Item_Data[] ������;

    public static InventoryDB IV;
    void Awake() { IV = this; }

    [System.Serializable]
    public struct Item_Data
    {
        public string Itemname;         // ������ �̸�

        public GameObject ItemObject;   // ������ ��ü
        public int ItemAmount;          // ������ ��
    }
}
