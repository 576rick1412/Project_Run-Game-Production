using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proto_move_Scene : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Loading_Manager.LoadScene("Scene_2");
        }
    }
}
