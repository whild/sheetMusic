using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMagnetProperty : FocusObject
{
    [Range(0.1f, 50.0f)]
    public float MagnetStrength = 5.0f;
    
    [Range(0.1f, 50.0f)]
    public float Proximity = 5.0f;

    public Transform ProximityVisualization;

    [HideInInspector] public Transform myTransform;

    void Start()
    {
        myTransform = transform;
        if (ProximityVisualization != null)
            ProximityVisualization.localScale = new Vector3(Proximity * 2.0f, Proximity * 2.0f, 1);

        SphereCollider collider = this.gameObject.AddComponent<SphereCollider>();
        collider.radius = Proximity * 2.0f;
        collider.isTrigger = true;
    }
}
