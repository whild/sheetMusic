using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触ったときの反応があるオブジェクトのベース
/// </summary>
public class ContactInteractCore : MonoBehaviour, IContactInteract
{
    [TagSelector]
    [SerializeField] protected string[] targetTags = new string[] { };
    [SerializeField] protected Dimension targetDimension;
    [SerializeField] protected MoveDirection contactDirection;

    protected Collider col;
    protected Collider2D col2D;

    protected virtual void Awake()
    {
        GameManager.CheckDemansionComponent(this.transform,ref col,ref col2D);
    }

    #region 基本機能
    private void OnCollisionEnter(Collision collision)
    {
        if (TagManager.IsRightTag(targetTags, collision.gameObject.tag))
        {
            if (IsRightDirection(collision.transform))
            {
                OnContact(collision);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (TagManager.IsRightTag(targetTags, collision.gameObject.tag))
        {
            OnUnContact(collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagManager.IsRightTag(targetTags, collision.gameObject.tag))
        {
            if (IsRightDirection(collision.transform))
            {
                OnContact(collision);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (TagManager.IsRightTag(targetTags, collision.gameObject.tag))
        {
            OnUnContact(collision);
        }
    }

    public virtual void OnContact(Collision collision)
    {

    }
    public virtual void OnContact(Collision2D collision)
    {

    }
    public virtual void OnUnContact(Collision collision)
    {

    }
    public virtual void OnUnContact(Collision2D collision)
    {

    }
    #endregion
    private bool IsRightDirection(Transform trans)
    {
        switch (contactDirection)
        {
            case MoveDirection.left:
                return trans.position.x < this.transform.position.x;
            case MoveDirection.right:
                return trans.position.x > this.transform.position.x;
            case MoveDirection.up:
                return trans.position.y > this.transform.position.y;
            case MoveDirection.down:
                return trans.position.y < this.transform.position.y;
            case MoveDirection.forward:
                return trans.position.z > this.transform.position.z;
            case MoveDirection.back:
                return trans.position.z < this.transform.position.z;
            default:
                return false;
        }
    }
}
