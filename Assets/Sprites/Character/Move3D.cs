using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MoveCore
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected Collider collider;
    public void Awake()
    {
        GameManager.Instance.player3D = this.transform;
        TryGetComponent(out rigid);
        TryGetComponent(out collider);
    }

    private void FixedUpdate()
    {
        if(direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public override void Jump()
    {
        if (isGround)
        {
            isGround = false;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
}
