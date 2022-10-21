using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inven_Information : MonoBehaviour
{
    public TextMeshProUGUI Item_Name;
    public TextMeshProUGUI Item_Amount;
    public TextMeshProUGUI Item_Information;
    public Image Icon;

    //public static Inven_Information II;
    //void Awake() { II = this; }
    void Start()
    {
        Item_Name.text = "";
        Item_Amount.text = "";
        Item_Information.text = "";
    }

    void Update()
    {

    }
}