using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEvent : MonoBehaviour
{
    [SerializeField] int syncroIndex;
    private IObjectEvent[] objectEvents;

    private void Awake()
    {
        this.gameObject.tag = TagManager.laserEvent;
        StartCoroutine(Syncro());
    }

    public void Event()
    {
        foreach (var eve in objectEvents)
        {
            eve.Event();
        }
    }

    IEnumerator Syncro()
    {
        yield return new WaitForSeconds(0.5f);
        this.objectEvents = SyncroOjbect.GetSyncroObjects<ObjectEventCore>(syncroIndex);
    }
}
