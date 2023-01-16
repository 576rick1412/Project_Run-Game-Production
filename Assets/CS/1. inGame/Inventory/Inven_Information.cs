using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inven_Information : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmount;
    public TextMeshProUGUI itemInformation;
    public Image icon;

    //public static Inven_Information II;
    //void Awake() { II = this; }
    void Start()
    {
        itemName.text = "";
        itemAmount.text = "";
        itemInformation.text = "";
    }

    void Update()
    {

    }
}