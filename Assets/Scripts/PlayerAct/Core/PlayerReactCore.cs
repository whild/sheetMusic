using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReactCore : MonoBehaviour, IPlayerReact
{
    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        if(TryGetComponent(out meshRenderer))
        {
            Setup3D();
            return;
        }
        if(TryGetComponent(out spriteRenderer))
        {
            Setup2D();
            return;
        }
    }

    public virtual void PlayerReact(int instrumentValue)
    {

    }
    protected virtual void Setup2D()
    {

    }

    protected virtual void Setup3D()
    {

    }
}
