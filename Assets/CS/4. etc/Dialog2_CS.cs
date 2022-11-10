using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Dialog2_CS : MonoBehaviour
{
    [Header("다이얼로그 관리")]
    [SerializeField]GameObject Dialog;
    [SerializeField]GameObject QuitWindow;

    [SerializeField] RunGame_EX RunGame_EX;
    [SerializeField] DialogData[] dialogue;
    int branch;
    private float typingSpeed;  // 텍스트 타이핑 속도

    int DialogIndex = 0;
    private bool isTypingEffect; // 텍스트 타이핑 중인지
    private bool isTypingEnd;    // 다음줄 대기
    private bool AutoTyping;     // 오토 타이핑
    private bool ButtonInput;    // 버튼 눌림

    bool CutsceneCheck;
    public GameObject[] eventScene;

    bool BGICheck;
    bool CharacterCheck;
    bool EndCheck;

    [Header("다이얼로그 이미지")]
    [SerializeField] Image BGI;
    [SerializeField] Image LeftCharacter;
    [SerializeField] Image MidCharacter;
    [SerializeField] Image RightCharacter;
    [Header("다이얼로그 스프라이트 넣는곳")]
    [SerializeField] Sprite[] BGIScene;
    [SerializeField] Sprite[] Character_Sprite;

    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Dialog;

    void Start()
    {
        EndCheck = false;
        QuitWindow.SetActive(false);
        branch = GameManager.GM.Data.GM_branch;
        int index = 0;
        // 구조체에 대사 넣어주기
        for (int i = 0; i < RunGame_EX.DialogSheet.Count; ++i)
        {
            if (RunGame_EX.DialogSheet[i].DIA_branch == branch)
            {
                Array.Resize(ref dialogue, dialogue.Length + 1);
                dialogue[index].name = RunGame_EX.DialogSheet[i].DIA_name;
                dialogue[index].dialog = RunGame_EX.DialogSheet[i].DIA_dialog;
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
        typingSpeed = GameManager.GM.Data.GM_typingSpeed;

        if (RunGame_EX.DialogSheet[DialogIndex + 1].DIA_End && isTypingEnd && !EndCheck)
        { 
            if(Input.anyKey)
            {
                Debug.Log("끗");
                GameManager.GM.Fade(Dialog,false);
                //Game_Control.GC.Result_Spawn();
                EndCheck = true;
            }
        }

        if (RunGame_EX.DialogSheet[DialogIndex].DIA_End == false)
        {
            // 타이핑 스킵
            if (ButtonInput == true && isTypingEnd == false)
                StartCoroutine("OnSkipText");

            // 타이핑 효과
            if (isTypingEnd == false && isTypingEffect == false)
                StartCoroutine("OnTypingText");

            // 다음 대사로 이동
            if (ButtonInput == true && isTypingEnd == true)
                StartCoroutine("OnInputText");
        }
    }
    void ImageLoop()
    {
        eventSceneLoad();
        BGILoad();
        CharacterLoad();
    }
    
    void Destroy()
    {
        eventSceneDestroy();
        BGIDestroy();
        CharacterDestroy();
    }   //  파괴자 모음집

    void eventSceneLoad()
    {
        if (CutsceneCheck == false)
        {
            switch (RunGame_EX.DialogSheet[DialogIndex].DIA_cutscene)
            {
                case "event_000": eventScene[0].SetActive(true); break;
                case "event_001": eventScene[1].SetActive(true); break;
                case "event_002": eventScene[2].SetActive(true); break;
                case "event_003": eventScene[3].SetActive(true); break;
                case "event_004": eventScene[4].SetActive(true); break;
            }
            CutsceneCheck = true;
        }
    }   // 컷씬 로딩
    void eventSceneDestroy()
    {
        for (int i = 0; i < eventScene.Length; i++)
        {
            eventScene[i].SetActive(false);
        }
    }   // 컷씬 초기화
    void BGILoad()
    {
        bool BGI_Check = RunGame_EX.DialogSheet[DialogIndex].BGI_Blackout == true;
        if (BGICheck == false)
        {
            switch (RunGame_EX.DialogSheet[DialogIndex].DIA_BGI)
            {
                case "BGI_city"  :  Blackout(BGI,BGIScene[0], BGI_Check); break;
                case "BGI_ground":  Blackout(BGI,BGIScene[1], BGI_Check); break;
            }
            BGICheck = true;
        }
    }   // 배경화면 로딩
    void BGIDestroy() { BGI.sprite = null; }   // 배경화면 초기화
    void CharacterLoad()
    {
        bool Left = RunGame_EX.DialogSheet[DialogIndex].Left_Blackout;
        bool Mid = RunGame_EX.DialogSheet[DialogIndex].Mid_Blackout;
        bool Right = RunGame_EX.DialogSheet[DialogIndex].Right_Blackout;
        if (CharacterCheck == false)
        {
            switch (RunGame_EX.DialogSheet[DialogIndex].Left_Character)
            {
                case "W": Blackout(LeftCharacter, Character_Sprite[1], Left); break;
                case "F": Blackout(LeftCharacter, Character_Sprite[2], Left); break;
                case "P": Blackout(LeftCharacter, Character_Sprite[3], Left); break;
                    ;            }
            switch (RunGame_EX.DialogSheet[DialogIndex].Mid_Character)
            {
                case "W": Blackout(MidCharacter, Character_Sprite[1], Mid); break;
                case "F": Blackout(MidCharacter, Character_Sprite[2] ,Mid); break;
                case "P": Blackout(MidCharacter, Character_Sprite[3] ,Mid); break;
            }
            switch (RunGame_EX.DialogSheet[DialogIndex].Right_Character)
            {
                case "W": Blackout(RightCharacter, Character_Sprite[1], Right); break;
                case "F": Blackout(RightCharacter, Character_Sprite[2], Right); break;
                case "P": Blackout(RightCharacter, Character_Sprite[3], Right); break;
            }
            CharacterCheck = true;
        }
    }   // 캐릭터 로딩
    void CharacterDestroy()
    {
        LeftCharacter.sprite =  Character_Sprite[0];
        MidCharacter.sprite =   Character_Sprite[0];
        RightCharacter.sprite = Character_Sprite[0];
    }   // 캐릭터 초기화
    private IEnumerator OnSkipText()
    {
        if (isTypingEnd == false)
        {
            ButtonInput = false;
            Debug.Log("타이핑 스킵");

            StopCoroutine("OnTypingText");
            TMP_Dialog.text = dialogue[DialogIndex].dialog;
            isTypingEnd = true;
            isTypingEffect = false;
        }
        yield return null;
    }   // 타이핑 스킵
    private IEnumerator OnInputText()
    {
        yield return null;
        if (RunGame_EX.DialogSheet[DialogIndex + 1].DIA_End == false)
        {
            // 대사 타이핑이 끝났을 때 화면을 터치하여 다음 대사로 이동
            if (ButtonInput == true)
            {
                ButtonInput = false;
                Debug.Log("다음 대사로 이동");

                DialogIndex++;          // -> 다음 대사로 넘어감
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
            string text = dialogue[DialogIndex].dialog;
            CutsceneCheck = false;
            BGICheck = false;
            CharacterCheck = false;
            isTypingEffect = true;

            // 텍스트를 한글자씩 타이핑치듯 재생
            TMP_Name.text = dialogue[DialogIndex].name;
            while (index < text.Length + 1)
            {
                TMP_Dialog.text = text.Substring(0, index);
                index++;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTypingEffect = false; // 타이핑 종료
            // 참으로 바꿔 Input_Text가 실행될 수 있도록 함

            switch(AutoTyping)
            {case  true: if (RunGame_EX.DialogSheet[DialogIndex + 1].
                        DIA_End == false) Destroy();
                    DialogIndex++; break;

             case false: isTypingEnd = true;    break;}

            yield return new WaitForSeconds(typingSpeed);
        }
        yield return null;
    }   // 타이핑 효과
    public void AutoTyping_F()
    {
        switch(AutoTyping)
        {
            case true:AutoTyping = false;break;
            case false:AutoTyping = true;break;
        }
    }   // 오토 타이핑
    public void GamaInput() { ButtonInput = true; } // 버튼 누름
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

    public void OnQuitWindow() { QuitWindow.SetActive(true);  }
    public void OffQuitWindow() { QuitWindow.SetActive(false); }
    //public void DeletDialog() { GameManager.GM.Fade(Dialog,false); Game_Control.GC.Result_Spawn(); }
}
