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
            Loading_Manager.LoadScene("Scene_2","1 - 1", 
                "세계의 정의를 위해 뛰어다니는 어느 용병의 이야기이다.");
        }
    }
}
