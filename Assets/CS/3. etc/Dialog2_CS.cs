using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class Dialog2_CS : MonoBehaviour
{
    [SerializeField] RunGame_EX RunGame_EX;
    [SerializeField] DialogData[] dialogue;
    int branch;
    private float typingSpeed;  // 텍스트 타이핑 속도

    int DialogIndex = 0;
    private bool isTypingEffect;    // 텍스트 타이핑 중인지
    private bool isTypingEnd;    // 다음줄 대기
    private bool AutoTyping;    // 다음줄 대기
    private bool ButtonInput;    // 다음줄 대기

    bool CutsceneCheck;
    public GameObject[] eventScene;

    bool BGICheck;
    public GameObject[] BGIScene;

    bool CharacterCheck;
    public GameObject[] LeftCharacter;
    public GameObject[] MidCharacter;
    public GameObject[] RightCharacter;

    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Dialog;

    void Start()
    {
        branch = GameManager.GM.GM_branch;
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

        eventSceneDestroy();
        BGIDestroy();
        CharacterDestroy();
    }

    void Update()
    {
        GameLoop();
        ImageLoop();
    }

    void GameLoop()
    {
        typingSpeed = GameManager.GM.GM_typingSpeed;

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
        if (BGICheck == false)
        {
            switch (RunGame_EX.DialogSheet[DialogIndex].DIA_BGI)
            {
                case "BGI_city": BGIScene[0].SetActive(true); break;
                case "BGI_ground": BGIScene[1].SetActive(true); break;
            }
            BGICheck = true;
        }
    }   // 배경화면 로딩
    void BGIDestroy()
    {
        for (int i = 0; i < BGIScene.Length; i++)
        {
            BGIScene[i].SetActive(false);
        }
    }   // 배경화면 초기화
    void CharacterLoad()
    {
        if (CharacterCheck == false)
        {
            switch (RunGame_EX.DialogSheet[DialogIndex].Left_Character)
            {
                case "skt": LeftCharacter[0].SetActive(true); break;
                case "lg": LeftCharacter[1].SetActive(true); break;
                case "kt": LeftCharacter[2].SetActive(true); break;
            }
            switch (RunGame_EX.DialogSheet[DialogIndex].Mid_Character)
            {
                case "skt": MidCharacter[0].SetActive(true); break;
                case "lg": MidCharacter[1].SetActive(true); break;
                case "kt": MidCharacter[2].SetActive(true); break;
            }
            switch (RunGame_EX.DialogSheet[DialogIndex].Right_Character)
            {
                case "skt": RightCharacter[0].SetActive(true); break;
                case "lg": RightCharacter[1].SetActive(true); break;
                case "kt": RightCharacter[2].SetActive(true); break;
            }
            CharacterCheck = true;
        }
    }   // 컷씬 로딩
    void CharacterDestroy()
    {
        for (int i = 0; i < LeftCharacter.Length; i++)  LeftCharacter[i].SetActive(false);
        for (int i = 0; i < MidCharacter.Length; i++)   MidCharacter[i].SetActive(false);
        for (int i = 0; i < RightCharacter.Length; i++) RightCharacter[i].SetActive(false);
       
    }   // 컷씬 초기화
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

                eventSceneDestroy();
                BGIDestroy();
                CharacterDestroy();
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
            {case  true: DialogIndex++;         break;
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
    [System.Serializable] public struct DialogData
    {
        public string name;                         // 캐릭터 이름
        [TextArea(3, 5)] public string dialog;      // 대사
    }
}

