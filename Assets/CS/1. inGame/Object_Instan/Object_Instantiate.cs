using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Instantiate : MonoBehaviour
{
    [SerializeField] GameObject[] spanwMaps; // ������ �� ���
    int spanwIndex = 0; // ������ ���� ��ȣ - ������ 0���� ����

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
