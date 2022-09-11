using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Proto_move_Scene : MonoBehaviour
{
    public TextMeshProUGUI Description;

    [SerializeField] int branch;
    [SerializeField] RunGame_EX RunGame_EX;

    public GameObject[] BGI;
    void Start()
    {
        Random_BGI();
        STG_Excel();
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
        for (int i = 0; i < RunGame_EX.StartSheet.Count; ++i)
        {
            if (RunGame_EX.StartSheet[i].STR_branch == branch)
            {
                Description.text = RunGame_EX.StartSheet[i].STR_description;
            }
        }
    }
}
