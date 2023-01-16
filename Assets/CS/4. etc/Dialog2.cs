using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Dialog2 : MonoBehaviour
{
    [Header("다이얼로그 관리")]
    [SerializeField]GameObject dialog;
    [SerializeField]GameObject quitWindow;

    [SerializeField] RunGame_EX runGame_EX;
    [SerializeField] DialogData[] dialogues;
    int branch;
    private float typingSpeed;  // 텍스트 타이핑 속도

    int dialogIndex = 0;
    private bool isTypingEffect; // 텍스트 타이핑 중인지
    private bool isTypingEnd;    // 다음줄 대기
    private bool autoTyping;     // 오토 타이핑
    private bool buttonInput;    // 버튼 눌림

    bool isCutScene;
    public GameObject[] eventScenes;

    bool isBGI;
    bool isCharacter;
    bool isEnd;

    [Header("다이얼로그 이미지")]
    [SerializeField] Image BGI;
    [SerializeField] Image leftCharacter;
    [SerializeField] Image midCharacter;
    [SerializeField] Image rightCharacter;
    [Header("다이얼로그 스프라이트 넣는곳")]
    [SerializeField] Sprite[] BGIScene;
    [SerializeField] Sprite[] characterSprite;

    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Dialog;

    void Start()
    {
        isEnd = false;
        quitWindow.SetActive(false);
        branch = GameManager.GM.data.branch_GM;
        int index = 0;
        // 구조체에 대사 넣어주기
        for (int i = 0; i < runGame_EX.DialogSheet.Count; ++i)
        {
            if (runGame_EX.DialogSheet[i].DIA_branch == branch)
            {
                Array.Resize(ref dialogues, dialogues.Length + 1);
                dialogues[index].name = runGame_EX.DialogSheet[i].DIA_name;
                dialogues[index].dialog = runGame_EX.DialogSheet[i].DIA_dialog;
                index++;
            }
        }

        Destroy();
    }

    void Update()
    {
        GameLoop();
        ImageLoop();
    }

    void GameLoop()
    {
        typingSpeed = GameManager.GM.data.typingSpeed_GM;

        if (runGame_EX.DialogSheet[dialogIndex + 1].DIA_End && isTypingEnd && !isEnd)
        { 
            if(Input.anyKey)
            {
                Debug.Log("끗");
                GameManager.GM.Fade(dialog,false);
                //Game_Control.GC.Result_Spawn();
                isEnd = true;
            }
        }

        if (runGame_EX.DialogSheet[dialogIndex].DIA_End == false)
        {
            // 타이핑 스킵
            if (buttonInput == true && isTypingEnd == false)
                StartCoroutine("OnSkipText");

            // 타이핑 효과
            if (isTypingEnd == false && isTypingEffect == false)
                StartCoroutine("OnTypingText");

            // 다음 대사로 이동
            if (buttonInput == true && isTypingEnd == true)
                StartCoroutine("OnInputText");
        }
    }
    void ImageLoop()
    {
        EventSceneLoad();
        BGILoad();
        CharacterLoad();
    }
    
    void Destroy()
    {
        EventSceneDestroy();
        BGIDestroy();
        CharacterDestroy();
    }   //  파괴자 모음집

    void EventSceneLoad()
    {
        if (isCutScene == false)
        {
            switch (runGame_EX.DialogSheet[dialogIndex].DIA_cutscene)
            {
                case "event_000": eventScenes[0].SetActive(true); break;
                case "event_001": eventScenes[1].SetActive(true); break;
                case "event_002": eventScenes[2].SetActive(true); break;
                case "event_003": eventScenes[3].SetActive(true); break;
                case "event_004": eventScenes[4].SetActive(true); break;
            }
            isCutScene = true;
        }
    }   // 컷씬 로딩
    void EventSceneDestroy()
    {
        for (int i = 0; i < eventScenes.Length; i++)
        {
            eventScenes[i].SetActive(false);
        }
    }   // 컷씬 초기화
    void BGILoad()
    {
        bool BGI_Check = runGame_EX.DialogSheet[dialogIndex].BGI_Blackout == true;
        if (isBGI == false)
        {
            switch (runGame_EX.DialogSheet[dialogIndex].DIA_BGI)
            {
                case "BGI_city"  :  Blackout(BGI,BGIScene[0], BGI_Check); break;
                case "BGI_ground":  Blackout(BGI,BGIScene[1], BGI_Check); break;
            }
            isBGI = true;
        }
    }   // 배경화면 로딩
    void BGIDestroy() { BGI.sprite = null; }   // 배경화면 초기화
    void CharacterLoad()
    {
        bool Left = runGame_EX.DialogSheet[dialogIndex].Left_Blackout;
        bool Mid = runGame_EX.DialogSheet[dialogIndex].Mid_Blackout;
        bool Right = runGame_EX.DialogSheet[dialogIndex].Right_Blackout;
        if (isCharacter == false)
        {
            switch (runGame_EX.DialogSheet[dialogIndex].Left_Character)
            {
                case "W": Blackout(leftCharacter, characterSprite[1], Left); break;
                case "F": Blackout(leftCharacter, characterSprite[2], Left); break;
                case "P": Blackout(leftCharacter, characterSprite[3], Left); break;
                    ;            }
            switch (runGame_EX.DialogSheet[dialogIndex].Mid_Character)
            {
                case "W": Blackout(midCharacter, characterSprite[1], Mid); break;
                case "F": Blackout(midCharacter, characterSprite[2] ,Mid); break;
                case "P": Blackout(midCharacter, characterSprite[3] ,Mid); break;
            }
            switch (runGame_EX.DialogSheet[dialogIndex].Right_Character)
            {
                case "W": Blackout(rightCharacter, characterSprite[1], Right); break;
                case "F": Blackout(rightCharacter, characterSprite[2], Right); break;
                case "P": Blackout(rightCharacter, characterSprite[3], Right); break;
            }
            isCharacter = true;
        }
    }   // 캐릭터 로딩
    void CharacterDestroy()
    {
        leftCharacter.sprite =  characterSprite[0];
        midCharacter.sprite =   characterSprite[0];
        rightCharacter.sprite = characterSprite[0];
    }   // 캐릭터 초기화
    private IEnumerator OnSkipText()
    {
        if (isTypingEnd == false)
        {
            buttonInput = false;
            Debug.Log("타이핑 스킵");

            StopCoroutine("OnTypingText");
            TMP_Dialog.text = dialogues[dialogIndex].dialog;
            isTypingEnd = true;
            isTypingEffect = false;
        }
        yield return null;
    }   // 타이핑 스킵
    private IEnumerator OnInputText()
    {
        yield return null;
        if (runGame_EX.DialogSheet[dialogIndex + 1].DIA_End == false)
        {
            // 대사 타이핑이 끝났을 때 화면을 터치하여 다음 대사로 이동
            if (buttonInput == true)
            {
                buttonInput = false;
                Debug.Log("다음 대사로 이동");

                dialogIndex++;          // -> 다음 대사로 넘어감
                isTypingEnd = false;
                isTypingEffect = false;
                Destroy();
            }
        }

    }   // 다음 대사로 이동
    private IEnumerator OnTypingText()
    {
        // 대사 타이핑 효과
        if (isTypingEnd == false)
        {
            int index = 0;
            string text = dialogues[dialogIndex].dialog;
            isCutScene = false;
            isBGI = false;
            isCharacter = false;
            isTypingEffect = true;

            // 텍스트를 한글자씩 타이핑치듯 재생
            TMP_Name.text = dialogues[dialogIndex].name;
            while (index < text.Length + 1)
            {
                TMP_Dialog.text = text.Substring(0, index);
                index++;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTypingEffect = false; // 타이핑 종료
            // 참으로 바꿔 Input_Text가 실행될 수 있도록 함

            switch(autoTyping)
            {case  true: if (runGame_EX.DialogSheet[dialogIndex + 1].
                        DIA_End == false) Destroy();
                    dialogIndex++; break;

             case false: isTypingEnd = true;    break;}

            yield return new WaitForSeconds(typingSpeed);
        }
        yield return null;
    }   // 타이핑 효과
    public void AutoTyping_F()
    {
        switch(autoTyping)
        {
            case true:autoTyping = false;break;
            case false:autoTyping = true;break;
        }
    }   // 오토 타이핑
    public void GamaInput() { buttonInput = true; } // 버튼 누름
    void Blackout(Image img,Sprite spri, bool oc) // 이미지 종류(좌우) / 바꿀 이미지 / 암전 여부
    {
        img.sprite = spri;
        if (oc == true) img.color = new Color32(80, 80, 80, byte.MaxValue);
    }   //  활성화 + 암전효과
    [System.Serializable] public struct DialogData
    {
        public string name;                         // 캐릭터 이름
        [TextArea(3, 5)] public string dialog;      // 대사
    }

    public void OnQuitWindow() { quitWindow.SetActive(true);  }
    public void OffQuitWindow() { quitWindow.SetActive(false); }
    //public void DeletDialog() { GameManager.GM.Fade(Dialog,false); Game_Control.GC.Result_Spawn(); }
}
