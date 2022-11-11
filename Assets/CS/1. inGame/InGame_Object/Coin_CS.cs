using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Coin_CS : MonoBehaviour
{
    [SerializeField] string SetObject;
    [SerializeField] bool SetPrefabs; // ������ �ȿ� �ִ� ������Ʈ��� üũ
    bool gameClear;
    [SerializeField] bool Magnet;
    Vector2 speed = new Vector2(80f, 50f);

    private IObjectPool<Coin_CS> _CoinPool_1;
    private IObjectPool<Coin_CS> _CoinPool_2;
    private IObjectPool<Coin_CS> _CoinPool_3;
    public void Set_CoinPool_1(IObjectPool<Coin_CS> pool_1) { _CoinPool_1 = pool_1; }
    public void Set_CoinPool_2(IObjectPool<Coin_CS> pool_2) { _CoinPool_2 = pool_2; }
    public void Set_CoinPool_3(IObjectPool<Coin_CS> pool_3) { _CoinPool_3 = pool_3; }

    void Update() 
    {
        Vector2 Player_Pos = Player_CS.PL.Player_Pos.position;
        if (Magnet) { transform.position = Vector2.SmoothDamp(transform.position, Player_Pos, ref speed, 0.1f); Debug.Log("�����̴���!"); return; }

        if (SetPrefabs) return;
        transform.Translate(-1 * GameManager.GM.Data.Floor_SpeedValue * Time.deltaTime, 0, 0);
    }

    public void DestroyCoin_1() { _CoinPool_1.Release(this); return; }
    public void DestroyCoin_2() { _CoinPool_2.Release(this); return; }
    public void DestroyCoin_3() { _CoinPool_3.Release(this); return; }

    private void Destroy()
    {
        switch (SetObject)
        {
            case "Nomal_Ice":   DestroyCoin_1(); break;               // �Ϲ� ���� * 1
            case "Hard_Ice":    DestroyCoin_2(); break;               // �ܴ��� ���� * 5
            case "Special_Ice": DestroyCoin_3(); break;               // �׸� ���� * 50
            case "Hub"          : Destroy(this.gameObject); break;      // ���
            case "Obstacle"     : Destroy(this.gameObject); break;      // ��ֹ�
            case "Prefab_Ice"   : Destroy(this.gameObject); break;      // ������Ʈ Ǯ�� ���� �� �ǵ��� ���� �ݸ�
            case "Platform"     : Destroy(this.gameObject); break;      // ����
            case "LastPoint"    : Destroy(this.gameObject); break;      // ����
        }
    }
    void LifeUp(int point)
    {
        GameManager.GM.Data.LifeScore += point;
        if (GameManager.GM.Data.LifeScore >= GameManager.GM.Data.Set_LifeScore)
        { GameManager.GM.Data.LifeScore = GameManager.GM.Data.Set_LifeScore; }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { Magnet = false; speed = new Vector2(80f, 50f); }

        // ������ �÷��̾� �ڷ� ���� �� �������� ���� ���� / �÷��̾� �ڿ� �ݶ��̴��� ����� �ذ���
        if (collision.gameObject.CompareTag("Coin_Base") && SetObject == "Nomal_Ice") { Magnet = false; speed = new Vector2(80f, 50f); LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }
        if (collision.gameObject.CompareTag("Coin_Base") && SetObject == "Hard_Ice") { Magnet = false; speed = new Vector2(80f, 50f); LifeUp(2); GameManager.GM.Data.CoinScore += (GameManager.GM.Data.Coin_Point * 5); Destroy(); }
        if (collision.gameObject.CompareTag("Coin_Base") && SetObject == "Special_Ice") { Magnet = false; speed = new Vector2(80f, 50f); LifeUp(1); GameManager.GM.Data.CoinScore += (GameManager.GM.Data.Coin_Point * 50); Destroy(); }
        if (collision.gameObject.CompareTag("Coin_Base") && SetObject == "Prefab_Ice") { Magnet = false; speed = new Vector2(80f, 50f); LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }

        if (collision.gameObject.CompareTag("End_Border")) { Destroy(); }

        if (collision.gameObject.CompareTag("Magnet_Borber")) { if (SetObject == "LastPoint" || SetObject == "Obstacle" || SetObject == "Hub") return; Magnet = true; Debug.Log("�ڼ����� �浹!");  }

        if (collision.gameObject.CompareTag("Player") && SetObject == "Nomal_Ice") { LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Hard_Ice") { LifeUp(2); GameManager.GM.Data.CoinScore += (GameManager.GM.Data.Coin_Point * 5); Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Special_Ice") { LifeUp(1); GameManager.GM.Data.CoinScore += (GameManager.GM.Data.Coin_Point * 50); Destroy(); }
        if (collision.gameObject.CompareTag("Player") && SetObject == "Prefab_Ice") { LifeUp(1); GameManager.GM.Data.CoinScore += GameManager.GM.Data.Coin_Point; Destroy(); }

        if (collision.gameObject.CompareTag("Player") && SetObject == "LastPoint") { Player_CS.PL.Clear_Check = true; if(!gameClear) GameEnd(); }

        if (collision.gameObject.CompareTag("Player") && SetObject == "Obstacle" && Player_CS.PL.On_HIT == false) 
        { 
            GameManager.GM.Data.LifeScore -= GameManager.GM.Data.Obstacle_Damage;
            Player_CS.PL.On_HIT = true; 
            Player_CS.PL.OnCoroutine();
            Player_CS.PL.anime.SetInteger("Player_Value", 5);
        }
    }
    void GameEnd()
    {
        Game_Control.GC.Result_Spawn(); GameManager.GM.SavaData(); Debug.Log("���� Ŭ����");
        GameManager.GM.Data.Game_WIN = true; Game_Control.GC.Game_ClearUI();
        gameClear = true;
    }
}
