using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Json_Test : MonoBehaviour
{
    // Start is called before the first frame update 
    void Start()
    {
        // file save 
        Data data3 = new Data();
        data3.m_nLevel = 89;
        data3.m_vecPositon = new Vector3(8.1f, 9.2f, 7.2f);

        var save = JsonUtility.ToJson(data3);
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "/TestJson.json"), save); // 기본

        //File.WriteAllText(Path.Combine("jar:file://" + Application.dataPath + "!/assets"), save); 안드용 22
        /*
        var save = JsonUtility.ToJson(data3);

        if (Application.platform == RuntimePlatform.Android)    // AOS 용
            File.WriteAllText(Path.Combine("jar:file://" + Application.streamingAssetsPath, "!/assets"), save);

        if(Application.platform == RuntimePlatform.IPhonePlayer) // IOS 용
            File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "/Raw"), save);

        else  File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "/TestJson.json"), save); // 에디터 용
        */

        // file load 
        var str2 = File.ReadAllText (Path.Combine(Application.streamingAssetsPath, "/TestJson.json"));
        Data data4 = JsonUtility.FromJson<Data>(str2);
        data4.printData();
    }

    // Update is called once per frame 
    void Update()
    {

    }
}

[System.Serializable]
public class Data
{
    public int m_nLevel;
    public Vector3 m_vecPositon;

    public void printData()
    {
        Debug.Log("Level : " + m_nLevel);
        Debug.Log("Position : " + m_vecPositon);
    }
    
}

