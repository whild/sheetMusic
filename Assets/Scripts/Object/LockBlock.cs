using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ËõªÇú°ð¶ª¹ªë«ª«Ö«¸«§«¯«È
/// </summary>
public class LockBlock : ContactInteractCore
{
    protected override void Awake()
    {
        base.Awake();
        Array.Resize(ref targetTags, 1);
        targetTags[0] = TagManager.player;
    }


    public override void OnContact(Collision collision)
    {
        UnlockEvent();
    }

    public override void OnContact(Collision2D collision)
    {
        UnlockEvent();
    }

    private void UnlockEvent()
    {
        if (StageManager.Instance.isGetKey)
        {
            //OpenSound
            StageManager.Instance.isGetKey = false;
            GameObject.Destroy(this.gameObject);
        }
    }
}
