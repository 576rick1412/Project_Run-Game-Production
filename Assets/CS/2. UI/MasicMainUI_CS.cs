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
            NickName.text = GameManager.GM.Data.GM_NickName;
            Level.text = "Lv. " + GameManager.GM.Data.GM_Level;
            Money.text = "" + GameManager.GM.Data.GM_Money;
            Goods.text = "" + GameManager.GM.Data.GM_Goods;
            EXE.fillAmount = (GameManager.GM.Data.GM_EXE / (GameManager.GM.Data.GM_MAX_EXE + (GameManager.GM.Data.GM_Level * 200)));
            // 기본 최종치( 1000 ) + ( 현재 레벨 * 200 ) = 최종 경험치 요구량
            // 만약 레벨이 5 일 경우 -> [ 1000 + ( 5 * 200 ) ] => [ 1000 + 1000 ] = 2000
            yield return new WaitForSeconds(1);
        }
    }
}