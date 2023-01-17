using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] string setObject;
    bool magnet;
    Vector2 speed;

    void Start()
    { 
        magnet = false;
        speed = new Vector2(30f, 20f);

    }

    void Update() 
    {
        Vector2 Player_Pos = Player.PL.playerPos.position;
        if (magnet) { transform.position = Vector2.SmoothDamp(transform.position, Player_Pos, ref speed, 0.1f); return; }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnet_Borber")) { if (setObject == "obstacle") return; magnet = true; }

        // Coin_Base�� ������ �÷��̾� �ڷ� ���� �� �������� ���� ���� / �÷��̾� �ڿ� �ݶ��̴��� ����� �ذ���
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Coin_Base"))
        {
            magnet = false;
            switch (setObject)
            {
                case "Nomal_Ice":       GetCoin(1, 1);  Destroy(); break;
                case "Hard_Ice":        GetCoin(2, 5);  Destroy(); break;
                case "Special_Ice":     GetCoin(2, 50); Destroy(); break;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
            if (setObject == "obstacle" && !Player.PL.onHit) Obstacle();
    }

    private void Destroy() { Destroy(this.gameObject); }
    void GetCoin(int point, int multiply)
    {
        // ���� ȹ�� �� ���� ���� Ȱ��ȭ
        magnet = false;

        // ���� ȹ�� �� ����� ����
        GameManager.GM.data.lifeScore += point;
        if (GameManager.GM.data.lifeScore >= GameManager.GM.data.setLifeScore)
        { GameManager.GM.data.lifeScore = GameManager.GM.data.setLifeScore; }

        // ���� ȹ�� �� ���� ����
        GameManager.GM.data.coinScore += GameManager.GM.data.coinPoint * multiply;
        Sound_Manager.SM.Coin();
    }
    void Obstacle()
    {
        // �ǰ� �� 2�� ���ظ� �԰�, ��ü ü���� ���������� 1 �پ��
        GameManager.GM.data.lifeScore -= GameManager.GM.data.obstacleDamage;
        GameManager.GM.data.setLifeScore -= 1f;
        Player.PL.onHit = true;
        Player.PL.OnCoroutine();
    }
}
