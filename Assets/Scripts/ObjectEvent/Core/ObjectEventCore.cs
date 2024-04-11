using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 特別なイベントがあるオブジェクト
/// syncroIndexが同じオブジェクトに反応
/// </summary>
public class ObjectEventCore : MonoBehaviour, IObjectEvent
{
    public int syncroIndex;
    public virtual void Event()
    {

    }

    public virtual void Event(float val)
    {

    }

    public virtual int GetSyncroIndex()
    {
        return -1;
    }
}
