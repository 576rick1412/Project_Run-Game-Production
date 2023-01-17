using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Instantiate : MonoBehaviour
{
    [SerializeField] GameObject[] spanwMaps; // 생성할 맵 목록
    int spanwIndex = 0; // 생성할 맵의 번호 - 시작은 0으로 고정

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) InstanceMap();
    }

    public void InstanceMap()
    {
        Debug.Log("fsghui");
        Instantiate(spanwMaps[spanwIndex],new Vector3(28, 0,0),Quaternion.identity);

        spanwIndex = Random.Range(0, spanwMaps.Length);
    }
}
