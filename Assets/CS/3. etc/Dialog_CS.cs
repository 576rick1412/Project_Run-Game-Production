using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog_CS : MonoBehaviour
{
    [SerializeField] RunGame_EX RunGame_EX;
    [SerializeField] DialogData[] dialogue;
    [SerializeField] int branch;
    [SerializeField] private float Keep_typingSpeed = 0.1f;  // 텍스트 타이핑 저장
    [SerializeField] private float typingSpeed;  // 텍스트 타이핑 속도
    int cutscene_num;

    [SerializeField] int DialogIndex = 0;
    [SerializeField] private bool isTypingEffect;    // 텍스트 타이핑 중인지
    [SerializeField] private bool isTypingEnd;    // 텍스트 타이핑 중인지
    [SerializeField] private bool isTypinSkip;    // 텍스트 타이핑 스킵


    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Dialog;

    void Start()
    {
        typingSpeed = Keep_typingSpeed;
        isTypinSkip = true;

        int index = 0;
        // 구조체에 대사 넣어주기
        for(int i =0;i<RunGame_EX.DialogSheet.Count;++i)
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
        // 전체 대사가 끝나지 않았을 때 함수 호출
        if (RunGame_EX.DialogSheet[DialogIndex].DIA_End == false) Dialog_Excel();    
    }

    void Dialog_Excel()
    {
        // 타이핑 중이지 않을 때 코루틴 시작
        if (isTypingEffect == false) StartCoroutine("OnTypingText");
    }

    private IEnumerator OnTypingText() 
    {
        // 대사 타이핑 효과
        if (isTypingEnd == false)
        {
            typingSpeed = Keep_typingSpeed;

            int index = 0;
            string text = dialogue[DialogIndex].dialog;
            isTypingEffect = true;

            // 텍스트를 한글자씩 타이핑치듯 재생
            TMP_Name.text = dialogue[DialogIndex].name;
            while (index < text.Length + 1)
            {
                Debug.Log("위");
                TMP_Dialog.text = text.Substring(0, index);
                index++;
                if (Input.anyKey && isTypinSkip == true && isTypingEnd == false)
                {
                    Debug.Log("이프 시작");
                    typingSpeed = 0f;
                    isTypinSkip = false;
                    Debug.Log("중간");
                }
                yield return new WaitForSeconds(typingSpeed);
                Debug.Log("아래");
            }

            isTypingEffect = false; // 타이핑 종료
            DialogIndex++;          // -> 다음 대사로 넘어감
            //isTypinSkip = false;

            isTypingEnd = true;     // 참으로 바꿔 Input_Text가 실행될 수 있도록 함
        }
        yield return null;
        StartCoroutine("Input_Text");
    }
    private IEnumerator Input_Text() 
    {
        // 대사 타이핑이 끝났을 때 화면을 터치하여 다음 대사로 이동
        if (Input.anyKey)
        {
            
            //참으로 바꿔 Input_Text가 실행되지 않도록 함
            if (isTypingEnd == true)
            {
                typingSpeed = Keep_typingSpeed;

                isTypingEnd = false;
                //isTypinSkip = true;
            }
            yield return new WaitForSeconds(Keep_typingSpeed);
            //yield return null;
        }
    }

    [System.Serializable]
    public struct DialogData
    {
        public string name;                         // 캐릭터 이름
        [TextArea(3, 5)] public string dialog;      // 대사
    }
}