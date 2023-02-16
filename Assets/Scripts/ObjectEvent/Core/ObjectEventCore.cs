using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEventCore : MonoBehaviour, IObjectEvent
{
    public int syncroIndex;
    public virtual void Event()
    {

    }

    public virtual void Event(float val)
    {

    }
}
