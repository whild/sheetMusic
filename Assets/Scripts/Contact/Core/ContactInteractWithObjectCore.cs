using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactInteractWithObjectCore : ContactInteractCore
{
    [SerializeField] GameObject targetObj;
    protected IObjectEvent objectEvent;

    protected override void Awake()
    {
        base.Awake();
        if(targetObj == null)
        {
            targetObj = this.gameObject;
        }
        this.objectEvent = targetObj.GetComponent<IObjectEvent>();
    }
}
