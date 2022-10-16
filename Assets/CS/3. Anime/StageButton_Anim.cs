using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton_Anim : MonoBehaviour
{
    bool OnStageCheck;

    private Camera _MainCam;
    Animator anime;

    private void Start() { anime = GetComponent<Animator>(); OnStageCheck = false; _MainCam = Camera.main; }

    private void Update()
    {
        // if (OnStageCheck == true && Input.anyKey) OnStageUI();
    }

    public void OnStageUI()  
    {
        switch (OnStageCheck)
        {
            case true: anime.SetInteger("ButtonNum", 1); OnStageCheck = false; break;
            case false: anime.SetInteger("ButtonNum", 2); OnStageCheck = true; break;
        }
    }
}