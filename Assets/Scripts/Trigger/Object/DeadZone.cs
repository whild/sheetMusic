using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが触ると死ぬオブジェクトです。
/// </summary>
public class DeadZone : TriggerInteractCore
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnTrigger(Collider collision)
    {
        StageManager.Instance.Retry();
    }

    public override void OnTrigger(Collider2D collision)
    {
        StageManager.Instance.Retry();
    }
}
