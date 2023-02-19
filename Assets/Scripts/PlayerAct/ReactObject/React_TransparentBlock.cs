using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React_TransparentBlock : PlayerReactCore
{
    [SerializeField] private Material[] materialContainer;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Setup2D()
    {
        materialContainer = spriteRenderer.materials;
        spriteRenderer.materials = GetTransparentMat();
    }

    protected override void Setup3D()
    {
        materialContainer = meshRenderer.materials;
        meshRenderer.materials = GetTransparentMat();
    }

    public override void PlayerReact(int instrumentValue)
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.materials = materialContainer;
            return;
        }

        if(meshRenderer != null)
        {
            meshRenderer.materials = materialContainer;
            return;
        }
    }

    private Material[] GetTransparentMat()
    {
        return new Material[] { ResourceData<Material>.GetData("Material/Transparent") };
    }

}
