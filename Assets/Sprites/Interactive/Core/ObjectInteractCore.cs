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


    public static Vector3 GetDirection(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.forward:
                return Vector3.forward;
            case MoveDirection.back:
                return Vector3.back;
            case MoveDirection.up:
                return Vector3.up;
            case MoveDirection.down:
                return Vector3.down;
            case MoveDirection.right:
                return Vector3.right;
            case MoveDirection.left:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }
}
