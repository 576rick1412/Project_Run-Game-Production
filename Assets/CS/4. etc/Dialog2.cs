using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Dialog2 : MonoBehaviour
{
    [Header("���̾�α� ����")]
    [SerializeField]GameObject dialog;
    [SerializeField]GameObject quitWindow;

    [SerializeField] RunGame_EX runGame_EX;
    [SerializeField] DialogData[] dialogues;
    int branch;
    private float typingSpeed;  // �ؽ�Ʈ Ÿ���� �ӵ�

    int dialogIndex = 0;
    private bool isTypingEffect; // �ؽ�Ʈ Ÿ���� ������
    private bool isTypingEnd;    // ������ ���
    private bool autoTyping;     // ���� Ÿ����
    private bool buttonInput;    // ��ư ����

    bool isCutScene;
    public GameObject[] eventScenes;

    bool isBGI;
    bool isCharacter;
    bool isEnd;

    [Header("���̾�α� �̹���")]
    [SerializeField] Image BGI;
    [SerializeField] Image leftCharacter;
    [SerializeField] Image midCharacter;
    [SerializeField] Image rightCharacter;
    [Header("���̾�α� ��������Ʈ �ִ°�")]
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
        // ����ü�� ��� �־��ֱ�
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
                Debug.Log("��");
                GameManager.GM.Fade(dialog,false);
                //Game_Control.GC.Result_Spawn();
                isEnd = true;
            }
        }

        if (runGame_EX.DialogSheet[dialogIndex].DIA_End == false)
        {
            // Ÿ���� ��ŵ
            if (buttonInput == true && isTypingEnd == false)
                StartCoroutine("OnSkipText");

            // Ÿ���� ȿ��
            if (isTypingEnd == false && isTypingEffect == false)
                StartCoroutine("OnTypingText");

            // ���� ���� �̵�
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
    }   //  �ı��� ������

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
    }   // �ƾ� �ε�
    void EventSceneDestroy()
    {
        for (int i = 0; i < eventScenes.Length; i++)
        {
            eventScenes[i].SetActive(false);
        }
    }   // �ƾ� �ʱ�ȭ
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
    }   // ���ȭ�� �ε�
    void BGIDestroy() { BGI.sprite = null; }   // ���ȭ�� �ʱ�ȭ
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
    }   // ĳ���� �ε�
    void CharacterDestroy()
    {
        leftCharacter.sprite =  characterSprite[0];
        midCharacter.sprite =   characterSprite[0];
        rightCharacter.sprite = characterSprite[0];
    }   // ĳ���� �ʱ�ȭ
    private IEnumerator OnSkipText()
    {
        if (isTypingEnd == false)
        {
            buttonInput = false;
            Debug.Log("Ÿ���� ��ŵ");

            StopCoroutine("OnTypingText");
            TMP_Dialog.text = dialogues[dialogIndex].dialog;
            isTypingEnd = true;
            isTypingEffect = false;
        }
        yield return null;
    }   // Ÿ���� ��ŵ
    private IEnumerator OnInputText()
    {
        yield return null;
        if (runGame_EX.DialogSheet[dialogIndex + 1].DIA_End == false)
        {
            // ��� Ÿ������ ������ �� ȭ���� ��ġ�Ͽ� ���� ���� �̵�
            if (buttonInput == true)
            {
                buttonInput = false;
                Debug.Log("���� ���� �̵�");

                dialogIndex++;          // -> ���� ���� �Ѿ
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
            string text = dialogues[dialogIndex].dialog;
            isCutScene = false;
            isBGI = false;
            isCharacter = false;
            isTypingEffect = true;

            // �ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
            TMP_Name.text = dialogues[dialogIndex].name;
            while (index < text.Length + 1)
            {
                TMP_Dialog.text = text.Substring(0, index);
                index++;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTypingEffect = false; // Ÿ���� ����
            // ������ �ٲ� Input_Text�� ����� �� �ֵ��� ��

            switch(autoTyping)
            {case  true: if (runGame_EX.DialogSheet[dialogIndex + 1].
                        DIA_End == false) Destroy();
                    dialogIndex++; break;

             case false: isTypingEnd = true;    break;}

            yield return new WaitForSeconds(typingSpeed);
        }
        yield return null;
    }   // Ÿ���� ȿ��
    public void AutoTyping_F()
    {
        switch(autoTyping)
        {
            case true:autoTyping = false;break;
            case false:autoTyping = true;break;
        }
    }   // ���� Ÿ����
    public void GamaInput() { buttonInput = true; } // ��ư ����
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

    public void OnQuitWindow() { quitWindow.SetActive(true);  }
    public void OffQuitWindow() { quitWindow.SetActive(false); }
    //public void DeletDialog() { GameManager.GM.Fade(Dialog,false); Game_Control.GC.Result_Spawn(); }
}
