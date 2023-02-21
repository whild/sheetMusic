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

    private void Start()
    {
        corruptedObject.SetActive(true);
        cleanObject.SetActive(false);
    }

    public void Purification()
    {
        corruptedObject.SetActive(false);
        cleanObject.SetActive(true);
        var effect = GameObject.Instantiate(ResourceData<GameObject>.GetData("Effect/Purification"), cleanObject.transform);
        Destroy(effect, effect.GetComponent<ParticleSystem>().startLifetime);
        cleanEvent.Invoke();
    }
}
