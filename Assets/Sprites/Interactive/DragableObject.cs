
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    [SerializeField] Dimension dimension;
    [SerializeField] Rigidbody rigid;
    [SerializeField] Rigidbody2D rigid2d;

    private void Awake()
    {
        if(!TryGetComponent(out rigid))
        {
            rigid = this.gameObject.AddComponent<Rigidbody>();
            init(rigid);
            return;
        }
        if(!TryGetComponent(out rigid))
        {
            rigid2d = this.gameObject.AddComponent<Rigidbody2D>();
            init(rigid2d);
            return;
        }
    }

    private void init(Rigidbody rigid)
    {
        rigid.constraints = RigidbodyConstraints.FreezePositionY;
        rigid.constraints = RigidbodyConstraints.FreezeRotationX;
        rigid.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    private void init(Rigidbody2D rigid)
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
