using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RigidbodyªÇÔÑª¯«ª«Ö«¸«§«¯«È
/// </summary>
public class DragableObject : MonoBehaviour
{
    [SerializeField] Dimension dimension;
    [SerializeField] Rigidbody rigid;
    [SerializeField] Rigidbody2D rigid2d;
    [SerializeField] int mass;

    private void Awake()
    {
        GameManager.CheckDemansionComponent(this.transform, ref rigid, ref rigid2d);
    }


    private void RigidInit(Rigidbody rigid)
    {
        rigid.mass = mass;
        rigid.constraints = RigidbodyConstraints.FreezePositionY;
        rigid.constraints = RigidbodyConstraints.FreezeRotationX;
        rigid.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    private void RigidInit(Rigidbody2D rigid)
    {
        rigid.mass = mass;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
