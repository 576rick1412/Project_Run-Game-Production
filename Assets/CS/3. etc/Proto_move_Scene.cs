using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proto_move_Scene : MonoBehaviour
{
    public GameObject[] BGI;
    void Start()
    {
        Random_BGI();
    }

    void Update()
    {

    }

    void Random_BGI()
    {
        int num = Random.Range(0, BGI.Length);
        for (int i = 0; i < BGI.Length; i++)
        {
            if (i == num) continue;
            BGI[i].SetActive(false);
        }
    }
}
