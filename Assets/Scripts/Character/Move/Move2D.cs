using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MoveCore
{
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected Collider2D collider_;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.player2D = this.transform;
        TryGetComponent(out rigid);
        TryGetComponent(out collider_);
    }
    public override void SetDirection(Vector3 direction)
    {
        base.SetDirection(direction);
        this.direction.z = 0;
    }
    public override void Jump()
    {
        base.Jump();
        if (isGround)
        {
            isGround = false;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    /*
    public override void PlayerAct(bool isMike)
    {

    }
    */
    protected override void SetLadderMove(bool useGravity)
    {
        base.SetLadderMove(useGravity);
        rigid.gravityScale = (useGravity) ? 1 : 0;
        rigid.velocity = Vector3.zero;
    }
}
