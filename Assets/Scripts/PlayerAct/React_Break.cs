using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React_Break : PlayerReactCore
{
    private int instrumentIndex = 0;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void PlayerReact(int instrumentValue)
    {
        if (instrumentIndex == instrumentValue)
        {
            Debug.Log("Break");
        }
    }
}
