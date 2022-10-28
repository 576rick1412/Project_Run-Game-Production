using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabUI : MonoBehaviour
{
    bool OnTabCheck;
    [SerializeField]GameObject ALLb;
    [SerializeField]GameObject GameWindow;
    Animator anime;

    private void Start() { anime = GetComponent<Animator>(); OnTabCheck = true; ALLb.SetActive(false); }

    public void OnTabUI()
    {
        switch(OnTabCheck)
        {
            case true: anime.SetInteger("TabNum", 2); OnTabCheck = false; ALLb.SetActive(true); break;
            case false:anime.SetInteger("TabNum", 1); OnTabCheck = true; ALLb.SetActive(false); break;
        }
    }
    
    public void DesThis() { Destroy(GameWindow); }
}