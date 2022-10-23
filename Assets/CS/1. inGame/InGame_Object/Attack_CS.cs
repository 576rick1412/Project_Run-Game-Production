using UnityEngine;
using UnityEngine.Pool;

public class Attack_CS : MonoBehaviour
{
    private int AttackDamage;

    void Awake() { AttackDamage = GameManager.GM.Player_Damage; }

    private IObjectPool<Attack_CS> _AttackPool;
    public void Set_AttackPool(IObjectPool<Attack_CS> pool) { _AttackPool = pool; }
    public void DestroyAttack() { _AttackPool.Release(this); }
    void Update() { transform.Translate((GameManager.GM.Floor_SpeedValue * GameManager.GM.Attack_Speed) * Time.deltaTime, 0, 0); }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Start_Border")) { DestroyAttack(); }

        if (collision.gameObject.CompareTag("Boss")) { GameManager.GM.Boss_HP -= AttackDamage; DestroyAttack(); }
    }
}