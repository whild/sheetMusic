using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCore : MonoBehaviour, IFocusable
{
    [SerializeField] protected Transform targetTransform;
    [SerializeField] protected float duration;
    [SerializeField] protected bool isOneTime;

    [Range(0.1f, 50.0f)]
    public float MagnetStrength = 5.0f;

    [Range(0.1f, 50.0f)]
    public float Proximity = 5.0f;
    protected virtual void Awake()
    {
        var container = this.gameObject.GetComponents<Collider>();
        foreach (var item in container)
        {
            Destroy(item);
        }
        var sphere = this.gameObject.AddComponent<SphereCollider>();
        sphere.radius = Proximity * 2f;
        sphere.isTrigger = true;
    }

    public virtual void FocusEffect(CinemachineTargetGroup group)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isOneTime)
        {
            FocusEffect(CameraMagnetTargetController.Instance.targetGroup);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isOneTime)
        {
            CameraMagnetTargetController.Instance.DeleteTargetGruop(targetTransform);
        }
    }
}
