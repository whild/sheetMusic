using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class React_Break : PlayerReactCore
{
    private int instrumentIndex = 0;
    [SerializeField] UnityAction breakAction;
    private Material targetMaterial;
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

        breakAction.Invoke();
    }

    protected override void Setup2D()
    {
        base.Setup2D();
        targetMaterial = spriteRenderer.material;
    }

    protected override void Setup3D()
    {
        base.Setup3D();
        targetMaterial = meshRenderer.material;
    }
}
