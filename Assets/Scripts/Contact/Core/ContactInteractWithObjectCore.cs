using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactInteractWithObjectCore : ContactInteractCore
{
    [SerializeField] int syncroIndex;
    protected IObjectEvent[] objectEvent;

    protected override void Awake()
    {
        base.Awake();
        this.objectEvent = SyncroOjbect.GetSyncroObjects(syncroIndex);
    }

    protected void InvokeEvent()
    {
        foreach (var eve in objectEvent)
        {
            eve.Event();
        }
    }
    protected void InvokeEvent(float val)
    {
        foreach (var eve in objectEvent)
        {
            eve.Event(val);
        }
    }
}
