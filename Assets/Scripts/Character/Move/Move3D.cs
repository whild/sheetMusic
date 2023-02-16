using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MoveCore
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected Transform shadow;

    [SerializeField] protected SphereCollider playeractRange;
    [SerializeField] protected Vector2 actRangeStorage = new Vector2(3, 5);
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.player3D = this.transform;
        isLadder = false;
        TryGetComponent(out rigid);
        TryGetComponent(out playeractRange);
        playeractRange.enabled = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        SetShadow();
    }

    public override void Jump()
    {
        base.Jump();
        if (isGround)
        {
            isGround = false;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    public override void PlayerAct()
    {
        base.PlayerAct();
        playeractRange.radius = actRangeStorage.x;
        StartCoroutine(this.TurnOffReactRangeCollier());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        var react = other.gameObject.GetComponent<IPlayerReact>();
        if(react != null)
        {
            react.PlayerReact((int)PlayerInputController.Instance.currentInstrument.Value);
        }
    }

    protected override void SetLadderMove(bool useGravity)
    {
        base.SetLadderMove(useGravity);
        rigid.useGravity = useGravity;

        rigid.velocity = Vector3.zero;

        if (useGravity)
        {
            rigid.AddForce(Vector3.up * 3, ForceMode.Impulse);
            direction.z = direction.y;
            direction.y = 0;
        }
    }

    private void SetShadow()
    {
        float distance = float.MaxValue;
        var hits = Physics.RaycastAll(this.transform.position, Vector3.down, 16);
        foreach (var item in hits)
        {
            if (item.collider.CompareTag(TagManager.wall) || item.collider.CompareTag(TagManager.ground))
            {
                Vector3 itempoint = item.point;
                float dis = Vector3.Distance(itempoint, transform.position);
                if (dis < distance)
                {
                    distance = dis;
                    itempoint.y += 0.1f;
                    shadow.position = itempoint;
                }
            }
        }
    }

    IEnumerator TurnOffReactRangeCollier()
    {
        playeractRange.enabled = true;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        playeractRange.enabled = false;
    }
}
