using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteracCore : MonoBehaviour, IInteract
{
    protected void Awake()
    {
        this.gameObject.tag = "Interact";
    }
    public virtual void Interact()
    {
        throw new System.NotImplementedException();
    }
}
