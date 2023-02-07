using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpring : ContactInteractCore
{
    [SerializeField] private float jumpPower;
    [SerializeField] Collider collider;
    [SerializeField] Collider2D collider2d;

    private void Awake()
    {
        if(targetDimension == Dimension._3D && !TryGetComponent<Collider>(out collider))
        {
            collider = this.gameObject.AddComponent<BoxCollider>();
        }
        if(targetDimension == Dimension._2D && !TryGetComponent<Collider2D>(out collider2d))
        {
            collider2d = this.gameObject.AddComponent<BoxCollider2D>();
        }
    }

    public override void OnContact(Collision collision)
    {
        Vector3 jumpValue = MoveCore.GetDirection(contactDirection) * jumpPower;
        GameManager.Instance.player3D.GetComponent<Rigidbody>().AddForce(jumpValue, ForceMode.VelocityChange);
    }

    public override void OnContact(Collision2D collision)
    {
        Vector3 jumpValue = MoveCore.GetDirection(contactDirection) * jumpPower;
        GameManager.Instance.player2D.GetComponent<Rigidbody2D>().AddForce(jumpValue, ForceMode2D.Impulse);
    }
}
