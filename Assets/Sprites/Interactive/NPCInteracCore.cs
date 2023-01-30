using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteracCore : MonoBehaviour, IInteract
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
