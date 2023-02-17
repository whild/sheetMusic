using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncroOjbect
{
    public static T[] GetSyncroObjects<T>(int index) where T : Object
    {
        return (T[])Resources.FindObjectsOfTypeAll(typeof(T));
    }

}
