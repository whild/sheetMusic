using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReactCore : MonoBehaviour, IPlayerReact
{
    private MeshFilter meshFilter;
    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        if(TryGetComponent(out meshFilter))
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

    private void Setup2D()
    {
        var col = this.gameObject.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = 2;
    }

    private void Setup3D()
    {
        var col = this.gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 2;
    }
}
