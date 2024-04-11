﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactInteractWithObjectCore : ContactInteractCore
{
    [SerializeField] int syncroIndex;
    protected IObjectEvent[] objectEvent;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Syncro());
    }

    protected void InvokeEvent(float val = float.MinValue)
    {
        foreach (var eve in objectEvent)
        {
            if (eve.GetSyncroIndex() == syncroIndex)
            {
                if(val == float.MinValue)
                {
                    eve.Event(val);
                }
                else
                {
                    eve.Event();
                }
            }
        }
    }

    IEnumerator Syncro()
    {
        yield return new WaitForSeconds(0.5f);
        this.objectEvent = SyncroOjbect.GetSyncroObjects<ObjectEventCore>();
    }
}
