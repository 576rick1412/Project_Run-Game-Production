using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Start_Move_CS : MonoBehaviour
{
    public static Singleton instance;

    public float Move_Time;

    private float DTime = 0;
    private int BGI_Num = 0;

    AsyncOperation op;
    public GameObject[] BGI;
    void Start()
    {

        op = SceneManager.LoadSceneAsync("Title_Scene");
        op.allowSceneActivation = false;

        for (int i = 0; i < BGI.Length; i++) BGI[i].SetActive(false);

        BGI[0].SetActive(true);
    }
    void FixedUpdate()
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
        Fade_effect oc = GameObject.Find("Hephaestus_Canvas").GetComponent<Fade_effect>();
        if (BGI_Num < (BGI.Length -1)){
            ++BGI_Num;

            oc.Fade(BGI[BGI_Num - 1], BGI[BGI_Num]);
        }
        else{
            oc.Fade(op);
        }
    }
}
