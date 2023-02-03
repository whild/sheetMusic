using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MoveCore
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected Collider collider;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.player3D = this.transform;
        isLadder = false;
        TryGetComponent(out rigid);
        TryGetComponent(out collider);
    }

    public override void Jump()
    {
        if (isGround)
        {
            isGround = false;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    public override void PlayerAct()
    {

    }

    protected override void SetLadderMove(bool useGravity)
    {
        base.SetLadderMove(useGravity);
        rigid.useGravity = useGravity;
        rigid.velocity = Vector3.zero;
    }

}
