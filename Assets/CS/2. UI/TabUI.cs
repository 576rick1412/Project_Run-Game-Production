using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabUI : MonoBehaviour
{
    bool isOnTab;
    [SerializeField]GameObject aLLb;
    [SerializeField]GameObject gameWindow;
    Animator anime;

    private void Start() { anime = GetComponent<Animator>(); isOnTab = true; aLLb.SetActive(false); }

    public void OnTabUI()
    {
        switch(isOnTab)
        {
            case true: anime.SetInteger("TabNum", 2); isOnTab = false; aLLb.SetActive(true); break;
            case false:anime.SetInteger("TabNum", 1); isOnTab = true; aLLb.SetActive(false); break;
        }
    }
    
    public void DesThis() { Destroy(gameWindow); }
    public void LoadLoby() { SceneManager.LoadScene("Mian_Loby_Scene"); }
    public void LoadChapter() { SceneManager.LoadScene("Chapter_Hub"); }
}