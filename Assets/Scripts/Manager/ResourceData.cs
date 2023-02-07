using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceData<T> where T: Object
{
    public static Dictionary<string, T> Data = new Dictionary<string, T>();

    public static T GetData(string path) 
    {
        if (Data.ContainsKey(path))
        {
            return Data[path];
        }
        else
        {
            Data.Add(path, Resources.Load<T>(path));
            return Data[path];
        }
    }

}
