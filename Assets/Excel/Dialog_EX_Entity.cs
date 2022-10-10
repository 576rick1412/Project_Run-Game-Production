using UnityEngine;
using TMPro;
[System.Serializable]
public class Dialog_EX_Entity
{
    public int DIA_branch;
    public string DIA_name;
    [TextArea(3, 5)] public string DIA_dialog;
    public string DIA_BGI;
    public bool DIA_End;

    public string DIA_cutscene;

    public string Left_Character;
    public string Mid_Character;
    public string Right_Character;
}