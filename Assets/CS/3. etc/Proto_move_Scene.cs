using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Proto_move_Scene : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;

    [SerializeField] int  branch;
    [SerializeField] RunGame_EX RunGame_EX;

    [SerializeField] public string Stage_Num;
    [SerializeField] public string Stage_Des;

    public GameObject[] BGI;
    void Start()
    {
        Random_BGI();
        STG_Excel();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Loading_Manager.LoadScene("Scene_2", Stage_Num, Stage_Des);
        }
    }

    void Random_BGI()
    {
        int num = Random.Range(0, BGI.Length);
        for (int i = 0; i < BGI.Length; i++)
        {
            if (i == num) continue;
            BGI[i].SetActive(false);
        }
    }

    void STG_Excel()
    {
        for(int i = 0; i < RunGame_EX.STSheet.Count; ++i)
        {
            if (RunGame_EX.STSheet[i].ST_branch == branch)
            {
                Stage_Num = Name.text = RunGame_EX.STSheet[i].ST_name;
                Stage_Des = Description.text = RunGame_EX.STSheet[i].ST_description;
            }
        }
    }
}
