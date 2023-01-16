using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextBox : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;

    [SerializeField] int branch;
    [SerializeField] RunGame_EX runGame_EX;

    [SerializeField] public string stageNum;
    [SerializeField] public string stageDes;

    void Start()
    {
        STGExcel();
    }

    /*void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Loading_Manager.LoadScene("Scene_2", Stage_Num, Stage_Des);
        }
    }*/
    void STGExcel()
    {
        for (int i = 0; i < runGame_EX.STSheet.Count; ++i)
        {
            if (runGame_EX.STSheet[i].ST_branch == branch)
            {
                stageNum = name.text = runGame_EX.STSheet[i].ST_name;
                stageDes = description.text = runGame_EX.STSheet[i].ST_description;
            }
        }
    }
}
