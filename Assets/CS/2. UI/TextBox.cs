using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextBox : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;

    [SerializeField] int branch;
    [SerializeField] RunGame_EX RunGame_EX;

    [SerializeField] public string Stage_Num;
    [SerializeField] public string Stage_Des;

    void Start()
    {
        STG_Excel();
    }

    /*void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Loading_Manager.LoadScene("Scene_2", Stage_Num, Stage_Des);
        }
    }*/
    void STG_Excel()
    {
        for (int i = 0; i < RunGame_EX.STSheet.Count; ++i)
        {
            if (RunGame_EX.STSheet[i].ST_branch == branch)
            {
                Stage_Num = Name.text = RunGame_EX.STSheet[i].ST_name;
                Stage_Des = Description.text = RunGame_EX.STSheet[i].ST_description;
            }
        }
    }
}
