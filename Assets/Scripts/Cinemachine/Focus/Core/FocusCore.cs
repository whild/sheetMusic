using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCore : MonoBehaviour, IFocusable
{
    [SerializeField] protected Transform targetTransform;
    [SerializeField] protected float duration;
    [SerializeField] protected bool isOneTime;
    [SerializeField] protected bool isTrigger = true;

    [Range(0.1f, 50.0f)]
    public float MagnetStrength = 5.0f;

    [Range(0.1f, 50.0f)]
    public float Proximity = 5.0f;

    protected SphereCollider sphereCollider;
    protected CircleCollider2D circleCollider2D;

    protected virtual void Awake()
    {
        ColliderInit();
    }

    protected void ColliderInit()
    {
        if (isTrigger)
        {
            GameManager.CheckDemansionComponent(this.transform, ref sphereCollider, ref circleCollider2D);

            if (sphereCollider != null)
            {
                sphereCollider.radius = Proximity * 2f;
                sphereCollider.isTrigger = true;
                return;
            }
            if (circleCollider2D != null)
            {
                circleCollider2D.radius = Proximity * 2f;
                circleCollider2D.isTrigger = true;
                return;
            }
        }
        if(targetTransform == null)
        {
            targetTransform = this.transform;
        }
    }

    public virtual void FocusEffect(CinemachineTargetGroup group)
    {

    }
    public virtual void FocusEffect()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isOneTime && isTrigger)
        {
            FocusEffect(CameraMagnetTargetController.Instance.targetGroup);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isOneTime && isTrigger)
        {
            if (targetTransform != null)
            {
                CameraMagnetTargetController.Instance.DeleteTargetGruop(targetTransform);
            }
        }
    }

}
