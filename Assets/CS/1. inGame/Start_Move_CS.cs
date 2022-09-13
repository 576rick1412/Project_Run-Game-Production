using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Start_Move_CS : MonoBehaviour
{
    public TextMeshProUGUI Description;
    public GameObject Play_Button;
    [SerializeField] RunGame_EX RunGame_EX;

    public float Move_Time;

    private float DTime = 0;
    private int BGI_Num = 0;

    public GameObject[] BGI;
    void Start()
    {
        for (int i = 0; i < BGI.Length; i++) BGI[i].SetActive(false);

        Active_BGI();
        STG_Excel();
    }
    void Update()
    {
        Time_BGI();
    }
    void Time_BGI()
    {
        DTime += Time.deltaTime;

        if (DTime > Move_Time)
        {
            Active_BGI();
            DTime = 0f;
        }
    }
    void Active_BGI()
    {
        if (BGI_Num < (BGI.Length -1)){
            ++BGI_Num;      
            BGI[BGI_Num].SetActive(true);
            BGI[BGI_Num - 1].SetActive(false); 
        }
        else{
            BGI_Num = 0;    
            BGI[BGI_Num].SetActive(true); 
            BGI[BGI.Length - 1].SetActive(false); 
        }
    }
    void STG_Excel()
    {
        int branch = Random.Range(1, 6);
        for (int i = 0; i < RunGame_EX.StartSheet.Count; ++i)
        {
            if (RunGame_EX.StartSheet[i].STR_branch == branch)
            {
                Description.text = RunGame_EX.StartSheet[i].STR_description;
            }
        }
    }
    public void Play_B()
    {
        SceneManager.LoadSceneAsync("Title_Scene");
    }
}
