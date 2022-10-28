using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterHub_CS : MonoBehaviour
{
    public GameObject Stage1_Hub;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Chapter1() { Instantiate(Stage1_Hub); }
}
