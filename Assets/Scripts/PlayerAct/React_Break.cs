using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React_Break : PlayerReactCore
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void PlayerReact()
    {
        Debug.Log("Break");
    }
}
