using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageButton : MonoBehaviour
{
    [Header("≈ÿΩ∫∆Æ")]
    public string Stage_Name;
    [TextArea(3, 5)] public string Stage_Information;

    [SerializeField] int Stage_Num;
    Stage_Info SI;
    private void Awake()
    {
        SI = FindObjectOfType<Stage_Info>();
    }

    public void OnWindow()
    {
        SI.Stage_Name.text = Stage_Name;
        SI.Stage_Information.text = Stage_Information;

        SI.Stage_Num = Stage_Num;

        SI.Name = Stage_Name;
        SI.Information = Stage_Information;

        SI.OnStage_Info();
    }
}