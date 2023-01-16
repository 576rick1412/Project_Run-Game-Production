using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_Info : MonoBehaviour
{
    public TextMeshProUGUI stageName;
    public TextMeshProUGUI stageMaxScore;
    public TextMeshProUGUI stageInformation;

    [HideInInspector] public int stageNum;
    public Image targetSprite;
    public Sprite[] infoSprites;
    [SerializeField] GameObject stageWindow;
    void Start()
    {
        stageName.text = "";
        stageInformation.text = "";
        stageWindow.SetActive(false);
    }

    void Update()
    {
        
    }
    public void OnStage_Info()
    {
        stageWindow.SetActive(true);
    }
    public void OffStage_Info()
    {
        stageWindow.SetActive(false);
    }

    public void GameStart()
    {
        GameManager.GM.data.floorSpeedValue = GameManager.GM.data.setFloorSpeedValue;
        GameManager.GM.data.BGM_Value = GameManager.GM.data.setBGSpeedValue;
        GameManager.GM.data.branch_GM = stageNum;

        GameManager.GM.stageName = stageName.text;
        GameManager.GM.stageInformation = stageInformation.text;

        GameManager.GM.Stage_Move();
    }
}