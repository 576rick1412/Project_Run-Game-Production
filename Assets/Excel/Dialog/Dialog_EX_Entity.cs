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

    public bool Left_Blackout;
    public bool Mid_Blackout;
    public bool Right_Blackout;
    public bool BGI_Blackout;
}

[System.Serializable]
public class STR_EX_Entity
{
    public int STR_branch;
    [TextArea(3, 5)] public string STR_description;
}
[System.Serializable]

public class STG_EX_Entity
{
    public int ST_branch;
    public string ST_name;
    [TextArea(3, 5)] public string ST_description;
}