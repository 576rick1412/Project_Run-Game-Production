using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Stage_Name;
    [TextArea(3,5)][SerializeField] string Stage_Information;

    [Header("≈ÿΩ∫∆Æ")]
    public string Name;
    [TextArea(3, 5)] public string Information;

    [SerializeField] int Stage_Num;
    Stage_Info SI;
    private void Awake()
    {
        SI = FindObjectOfType<Stage_Info>();
    }

    public void OnWindow()
    {
        SI.Stage_Name.text = Stage_Name.text;
        SI.Stage_Information.text = Stage_Information;
        SI.Stage_Num = Stage_Num;

        SI.Name = Name;
        SI.Information = Information;

        SI.OnStage_Info();
    }
}