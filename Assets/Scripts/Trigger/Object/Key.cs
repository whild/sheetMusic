using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : TriggerInteractCore
{
    public override void OnTrigger(Collider collision)
    {
        StageManager.Instance.isGetKey = true;
    }
}
