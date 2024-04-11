using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラが照らす時の基本的な内容実装
/// </summary>
public abstract class FocusCore : MonoBehaviour, IFocusable
{
    [SerializeField] protected Transform targetTransform;
    /// <summary>
    /// 照らす時間
    /// </summary>
    [SerializeField] protected float duration;
    /// <summary>
    /// 一回照らすのか
    /// </summary>
    [SerializeField] protected bool isOneTime;
    [SerializeField] protected bool isTrigger = true;

    [Range(0.1f, 50.0f)]
    public float MagnetStrength = 5.0f;

    [Range(0.1f, 50.0f)]
    public float Proximity = 5.0f;

    //球体のColliderを使って実装
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
        if (targetTransform == null)
        {
            targetTransform = this.transform;
        }
    }

    public void FocusEffect(CinemachineTargetGroup group = null)//IFocusable
    {
        group = (group == null) ? group : CameraMagnetTargetController.Instance.targetGroup;
        this.StartCoroutine(Focus(group));
    }

    /// <summary>
    /// Corotineで、映す効果を書く
    /// </summary>
    /// <param name="targetGroup"></param>
    /// <returns></returns>
    protected abstract IEnumerator Focus(CinemachineTargetGroup targetGroup);

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
