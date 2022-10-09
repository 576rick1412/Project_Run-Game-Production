using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MasicMainUI_CS : MonoBehaviour
{
    public TextMeshProUGUI NickName;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI Goods;
    public Image EXE;
    
    void Start()
    {
        StartCoroutine(UI_Renew_COR());
    }


    IEnumerator UI_Renew_COR()
    {
        while (true)
        {
            NickName.text = GameManager.GM.GM_NickName;
            Level.text = "Lv. " + GameManager.GM.GM_Level;
            Money.text = GameManager.GM.GM_Money;
            Goods.text = GameManager.GM.GM_Goods;
            EXE.fillAmount = (GameManager.GM.GM_EXE / GameManager.GM.GM_MAX_EXE);

            yield return new WaitForSeconds(5);
        }
    }
}