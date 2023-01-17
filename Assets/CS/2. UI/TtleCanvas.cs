using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TtleCanvas : MonoBehaviour
{
    [SerializeField] RunGame_EX runGame_EX;

    string stageDes;
    [SerializeField] Animator BG;

    [SerializeField] GameObject BGI_01, BGI_02;
    bool isClick;

    void Start()
    {
        isClick = false;
        STGExcel();

        GameManager.GM.Fade(BGI_01, BGI_02);
    }

    public void GoMain()
    {
        if (!isClick)
        {
            BG.SetBool("Start", true);
            Invoke("RoadLoby", 3f);
            isClick = true;
        }
    }

    void RoadLoby() { Loading_Manager.LoadScene("Mian_Loby_Scene", stageDes); }
    void STGExcel()
    {
        int branch = Random.Range(1, runGame_EX.StartSheet.Count + 1);

        for (int i = 0; i < runGame_EX.StartSheet.Count; ++i)
        {
            if (runGame_EX.StartSheet[i].STR_branch == branch)
            {
                stageDes = runGame_EX.StartSheet[i].STR_description;
            }
        }
    }
}
