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
        GameManager.GM.data.floorSpeedValue *= 1.05f;
        GameManager.GM.data.BGSpeedValue    *= 1.05f;

        Debug.Log("�� ����");
        Instantiate(spanwMaps[spanwIndex],new Vector3(28, 0,0),Quaternion.identity);

        spanwIndex = Random.Range(0, spanwMaps.Length);
    }
}
