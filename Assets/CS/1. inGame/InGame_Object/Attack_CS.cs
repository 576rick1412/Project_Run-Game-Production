using UnityEngine;
using UnityEngine.Pool;

public class Attack_CS : MonoBehaviour
{
    private int AttackDamage;

    void Awake() { AttackDamage = GameManager.GM.Get_Player_Damage_1; }

    private IObjectPool<Attack_CS> _AttackPool;
    public void Set_AttackPool(IObjectPool<Attack_CS> pool) { _AttackPool = pool; }
    public void DestroyAttack() { _AttackPool.Release(this); }
    void Update()
    {
        transform.Translate((GameManager.GM.Floor_SpeedValue * GameManager.GM.Get_Attack_Speed_1) * Time.deltaTime, 0, 0);
        
        /*switch (SetDamage)
        {
            case 1: SetDamage = GameManager.GM.Get_Player_Damage_1; break;
            case 2: SetDamage = GameManager.GM.Get_Player_Damage_2; break;
            case 3: SetDamage = GameManager.GM.Get_Player_Damage_3; break;
        }*/
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Start_Border")) { DestroyAttack(); }

        if (collision.gameObject.CompareTag("Boss")) { GameManager.GM.Boss_HP -= AttackDamage; DestroyAttack(); }
    }
}
