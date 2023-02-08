using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React_Knockback : PlayerReactCore
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] int value;
    [SerializeField] int mass;

    private int instrumentIndex = 1;
    protected override void Awake()
    {
        base.Awake();
        if (!TryGetComponent(out rigid))
        {
            rigid = this.gameObject.AddComponent<Rigidbody>();
            rigid.mass = mass;
            rigid.isKinematic = true;
        }
    }

    public override void PlayerReact(int instrumentValue)
    {
        if (instrumentIndex == instrumentValue)
        {
            rigid.isKinematic = false;

            var direction = this.transform.position - GameManager.Instance.player3D.position;
            rigid.AddForce(direction.normalized * value);

            StartCoroutine(WaitStop());
        }
    }

    IEnumerator WaitStop()
    {
        yield return new WaitUntil(() =>
        {
            if(rigid.velocity == Vector3.zero)
            {
                return true;
            }
            return false;
        });

        rigid.isKinematic = true;
    }
}
