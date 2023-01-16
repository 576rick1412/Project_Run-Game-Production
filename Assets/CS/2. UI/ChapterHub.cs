using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterHub : MonoBehaviour
{
    public GameObject[] stageHubs;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Chapter1() { Instantiate(stageHubs[0]); }
    public void Chapter2() { Instantiate(stageHubs[1]); }
    public void Chapter3() { Instantiate(stageHubs[2]); }
    public void Chapter4() { Instantiate(stageHubs[3]); }
    public void Chapter5() { Instantiate(stageHubs[4]); }
    public void Chapter6() { Instantiate(stageHubs[5]); }
}
