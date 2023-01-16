using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Button : MonoBehaviour
{
    int branch;
    [SerializeField] RunGame_EX runGame_EX;

    [SerializeField] string stageDes;
    [SerializeField] Animator BG;

    bool click;

    void Start()
    {
        click = false;
        branch = Random.Range(1, runGame_EX.StartSheet.Count + 1);
        STGExcel();
    }
    public void GoMain()
    {
        if (!click)
        {
            BG.SetBool("Start", true);
            Invoke("RoadLoby", 3f);
            click = true;
        }
    }
    void RoadLoby() { Loading_Manager.LoadScene("Mian_Loby_Scene", stageDes); }
    void STGExcel()
    {
        for (int i = 0; i < runGame_EX.StartSheet.Count; ++i)
        {
            if (runGame_EX.StartSheet[i].STR_branch == branch)
            {
                stageDes = runGame_EX.StartSheet[i].STR_description;
            }
        }
    }
}
