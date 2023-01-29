using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MoveCore
{
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected Collider2D collider;
    public void Awake()
    {
        GameManager.Instance.player2D = this.transform;
        TryGetComponent(out rigid);
        TryGetComponent(out collider);
    }
    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public override void SetDirection(Vector3 direction)
    {
        base.SetDirection(direction);
        this.direction.z = 0;
    }
    public override void Jump()
    {
        if (isGround)
        {
            isGround = false;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
