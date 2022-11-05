using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Chapter_1 : ScriptableObject
{
    public List<Stage_1_Entity>  Stage_1;
    public List<Stage_2_Entity>  Stage_2;
    public List<Stage_3_Entity>  Stage_3;
    public List<Stage_4_Entity>  Stage_4;
    public List<Stage_5_Entity>  Stage_5;
    public List<Stage_6_Entity>  Stage_6;
    public List<Stage_7_Entity>  Stage_7;
    public List<Stage_8_Entity>  Stage_8;
    public List<Stage_9_Entity>  Stage_9;
    public List<Stage_10_Entity> Stage_10;

    public static implicit operator Chapter_1(Chapter_2 v)
    {
        throw new NotImplementedException();
    }

    public static implicit operator Chapter_1(Chapter_3 v)
    {
        throw new NotImplementedException();
    }

    public static implicit operator Chapter_1(Chapter_4 v)
    {
        throw new NotImplementedException();
    }

    public static implicit operator Chapter_1(Chapter_5 v)
    {
        throw new NotImplementedException();
    }

    public static implicit operator Chapter_1(Chapter_6 v)
    {
        throw new NotImplementedException();
    }
}