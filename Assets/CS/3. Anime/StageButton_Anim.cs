using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton_Anim : MonoBehaviour
{
    [SerializeField]bool OnStageCheck;
    [SerializeField] bool OnCheck;

    Animator anime;

    private void Start() { anime = GetComponent<Animator>(); OnStageCheck = false; OnCheck = false; }

    private void Update() { if (OnStageCheck == true && Input.anyKeyDown) { OnStageUI(); OnCheck = true; Invoke("OffCheck", 0.15f); } } 

    public void OnStageUI()  
    {
        if (OnCheck == true) { OnCheck = false; return; }

        switch (OnStageCheck)
        {
            case true: anime.SetInteger("ButtonNum", 1); OnStageCheck = false; break;
            case false: anime.SetInteger("ButtonNum", 2); OnStageCheck = true; break;
        }
    }
    void OffCheck() { OnCheck = false; }
}