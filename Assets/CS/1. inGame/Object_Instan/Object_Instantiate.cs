using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Instantiate : MonoBehaviour
{
    [System.Serializable]
    public struct Map
    {
        public GameObject[] spanwMaps; // 생성할 맵 목록
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
        int mapIndex = 0; // 생성할 맵의 번호 - 시작은 0으로 고정
        int spanwIndex = 0; // 생성할 맵의 세부 번호 - 시작은 0으로 고정

        GameManager.GM.data.floorSpeedValue *= 1.05f;
        GameManager.GM.data.BGSpeedValue    *= 1.05f;

        // 생성할 맵 지정 랜덤 돌리기
        mapIndex = Random.Range(0, maps.Length);
        spanwIndex = Random.Range(0, maps[mapIndex].spanwMaps.Length);

        Debug.Log("맵 생성");
        Instantiate(maps[mapIndex].spanwMaps[spanwIndex],new Vector3(28, 0,0),Quaternion.identity);

    }
}
