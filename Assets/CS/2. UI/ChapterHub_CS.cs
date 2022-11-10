using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterHub_CS : MonoBehaviour
{
    public GameObject[] Stage_Hub;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Chapter1() { Instantiate(Stage_Hub[0]); }
    public void Chapter2() { Instantiate(Stage_Hub[1]); }
    public void Chapter3() { Instantiate(Stage_Hub[2]); }
    public void Chapter4() { Instantiate(Stage_Hub[3]); }
    public void Chapter5() { Instantiate(Stage_Hub[4]); }
    public void Chapter6() { Instantiate(Stage_Hub[5]); }
}
