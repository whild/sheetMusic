using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーの能力で壊れるオブジェクト
/// </summary>
public class React_Break : PlayerReactCore
{
    [SerializeField] public int syncroIndex;
    [SerializeField] GameObject breakObject;
    private int instrumentIndex = 0;

    [SerializeField] Material breakMaterial;
    private Material[] targetMaterials;
    int lengthContainer;

    [SerializeField] UnityEvent breakEvent;

    protected override void Awake()
    {
        base.Awake();
        if(breakObject == null)
        {
            breakObject = this.gameObject;
        }
        breakMaterial = ResourceData<Material>.GetData("Material/" + "BreakMaterial");
    }

    public override void PlayerReact(int instrumentValue)
    {
        if (instrumentIndex == instrumentValue)
        {
            if (AudioManager.isMike)//マイクを使ったのか判断して
            {//もっと強い能力
                Break();
                return;
            }
            else
            {
                CheckBreak();
                return;
            }
        }
    }

    protected override void Setup2D()
    {
        targetMaterials = spriteRenderer.materials;
        lengthContainer = targetMaterials.Length;
    }

    protected override void Setup3D()
    {
        targetMaterials = meshRenderer.materials;
        lengthContainer = targetMaterials.Length;
    }

    private void CheckBreak()
    {
        if (targetMaterials.Length == lengthContainer)
        {
            AddBreakMaterial();
        }
        else if (targetMaterials.Length == lengthContainer + 1)
        {
            Break();
        }
    }

    private void Break()
    {
        Destroy(breakObject);
        breakEvent.Invoke();
    }

    private void AddBreakMaterial()
    {
        Array.Resize(ref targetMaterials, lengthContainer + 1);
        targetMaterials[lengthContainer] = breakMaterial;
        if(meshRenderer != null)
        {//3D
            meshRenderer.materials = targetMaterials;
        }
        else
        {
            spriteRenderer.materials = targetMaterials;
        }
    }
}
