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
    [SerializeField] private float Keep_typingSpeed = 0.1f;  // �ؽ�Ʈ Ÿ���� ����
    [SerializeField] private float typingSpeed;  // �ؽ�Ʈ Ÿ���� �ӵ�
    int cutscene_num;

    [SerializeField] int DialogIndex = 0;
    [SerializeField] private bool isTypingEffect;    // �ؽ�Ʈ Ÿ���� ������
    [SerializeField] private bool isTypingEnd;    // �ؽ�Ʈ Ÿ���� ������
    [SerializeField] private bool isTypinSkip;    // �ؽ�Ʈ Ÿ���� ��ŵ


    [SerializeField] TextMeshProUGUI TMP_Name;
    [SerializeField] TextMeshProUGUI TMP_Dialog;

    void Start()
    {
        typingSpeed = Keep_typingSpeed;
        isTypinSkip = true;

        int index = 0;
        // ����ü�� ��� �־��ֱ�
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
        // ��ü ��簡 ������ �ʾ��� �� �Լ� ȣ��
        if (RunGame_EX.DialogSheet[DialogIndex].DIA_End == false) Dialog_Excel();    
    }

    void Dialog_Excel()
    {
        // Ÿ���� ������ ���� �� �ڷ�ƾ ����
        if (isTypingEffect == false) StartCoroutine("OnTypingText");
    }

    private IEnumerator OnTypingText() 
    {
        // ��� Ÿ���� ȿ��
        if (isTypingEnd == false)
        {
            typingSpeed = Keep_typingSpeed;

            int index = 0;
            string text = dialogue[DialogIndex].dialog;
            isTypingEffect = true;

            // �ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
            TMP_Name.text = dialogue[DialogIndex].name;
            while (index < text.Length + 1)
            {
                Debug.Log("��");
                TMP_Dialog.text = text.Substring(0, index);
                index++;
                if (Input.anyKey && isTypinSkip == true && isTypingEnd == false)
                {
                    Debug.Log("���� ����");
                    typingSpeed = 0f;
                    isTypinSkip = false;
                    Debug.Log("�߰�");
                }
                yield return new WaitForSeconds(typingSpeed);
                Debug.Log("�Ʒ�");
            }

            isTypingEffect = false; // Ÿ���� ����
            DialogIndex++;          // -> ���� ���� �Ѿ
            //isTypinSkip = false;

            isTypingEnd = true;     // ������ �ٲ� Input_Text�� ����� �� �ֵ��� ��
        }
        yield return null;
        StartCoroutine("Input_Text");
    }
    private IEnumerator Input_Text() 
    {
        // ��� Ÿ������ ������ �� ȭ���� ��ġ�Ͽ� ���� ���� �̵�
        if (Input.anyKey)
        {
            
            //������ �ٲ� Input_Text�� ������� �ʵ��� ��
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
        public string name;                         // ĳ���� �̸�
        [TextArea(3, 5)] public string dialog;      // ���
    }
}