using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Button_CS : MonoBehaviour
{
    int branch;
    [SerializeField] RunGame_EX RunGame_EX;

    [SerializeField] string Stage_Des;
    [SerializeField] Animator BG;

    bool Click;

    void Start()
    {
        Click = false;
        branch = Random.Range(1, RunGame_EX.StartSheet.Count + 1);
        STG_Excel();
    }
    public void GoMain()
    {
        if (!Click)
        {
            BG.SetBool("Start", true);
            Invoke("RoadLoby", 3f);
            Click = true;
        }
    }
    void RoadLoby() { Loading_Manager.LoadScene("Mian_Loby_Scene", Stage_Des); }
    void STG_Excel()
    {
        for (int i = 0; i < RunGame_EX.StartSheet.Count; ++i)
        {
            if (RunGame_EX.StartSheet[i].STR_branch == branch)
            {
                Stage_Des = RunGame_EX.StartSheet[i].STR_description;
            }
        }
    }
}
