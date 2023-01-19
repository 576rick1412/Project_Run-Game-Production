using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Control : MonoBehaviour
{
    public static Game_Control GC;

    [SerializeField] Transform spawnPos;    // 플레이어 스폰 위치
    [SerializeField] GameObject[] players;  // 플레이어 목록
    GameObject spanwPlayer;                 // 생성할 플레이어

    [SerializeField] Image hPBar;           // 체력바
    [SerializeField] GameObject pauseImage; // 일시정지 프레임
    public TextMeshProUGUI score;           // 점수 UI
    public TextMeshProUGUI runTime;         // 달린시간 UI
    float runTimeInt;                         // 달린시간 숫자

    [SerializeField] Transform midSpawnPos;// 임시

    [Header("일시정지 버튼")]
    public GameObject startIcon;            // 시작 아이콘
    public GameObject pauseIcon;            // 일시정지 아이콘

    [Header("UI")]
    [HideInInspector] public bool isGameEnd;// 게임 종료 확인 
    [SerializeField] Image bloodScreen;     // 블러드씬
    [SerializeField] GameObject result;     // 결과창 프리팹
    [SerializeField] GameObject clearUI;    // 게임 클리어 프리팹
    [SerializeField] GameObject overUI;     // 게임 오버 프리팹

    [Header("피격 효과")]
    [SerializeField] float setAlphaValue;  
    [SerializeField] float setEfeectTime;
    Vector3 camPos;

    bool isPause; // 일시정지

    void Awake()
    {
        GC = this;
        PlayerType();

        startIcon.SetActive(false); pauseIcon.SetActive(true);

        pauseImage.SetActive(false);
        isPause = false;

        GameManager.GM.nowClearStar = 0; // 결과창 현재 별 수 초기화
        GameManager.GM.data.curRunRatio = 0; // 현재 달린 거리 초기화

        GameManager.GM.data.floorSpeedValue = GameManager.GM.data.setFloorSpeedValue; // 발판 속도 지정된 초기값으로 초기화
        GameManager.GM.data.BGSpeedValue = GameManager.GM.data.setBGSpeedValue;     // 배경 속도 지정된 초기값으로 초기화

        // HP 지정된 초기값으로 초기화
        GameManager.GM.data.setLifeScore = GameManager.GM.data.constLifeScore;
        GameManager.GM.data.lifeScore = GameManager.GM.data.setLifeScore;

        GameManager.GM.data.coinScore = 0;  // 게임 내 점수 초기화

        GameManager.GM.playerAlive = false; // 플레이어 사망처리 초기화

        isGameEnd = false;

        camPos = Camera.main.transform.position; // 메인 카메라 위치값 넣어 초기화
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
        score.text = "점수 : " + (GameManager.GM.data.coinScore == 0 ? 0 : CommaText(GameManager.GM.data.coinScore).ToString());

        runTime.text = "달린시간 " + Mathf.RoundToInt(runTimeInt) + " 초";

        if (GameManager.GM.data.lifeScore <= 0 && isGameEnd == false) StartCoroutine(EndGame(false));
    }
    public string CommaText(long score) { return string.Format("{0:#,###}", score); }

    public void Pause_B()
    {
        /*일시정지?비활성화*/
        if (isPause == false)
        {
            Time.timeScale = 0;
            pauseImage.SetActive(true);
            startIcon.SetActive(true); pauseIcon.SetActive(false);
            isPause = true; return;
        }

        /*일시정지?활성화*/
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
        GameObject endUI = isClear == true ? clearUI : overUI; // 생성될 결과 UI 지정

        // 게임 오버 시 점수는 0으로 고정 / 클리어가 아니기 때문에 별을 줄 수 없음
        if (!isClear) GameManager.GM.data.coinScore = 0;

        // 클리어 시 다음 스테이지가 열리도록 함, 만약 이미 클리어한 스테이지를 다시 플레이한 경우 그냥 리턴
        if (GameManager.GM.data.stageClearNum < GameManager.GM.data.branch_GM && isClear)
        { GameManager.GM.data.stageClearNum = GameManager.GM.data.branch_GM; }

        // 결과 UI 생성
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

        Time.timeScale = 1; //  타임 스케일 초기화, 게임 멈춤 방지
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
        Time.timeScale = 1; //  타임 스케일 초기화, 게임 멈춤 방지
        string stageDes = GameManager.GM.STG_Excel();
        GameManager.GM.Stage_Move("GameScene", "무한모드", stageDes);
    }

    public IEnumerator CameraShake(float magnitude)
    {
        for (int i = 0;i < 10;i++)
        {
            Camera.main.transform.localPosition = Random.insideUnitSphere * magnitude + camPos;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Camera.main.transform.localPosition = camPos; // 메인 카메라 위치 복귀
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
