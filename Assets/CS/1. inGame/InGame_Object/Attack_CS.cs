using UnityEngine;
using UnityEngine.Pool;

public class Attack_CS : MonoBehaviour
{
    private int AttackDamage;

    void Awake() { AttackDamage = GameManager.GM.Data.Player_Damage; }

    private IObjectPool<Attack_CS> _AttackPool;
    public void Set_AttackPool(IObjectPool<Attack_CS> pool) { _AttackPool = pool; }
    public void DestroyAttack() { _AttackPool.Release(this); }
    void FixedUpdate() { transform.Translate((GameManager.GM.Data.Floor_SpeedValue * GameManager.GM.Data.Attack_Speed) * Time.smoothDeltaTime, 0, 0); }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Start_Border")) { DestroyAttack(); }

        if (collision.gameObject.CompareTag("Boss")) { GameManager.GM.Data.Boss_HP -= AttackDamage; DestroyAttack(); }
    }
}