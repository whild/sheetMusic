using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactIInteractCore : MonoBehaviour, IContactInteract
{

    [TagSelector]
    [SerializeField] protected string[] targetTags = new string[] { };
    [SerializeField] protected MoveDirection contactDirection;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsRightTag(collision.gameObject.tag))
        {
            if (IsRightDirection(collision.transform))
            {
                OnContact();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (IsRightTag(collision.gameObject.tag))
        {
            OnUnContact();
        }
    }

    public virtual void OnContact()
    {

    }

    public virtual void OnUnContact()
    {

    }

    private bool IsRightTag(string targetTag)
    {
        string tag = Array.Find(targetTags, tag => tag == targetTag);
        if(tag != null)
        {
            return true;
        }
        return false;
    }

    private bool IsRightDirection(Transform trans)
    {
        switch (contactDirection)
        {
            case MoveDirection.forward:
                return trans.position.x > this.transform.position.x;
            case MoveDirection.back:
                return trans.position.x < this.transform.position.x;
            case MoveDirection.up:
                return trans.position.y > this.transform.position.y;
            case MoveDirection.down:
                return trans.position.y < this.transform.position.y;
            case MoveDirection.right:
                return trans.position.z > this.transform.position.z;
            case MoveDirection.left:
                return trans.position.z < this.transform.position.z;
            default:
                return false;
        }
    }
}
