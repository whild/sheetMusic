using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserEvent : MonoBehaviour
{
    [SerializeField] int syncroIndex;
    private IObjectEvent[] objectEvents;
    [SerializeField] UnityEvent completeEvent;

    private void Awake()
    {
        this.gameObject.tag = TagManager.laserEvent;
        StartCoroutine(Syncro());
    }

    public void Event()
    {
        if(objectEvents == null)
        {
            return;
        }
        foreach (var eve in objectEvents)
        {
            if (syncroIndex == eve.GetSyncroIndex())
            {
                eve.Event();
            }
        }
        completeEvent.Invoke();
        Destroy(this);

    }

    IEnumerator Syncro()
    {
        yield return new WaitForSeconds(0.5f);
        this.objectEvents = SyncroOjbect.GetSyncroObjects<ObjectEventCore>();
    }
}
