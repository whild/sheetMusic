using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReactCore : MonoBehaviour, IPlayerReact
{
    protected Collider collider;
    protected Collider2D collider2D;

    private MeshFilter meshFilter;
    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        var col = this.gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 2;
    }

    public virtual void PlayerReact()
    {

    }
}
