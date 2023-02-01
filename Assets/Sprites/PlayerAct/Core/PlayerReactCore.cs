using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReactCore : MonoBehaviour, IPlayerReact
{
    protected void Awake()
    {
        var col = this.gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 2;
    }

    public virtual void PlayerReact()
    {

    }
}
