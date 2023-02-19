using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurificationObject : MonoBehaviour
{
    public int SyncroIndex;
    public GameObject corruptedObject;
    public GameObject cleanObject;
    public UnityEvent cleanEvent;

    public void Purification()
    {
        corruptedObject.SetActive(false);
        cleanObject.SetActive(true);
        cleanEvent.Invoke();
    }
}
