using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabUI : MonoBehaviour
{
    bool OnTabCheck;
    Animator anime;

    private void Start() { anime = GetComponent<Animator>(); OnTabCheck = true; }

    public void OnTabUI()
    {
        switch(OnTabCheck)
        {
            case true: anime.SetInteger("TabNum", 2); OnTabCheck = false;break;
            case false:anime.SetInteger("TabNum", 1); OnTabCheck = true; break;
        }
    }
}