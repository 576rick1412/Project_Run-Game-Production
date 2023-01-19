using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Control : MonoBehaviour
{
    public static Game_Control GC;

    [SerializeField] Transform spawnPos;    // �÷��̾� ���� ��ġ
    [SerializeField] GameObject[] players;  // �÷��̾� ���
    GameObject spanwPlayer;                 // ������ �÷��̾�

    [SerializeField] Image hPBar;           // ü�¹�
    [SerializeField] GameObject pauseImage; // �Ͻ����� ������
    public TextMeshProUGUI score;           // ���� UI
    public TextMeshProUGUI runTime;         // �޸��ð� UI
    float runTimeInt;                         // �޸��ð� ����

    [SerializeField] Transform midSpawnPos;// �ӽ�

    [Header("�Ͻ����� ��ư")]
    public GameObject startIcon;            // ���� ������
    public GameObject pauseIcon;            // �Ͻ����� ������

    [Header("UI")]
    [HideInInspector] public bool isGameEnd;// ���� ���� Ȯ�� 
    [SerializeField] Image bloodScreen;     // �����
    [SerializeField] GameObject result;     // ���â ������
    [SerializeField] GameObject clearUI;    // ���� Ŭ���� ������
    [SerializeField] GameObject overUI;     // ���� ���� ������

    [Header("�ǰ� ȿ��")]
    [SerializeField] float setAlphaValue;  
    [SerializeField] float setEfeectTime;
    Vector3 camPos;

    bool isPause; // �Ͻ�����

    void Awake()
    {
        GC = this;
        PlayerType();

        startIcon.SetActive(false); pauseIcon.SetActive(true);

        pauseImage.SetActive(false);
        isPause = false;

        GameManager.GM.nowClearStar = 0; // ���â ���� �� �� �ʱ�ȭ
        GameManager.GM.data.curRunRatio = 0; // ���� �޸� �Ÿ� �ʱ�ȭ

        GameManager.GM.data.floorSpeedValue = GameManager.GM.data.setFloorSpeedValue; // ���� �ӵ� ������ �ʱⰪ���� �ʱ�ȭ
        GameManager.GM.data.BGSpeedValue = GameManager.GM.data.setBGSpeedValue;     // ��� �ӵ� ������ �ʱⰪ���� �ʱ�ȭ

        // HP ������ �ʱⰪ���� �ʱ�ȭ
        GameManager.GM.data.setLifeScore = GameManager.GM.data.constLifeScore;
        GameManager.GM.data.lifeScore = GameManager.GM.data.setLifeScore;

        GameManager.GM.data.coinScore = 0;  // ���� �� ���� �ʱ�ȭ

        GameManager.GM.playerAlive = false; // �÷��̾� ���ó�� �ʱ�ȭ

        isGameEnd = false;

        camPos = Camera.main.transform.position; // ���� ī�޶� ��ġ�� �־� �ʱ�ȭ
    }

    void Start()
    {

    }

    void PlayerType()
    {
        switch (GameManager.GM.data.playerType)
        {
            case "Player_1": spanwPlayer = players[0]; break;
        }

        Instantiate(spanwPlayer, spawnPos.position, Quaternion.identity);
    }

    void Update()
    {
        runTimeInt += Time.deltaTime;

        hPBar.fillAmount = (GameManager.GM.data.lifeScore / GameManager.GM.data.setLifeScore);
        score.text = "���� : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString());

        runTime.text = "�޸��ð� " + Mathf.RoundToInt(runTimeInt) + " ��";

        if (GameManager.GM.data.lifeScore <= 0 && isGameEnd == false) StartCoroutine(EndGame(false));
    }
    public string CommaText(long score) { return string.Format("{0:#,###}", score); }

    public void Pause_B()
    {
        /*�Ͻ�����?��Ȱ��ȭ*/
        if (isPause == false)
        {
            Time.timeScale = 0;
            pauseImage.SetActive(true);
            startIcon.SetActive(true); pauseIcon.SetActive(false);
            isPause = true; return;
        }

        /*�Ͻ�����?Ȱ��ȭ*/
        if (isPause == true)
        {
            Time.timeScale = 1;
            pauseImage.SetActive(false);
            startIcon.SetActive(false); pauseIcon.SetActive(true);
            isPause = false; return;
        }
    }

    public IEnumerator EndGame(bool isClear)
    {
        GameObject endUI = isClear == true ? clearUI : overUI; // ������ ��� UI ����

        // ���� ���� �� ������ 0���� ���� / Ŭ��� �ƴϱ� ������ ���� �� �� ����
        if (!isClear) GameManager.GM.data.coinScore = 0;

        // Ŭ���� �� ���� ���������� �������� ��, ���� �̹� Ŭ������ ���������� �ٽ� �÷����� ��� �׳� ����
        if (GameManager.GM.data.stageClearNum < GameManager.GM.data.branch_GM && isClear)
        { GameManager.GM.data.stageClearNum = GameManager.GM.data.branch_GM; }

        // ��� UI ����
        GameObject UI = Instantiate(endUI, midSpawnPos.position, Quaternion.identity);
        UI.transform.SetParent(midSpawnPos);

        isGameEnd = true;
        GameManager.GM.data.gameWin = isClear;
        GameManager.GM.SavaData();

        yield return new WaitForSeconds(3f);
        GameManager.GM.Fade(result, true); Player.PL.clearCheck = true;
        yield return null;
    }


    public void ReStage()
    {
        string stageDes = GameManager.GM.STG_Excel();

        Time.timeScale = 1; //  Ÿ�� ������ �ʱ�ȭ, ���� ���� ����
        switch (GameManager.GM.data.branch_GM / 10)
        {
            case <= 10: Loading_Manager.LoadScene("Stage1_Hub", stageDes); break;
            case <= 20: Loading_Manager.LoadScene("Stage2_Hub", stageDes); break;
            case <= 30: Loading_Manager.LoadScene("Stage3_Hub", stageDes); break;
            case <= 40: Loading_Manager.LoadScene("Stage4_Hub", stageDes); break;
            case <= 50: Loading_Manager.LoadScene("Stage5_Hub", stageDes); break;
            case <= 60: Loading_Manager.LoadScene("Stage6_Hub", stageDes); break;
        }
    }
    public void GoRetry()
    {
        Time.timeScale = 1; //  Ÿ�� ������ �ʱ�ȭ, ���� ���� ����
        string stageDes = GameManager.GM.STG_Excel();
        GameManager.GM.Stage_Move("GameScene", "���Ѹ��", stageDes);
    }

    public IEnumerator CameraShake(float magnitude)
    {
        for (int i = 0;i < 10;i++)
        {
            Camera.main.transform.localPosition = Random.insideUnitSphere * magnitude + camPos;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Camera.main.transform.localPosition = camPos; // ���� ī�޶� ��ġ ����
        yield return null;
    }

    public IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, setAlphaValue);
        yield return new WaitForSeconds(setEfeectTime);
        bloodScreen.color = Color.clear;
        yield return null;
    }
}
