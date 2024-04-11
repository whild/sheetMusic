using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触ったときに高くジャンプさせるオブジェクト
/// </summary>
public class JumpSpring : ContactInteractCore
{
    [SerializeField] private float jumpPower;
    [SerializeField] Collider collider_;
    [SerializeField] Collider2D collider2d;

    AudioSource source;

    protected override void Awake()
    {
        source = this.GetComponent<AudioSource>();
        if(targetDimension == Dimension._3D && !TryGetComponent<Collider>(out collider_))
        {
            collider_ = this.gameObject.AddComponent<BoxCollider>();
        }
        if(targetDimension == Dimension._2D && !TryGetComponent<Collider2D>(out collider2d))
        {
            collider2d = this.gameObject.AddComponent<BoxCollider2D>();
        }
    }

    public override void OnContact(Collision collision)
    {
        source.Play();
        Vector3 jumpValue = MoveCore.GetDirection(contactDirection) * jumpPower;
        GameManager.Instance.player3D.GetComponent<Rigidbody>().AddForce(jumpValue, ForceMode.VelocityChange);
    }

    public override void OnContact(Collision2D collision)
    {
        source.Play();
        Vector3 jumpValue = MoveCore.GetDirection(contactDirection) * jumpPower;
        GameManager.Instance.player2D.GetComponent<Rigidbody2D>().AddForce(jumpValue, ForceMode2D.Impulse);
    }
}
