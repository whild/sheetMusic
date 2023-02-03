using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    forward,
    back,
    up,
    down,
    right,
    left,
}
public class ObjectInteractCore : MonoBehaviour, IInteract
{
    public virtual void Interact()
    {

    }

    public virtual void Interact(float val)
    {

    }
}
