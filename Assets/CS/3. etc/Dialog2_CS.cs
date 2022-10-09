using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog2_CS : MonoBehaviour
{
    [SerializeField] RunGame_EX RunGame_EX;
    [SerializeField] DialogData[] dialogue;
    [SerializeField] int branch;
    [SerializeField] private float typingSpeed;  // 텍스트 타이핑 속도
    int cutscene_num;

    [SerializeField] int DialogIndex = 0;
    [SerializeField] private bool isTypingEffect;    // 텍스트 타이핑 중인지
    [SerializeField] private bool isTypingEnd;    // 다음줄 대기
    [SerializeField] private bool AutoTyping;    // 다음줄 대기
    [SerializeField] private bool ButtonInput;    // 다음줄 대기


    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Dialog;

    void Start()
    {
        int index = 0;
        // 구조체에 대사 넣어주기
        for (int i = 0; i < RunGame_EX.DialogSheet.Count; ++i)
        {
            if (RunGame_EX.DialogSheet[i].DIA_branch == branch)
            {
                dialogue[index].name = RunGame_EX.DialogSheet[i].DIA_name;
                dialogue[index].dialog = RunGame_EX.DialogSheet[i].DIA_dialog;
                index++;
            }
        }
    }

    void Update()
    {
        typingSpeed = GameManager.GM.GM_typingSpeed;
        GameLoop();
    }

    void GameLoop()
    {
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
    }
    private IEnumerator OnInputText()
    {
        yield return null;

        // 대사 타이핑이 끝났을 때 화면을 터치하여 다음 대사로 이동
        if (ButtonInput == true)
        {
            ButtonInput = false;
            Debug.Log("다음 대사로 이동");

            DialogIndex++;          // -> 다음 대사로 넘어감
            isTypingEnd = false;
            isTypingEffect = false;
        }

    }
    private IEnumerator OnTypingText()
    {
        // 대사 타이핑 효과
        if (isTypingEnd == false)
        {
            int index = 0;
            string text = dialogue[DialogIndex].dialog;
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
    }

    public void AutoTyping_F()
    {
        switch(AutoTyping)
        {
            case true:AutoTyping = false;break;
            case false:AutoTyping = true;break;
        }
    }
    public void GamaInput() { ButtonInput = true; }

    [System.Serializable]
    public struct DialogData
    {
        public string name;                         // 캐릭터 이름
        [TextArea(3, 5)] public string dialog;      // 대사
    }
}

