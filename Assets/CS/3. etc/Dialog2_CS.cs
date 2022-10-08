using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog2_CS : MonoBehaviour
{
    [SerializeField] RunGame_EX RunGame_EX;
    [SerializeField] DialogData[] dialogue;
    [SerializeField] int branch;
    [SerializeField] private float typingSpeed;  // �ؽ�Ʈ Ÿ���� �ӵ�
    int cutscene_num;

    [SerializeField] int DialogIndex = 0;
    [SerializeField] private bool isTypingEffect;    // �ؽ�Ʈ Ÿ���� ������
    [SerializeField] private bool isTypingEnd;    // �ؽ�Ʈ Ÿ���� ������


    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Dialog;

    void Start()
    {
        int index = 0;
        // ����ü�� ��� �־��ֱ�
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
        GameLoop();
    }

    void GameLoop()
    {
        if (RunGame_EX.DialogSheet[DialogIndex].DIA_End == false)
        {
            if (Input.anyKeyDown && isTypingEnd == false)
            { StartCoroutine("OnSkipText"); }

            if (isTypingEnd == false && isTypingEffect == false) 
                StartCoroutine("OnTypingText");

            if (isTypingEnd == true)
            {
                StartCoroutine("Input_Text");
            }
        }
    }
    private IEnumerator OnSkipText()
    {
        if (isTypingEnd == false)
        {
            Debug.Log("�۵�");
            StopCoroutine("OnTypingText");
            TMP_Dialog.text = dialogue[DialogIndex].dialog;
            isTypingEnd = true;
            isTypingEffect = false;
        }
        yield return null;
    }

    private IEnumerator OnTypingText()
    {
        // ��� Ÿ���� ȿ��
        if (isTypingEnd == false)
        {
            int index = 0;
            string text = dialogue[DialogIndex].dialog;
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
            isTypingEnd = true;     // ������ �ٲ� Input_Text�� ����� �� �ֵ��� ��
        }
        yield return null;
    }
    private IEnumerator Input_Text()
    {
        yield return null;

        // ��� Ÿ������ ������ �� ȭ���� ��ġ�Ͽ� ���� ���� �̵�
        if (Input.anyKeyDown)
        {
            Debug.Log("�۵�222222");
            DialogIndex++;          // -> ���� ���� �Ѿ
            isTypingEnd = false;
            isTypingEffect = false;
        }
    }


    [System.Serializable]
    public struct DialogData
    {
        public string name;                         // ĳ���� �̸�
        [TextArea(3, 5)] public string dialog;      // ���
    }
}

