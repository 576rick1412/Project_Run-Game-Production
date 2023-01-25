using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    [Header("����â")]
    [SerializeField] GameObject normalUI;
    [SerializeField] GameObject hardUI;

    [Header("�ְ����� �ؽ�Ʈ")]
    [SerializeField] TextMeshProUGUI normalText;
    [SerializeField] TextMeshProUGUI hardText;

    void Start()
    {
        if (GameManager.GM.data.nomalMaxScore == 0) { normalText.text = "�ְ����� : ��Ͼ���"; }
        else { normalText.text = "�ְ����� : " + CommaText(GameManager.GM.data.nomalMaxScore) + " ��"; }

        if (GameManager.GM.data.hardMaxScore == 0) { hardText.text = "�ְ����� : ��Ͼ���"; }
        else { hardText.text = "�ְ����� : " + CommaText(GameManager.GM.data.hardMaxScore) + " ��"; }
    }

    public void OnNormal()
    {
        GameManager.GM.isNormal = true;
    }

    public void OnHard()
    {
        GameManager.GM.isNormal = false;
    }
    string CommaText(long score)
    {
        return string.Format("{0:#,###}", score);
    }
}