using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEvent : MonoBehaviour
{
    [SerializeField] int syncroIndex;
    private IObjectEvent[] objectEvents;

    private void Awake()
    {
        this.gameObject.tag = TagManager.laserEvent;
        objectEvents = SyncroOjbect.GetSyncroObjects(syncroIndex);
    }

    public void Event()
    {
        foreach (var eve in objectEvents)
        {
            eve.Event();
        }
    }
}
