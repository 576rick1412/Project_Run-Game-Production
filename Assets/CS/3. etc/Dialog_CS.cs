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
    [SerializeField] private float typingSpeed = 0.1f;  // �ؽ�Ʈ Ÿ���� �ӵ�
    int cutscene_num;

    int DialogIndex = 0;
    private bool isTypingEffect = false;    // �ؽ�Ʈ Ÿ���� ������
    private bool isTypingEnd = false;    // �ؽ�Ʈ Ÿ���� ������


    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Dialog;

    void Start()
    {
        int index = 0;
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
        if(RunGame_EX.DialogSheet[DialogIndex].DIA_End == false) Dialog_Excel();
    }

    void Dialog_Excel()
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (isTypingEffect == false) StartCoroutine("OnTypingText");
          
        }
    }

    private IEnumerator OnTypingText()
    {
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
            isTypingEffect = false;
            DialogIndex++;

            isTypingEnd = true;
            
            yield return null;
        }
        StartCoroutine("Input_Text");
        /*while (index < dialogue.Length)
        {
            TMP_Dialog.text = dialogue[DialogIndex].dialog.Substring(0, index);
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }*/
    }
    private IEnumerator Input_Text()
    {
        if(isTypingEnd == true && Input.anyKey)
        {
            isTypingEnd = false;
            yield return null;
        }
    }

    [System.Serializable]
    public struct DialogData
    {
        public string name;                         // ĳ���� �̸�
        [TextArea(3, 5)] public string dialog;      // ���
    }
}