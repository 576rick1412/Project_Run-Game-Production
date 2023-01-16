using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMove : MonoBehaviour
{
    public float moveTime;

    private float dTime = 0;
    private int BGI_Num = 0;

    AsyncOperation op;
    public GameObject[] BGIs;
    void Start()
    {

        op = SceneManager.LoadSceneAsync("Title_Scene");
        op.allowSceneActivation = false;

        for (int i = 0; i < BGIs.Length; i++) BGIs[i].SetActive(false);

        BGIs[0].SetActive(true);
    }
    void FixedUpdate()
    {
        Time_BGI();
    }
    void Time_BGI()
    {
        dTime += Time.deltaTime;

        if (dTime > moveTime)
        {
            Active_BGI();
            dTime = 0f;
        }
    }
    void Active_BGI()
    {
        if (BGI_Num < (BGIs.Length -1)){
            ++BGI_Num;
            GameManager.GM.Fade(BGIs[BGI_Num - 1], BGIs[BGI_Num]);
        }
        else{ GameManager.GM.Fade(op); }
    }
}
