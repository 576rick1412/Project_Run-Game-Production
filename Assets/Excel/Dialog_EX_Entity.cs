using UnityEngine;
using TMPro;
[System.Serializable]
public class Dialog_EX_Entity
{
    public int DIA_branch;
    public string DIA_name;
    [TextArea(3, 5)] public string DIA_dialog;
    public bool DIA_cutscene;
    public bool DIA_End;
}