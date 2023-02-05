using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React_Knockback : PlayerReactCore
{
    Rigidbody rigid;
    public int value;

    private int instrumentIndex = 1;
    protected override void Awake()
    {
        base.Awake();
        rigid = this.gameObject.AddComponent<Rigidbody>();
    }

    public override void PlayerReact(int instrumentValue)
    {
        if (instrumentIndex == instrumentValue)
        {
            var direction = this.transform.position - GameManager.Instance.player3D.position;
            rigid.AddForce(direction.normalized * value);
        }
    }
}
