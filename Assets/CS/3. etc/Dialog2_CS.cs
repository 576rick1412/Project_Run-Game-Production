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
    private float typingSpeed;  // �ؽ�Ʈ Ÿ���� �ӵ�

    int DialogIndex = 0;
    private bool isTypingEffect;    // �ؽ�Ʈ Ÿ���� ������
    private bool isTypingEnd;    // ������ ���
    private bool AutoTyping;    // ������ ���
    private bool ButtonInput;    // ������ ���

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
        // ����ü�� ��� �־��ֱ�
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
            // Ÿ���� ��ŵ
            if (ButtonInput == true && isTypingEnd == false)
                StartCoroutine("OnSkipText");

            // Ÿ���� ȿ��
            if (isTypingEnd == false && isTypingEffect == false)
                StartCoroutine("OnTypingText");

            // ���� ���� �̵�
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
    }   // �ƾ� �ε�
    void eventSceneDestroy()
    {
        for (int i = 0; i < eventScene.Length; i++)
        {
            eventScene[i].SetActive(false);
        }
    }   // �ƾ� �ʱ�ȭ
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
    }   // ���ȭ�� �ε�
    void BGIDestroy()
    {
        for (int i = 0; i < BGIScene.Length; i++)
        {
            BGIScene[i].SetActive(false);
        }
    }   // ���ȭ�� �ʱ�ȭ
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
    }   // �ƾ� �ε�
    void CharacterDestroy()
    {
        for (int i = 0; i < LeftCharacter.Length; i++)  LeftCharacter[i].SetActive(false);
        for (int i = 0; i < MidCharacter.Length; i++)   MidCharacter[i].SetActive(false);
        for (int i = 0; i < RightCharacter.Length; i++) RightCharacter[i].SetActive(false);
       
    }   // �ƾ� �ʱ�ȭ
    private IEnumerator OnSkipText()
    {
        if (isTypingEnd == false)
        {
            ButtonInput = false;
            Debug.Log("Ÿ���� ��ŵ");

            StopCoroutine("OnTypingText");
            TMP_Dialog.text = dialogue[DialogIndex].dialog;
            isTypingEnd = true;
            isTypingEffect = false;
        }
        yield return null;
    }   // Ÿ���� ��ŵ
    private IEnumerator OnInputText()
    {
        yield return null;
        if (RunGame_EX.DialogSheet[DialogIndex + 1].DIA_End == false)
        {
            // ��� Ÿ������ ������ �� ȭ���� ��ġ�Ͽ� ���� ���� �̵�
            if (ButtonInput == true)
            {
                ButtonInput = false;
                Debug.Log("���� ���� �̵�");

                DialogIndex++;          // -> ���� ���� �Ѿ
                isTypingEnd = false;
                isTypingEffect = false;

                eventSceneDestroy();
                BGIDestroy();
                CharacterDestroy();
            }
        }

    }   // ���� ���� �̵�
    private IEnumerator OnTypingText()
    {
        // ��� Ÿ���� ȿ��
        if (isTypingEnd == false)
        {
            int index = 0;
            string text = dialogue[DialogIndex].dialog;
            CutsceneCheck = false;
            BGICheck = false;
            CharacterCheck = false;
            isTypingEffect = true;

            // �ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
            TMP_Name.text = dialogue[DialogIndex].name;
            while (index < text.Length + 1)
            {
                TMP_Dialog.text = text.Substring(0, index);
                index++;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTypingEffect = false; // Ÿ���� ����
            // ������ �ٲ� Input_Text�� ����� �� �ֵ��� ��

            switch(AutoTyping)
            {case  true: DialogIndex++;         break;
             case false: isTypingEnd = true;    break;}

            yield return new WaitForSeconds(typingSpeed);
        }
        yield return null;
    }   // Ÿ���� ȿ��
    public void AutoTyping_F()
    {
        switch(AutoTyping)
        {
            case true:AutoTyping = false;break;
            case false:AutoTyping = true;break;
        }
    }   // ���� Ÿ����

    public void GamaInput() { ButtonInput = true; } // ��ư ����
    [System.Serializable] public struct DialogData
    {
        public string name;                         // ĳ���� �̸�
        [TextArea(3, 5)] public string dialog;      // ���
    }
}

