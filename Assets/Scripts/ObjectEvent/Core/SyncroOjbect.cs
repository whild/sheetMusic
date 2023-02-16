using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncroOjbect
{
    public static IObjectEvent[] GetSyncroObjects(int index)
    {
        List<IObjectEvent> objects = new List<IObjectEvent>();

        var container = (ObjectEventCore[])Resources.FindObjectsOfTypeAll(typeof(ObjectEventCore));

        foreach (var item in container)
        {
            if(item.syncroIndex == index)
            {
                objects.Add(item);
            }
        }

        return objects.ToArray();
    }

}
