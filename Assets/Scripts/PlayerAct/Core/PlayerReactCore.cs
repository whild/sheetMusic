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
        CircleCollider2D col;
        if (!TryGetComponent(out col))
        {
            col = this.gameObject.AddComponent<CircleCollider2D>();
        }
        col.isTrigger = true;
        col.radius = 2;
    }

    protected virtual void Setup3D()
    {
        /*
        SphereCollider col;
        if (!TryGetComponent(out col))
        {
            col = this.gameObject.AddComponent<SphereCollider>();
        }
        col.isTrigger = true;
        col.radius = 2;
        */
    }
}
