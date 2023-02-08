
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    [SerializeField] Dimension dimension;
    [SerializeField] Rigidbody rigid;
    [SerializeField] Rigidbody2D rigid2d;
    [SerializeField] int mass;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        switch (dimension)
        {
            case Dimension._2D:
                rigid = GameManager.CheckNull<Rigidbody>(this.transform);
                RigidInit(rigid);
                break;
            case Dimension._3D:
                rigid2d = GameManager.CheckNull<Rigidbody2D>(this.transform);
                RigidInit(rigid2d);
                break;
            default:
                break;
        }
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
