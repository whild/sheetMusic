using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : TriggerInteractCore
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void OnTrigger(Collider collision)
    {
        StageManager.Instance.isGetKey = true;
    }
}
