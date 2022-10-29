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

        GameManager.GM.Boss_HP = GameManager.GM.Set_Boss_HP;
        BossTMP.text = BossName; 
        if(Player_CS.Onalive == false) InvokeRepeating("InstanPattern", 5f, 5f);
    }

    void Update()
    {
        Boss_HP_Bar.fillAmount = (GameManager.GM.Boss_HP / GameManager.GM.Set_Boss_HP);
        if (GameManager.GM.Boss_HP <= 0 && Game_Control.GC.Game_End == false) { GameManager.GM.Boss_DIE = true; anime.SetInteger("BossControl", 1); Destroy(Boss); // Invoke("DestroyBoss", 2f);
            Game_Control.GC.BossAttack = false; Game_Control.GC.Game_ClearUI(); Game_Control.GC.Game_End = true; Game_Control.GC.Result_Spawn();
        }
    }
    void DestroyBoss() { Destroy(Boss); }
    void InstanPattern() { Instantiate(BossPattern, BossPatternPos.position, Quaternion.identity); }
}
