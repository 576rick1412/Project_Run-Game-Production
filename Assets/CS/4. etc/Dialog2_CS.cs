using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Dialog2_CS : MonoBehaviour
{
    [Header("���̾�α� ����")]
    [SerializeField]GameObject Dialog;
    [SerializeField]GameObject QuitWindow;

    [SerializeField] RunGame_EX RunGame_EX;
    [SerializeField] DialogData[] dialogue;
    int branch;
    private float typingSpeed;  // �ؽ�Ʈ Ÿ���� �ӵ�

    int DialogIndex = 0;
    private bool isTypingEffect; // �ؽ�Ʈ Ÿ���� ������
    private bool isTypingEnd;    // ������ ���
    private bool AutoTyping;     // ���� Ÿ����
    private bool ButtonInput;    // ��ư ����

    bool CutsceneCheck;
    public GameObject[] eventScene;

    bool BGICheck;
    bool CharacterCheck;
    bool EndCheck;

    [Header("���̾�α� �̹���")]
    [SerializeField] Image BGI;
    [SerializeField] Image LeftCharacter;
    [SerializeField] Image MidCharacter;
    [SerializeField] Image RightCharacter;
    [Header("���̾�α� ��������Ʈ �ִ°�")]
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
                Debug.Log("��");
                GameManager.GM.Fade(Dialog,false);
                //Game_Control.GC.Result_Spawn();
                EndCheck = true;
            }
        }

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
    
    void Destroy()
    {
        eventSceneDestroy();
        BGIDestroy();
        CharacterDestroy();
    }   //  �ı��� ������

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
    }   // ���ȭ�� �ε�
    void BGIDestroy() { BGI.sprite = null; }   // ���ȭ�� �ʱ�ȭ
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
    }   // ĳ���� �ε�
    void CharacterDestroy()
    {
        LeftCharacter.sprite =  Character_Sprite[0];
        MidCharacter.sprite =   Character_Sprite[0];
        RightCharacter.sprite = Character_Sprite[0];
    }   // ĳ���� �ʱ�ȭ
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
                Destroy();
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
            {case  true: if (RunGame_EX.DialogSheet[DialogIndex + 1].
                        DIA_End == false) Destroy();
                    DialogIndex++; break;

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
    void Blackout(Image img,Sprite spri, bool oc) // �̹��� ����(�¿�) / �ٲ� �̹��� / ���� ����
    {
        img.sprite = spri;
        if (oc == true) img.color = new Color32(80, 80, 80, byte.MaxValue);
    }   //  Ȱ��ȭ + ����ȿ��
    [System.Serializable] public struct DialogData
    {
        public string name;                         // ĳ���� �̸�
        [TextArea(3, 5)] public string dialog;      // ���
    }

    public void OnQuitWindow() { QuitWindow.SetActive(true);  }
    public void OffQuitWindow() { QuitWindow.SetActive(false); }
    //public void DeletDialog() { GameManager.GM.Fade(Dialog,false); Game_Control.GC.Result_Spawn(); }
}
