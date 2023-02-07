using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractCore : MonoBehaviour, ITriggerInteract
{
    [TagSelector]
    [SerializeField] protected string[] targetTags = new string[1] { "Player" };
    [SerializeField] protected Dimension targetDimension;

    private void OnTriggerEnter(Collider other)
    {
        if(TagManager.IsRightTag(targetTags, other.gameObject.tag))
        {
            OnTrigger(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (TagManager.IsRightTag(targetTags, other.gameObject.tag))
        {
            OnTrigger(other);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (TagManager.IsRightTag(targetTags, other.gameObject.tag))
        {
            OnUnTrigger(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (TagManager.IsRightTag(targetTags, other.gameObject.tag))
        {
            OnUnTrigger(other);
        }
    }


    public virtual void OnTrigger(Collider collision)
    {
    }

    public virtual void OnTrigger(Collider2D collision)
    {
    }

    public virtual void OnUnTrigger(Collider collision)
    {
    }

    public virtual void OnUnTrigger(Collider2D collision)
    {
    }
}
