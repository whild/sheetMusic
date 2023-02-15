using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEvent : MonoBehaviour
{
    [SerializeField] GameObject targetObj;
    private IObjectEvent objectEvent;

    private void Awake()
    {
        this.gameObject.tag = TagManager.laserEvent;

    }

    public void Event()
    {
        objectEvent.Event();
    }
}
