using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class React_Break : PlayerReactCore
{
    [SerializeField] public int syncroIndex;
    [SerializeField] GameObject breakObject;
    private int instrumentIndex = 0;

    [SerializeField] Material breakMaterial;
    private Material[] targetMaterials;
    int lengthContainer;
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
            CheckBreak();
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
            Destroy(breakObject);
        }
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
