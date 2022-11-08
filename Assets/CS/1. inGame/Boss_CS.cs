using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Boss_CS : MonoBehaviour
{
    Animator anime;

    public GameObject Boss;
    public GameObject BossPattern;

    public Transform BossPatternPos;
    [SerializeField] Image Boss_HP_Bar;

    [SerializeField] string BossName;
    [SerializeField] TextMeshProUGUI BossTMP;

    void Start()
    {
        anime = GetComponent<Animator>();

        GameManager.GM.Data.Boss_HP = GameManager.GM.Data.Set_Boss_HP;
        BossTMP.text = BossName; 
        if(Player_CS.PL.Player_alive == false) InvokeRepeating("InstanPattern", 3f, 3f);
    }

    void Update()
    {
        Boss_HP_Bar.fillAmount = (GameManager.GM.Data.Boss_HP / GameManager.GM.Data.Set_Boss_HP);
        if (GameManager.GM.Data.Boss_HP <= 0 && Game_Control.GC.Game_End == false) 
        { 
            GameManager.GM.Data.Boss_DIE = true; Destroy(Boss);
            Game_Control.GC.Game_ClearUI(); Game_Control.GC.Game_End = true;  GameManager.GM.SavaData();
            GameManager.GM.Data.Game_Fail = true; Game_Control.GC.Result_Spawn();
        }
    }
    void InstanPattern() { Instantiate(BossPattern, BossPatternPos.position, Quaternion.identity); }
}
