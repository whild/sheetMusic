using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React_Knockback : PlayerReactCore
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] int value;
    [SerializeField] int mass;

    private int instrumentIndex = 2;
    protected override void Awake()
    {
        base.Awake();
        if (!TryGetComponent(out rigid))
        {
            rigid = this.gameObject.AddComponent<Rigidbody>();
        }
        rigid.mass = mass;
        StopMove(true);
    }

    public override void PlayerReact(int instrumentValue)
    {
        if (instrumentIndex == instrumentValue)
        {
            StopMove(false);

            var direction = this.transform.position - GameManager.Instance.player3D.position;
            rigid.AddForce(direction.normalized * value);

            StartCoroutine(WaitStop());
        }
    }

    IEnumerator WaitStop()
    {
        yield return new WaitUntil(() =>
        {
            if(rigid.IsSleeping())
            {
                return true;
            }
            return false;
        });

        StopMove(true);
    }

    private void StopMove(bool val)
    {
        if (val)
        {
            rigid.constraints = (RigidbodyConstraints)10;
            //rigid.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            rigid.constraints = RigidbodyConstraints.None;
        }
    }
}
