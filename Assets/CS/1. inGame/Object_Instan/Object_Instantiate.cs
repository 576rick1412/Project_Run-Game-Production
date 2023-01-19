using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Instantiate : MonoBehaviour
{
    [System.Serializable]
    public struct Map
    {
        public GameObject[] spanwMaps; // ������ �� ���
    }
    public Map[] maps;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InstanceMap()
    {
        int mapIndex = 0; // ������ ���� ��ȣ - ������ 0���� ����
        int spanwIndex = 0; // ������ ���� ���� ��ȣ - ������ 0���� ����

        GameManager.GM.data.floorSpeedValue *= 1.05f;
        GameManager.GM.data.BGSpeedValue    *= 1.05f;

        // ������ �� ���� ���� ������
        mapIndex = Random.Range(0, maps.Length);
        spanwIndex = Random.Range(0, maps[mapIndex].spanwMaps.Length);

        Debug.Log("�� ����");
        Instantiate(maps[mapIndex].spanwMaps[spanwIndex],new Vector3(28, 0,0),Quaternion.identity);

    }
}
