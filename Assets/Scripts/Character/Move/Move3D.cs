using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3Dの動きを担当
/// 基本的に”MoveCore”側の関数をoverrideします。
/// </summary>
public class Move3D : MoveCore
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected Transform shadow;

    [SerializeField] protected SphereCollider playeractRange;
    [SerializeField] protected Vector2 actRangeStorage = new Vector2(3, 5);

    [SerializeField] ParticleSystem walkParticle;
    [SerializeField] ParticleSystem jumpParticle;

    [SerializeField] AudioSource playeractAudio;
    [SerializeField] AudioSource transformAudio;

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
        WalkParticle();
    }

    //影の実装
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
    //動く時のイフェクトを実装
    private void WalkParticle()
    {
        if (direction == Vector3.zero || !isGround)
        {
            walkParticle.Stop();
        }
        else
        {
            if (!walkParticle.isPlaying)
            {
                walkParticle.Play();
            }
        }
    }

    public override void Jump()
    {
        base.Jump();
        if (isGround)
        {
            isGround = false;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpParticle.Play();
        }
    }

    public override void PlayerAct(bool isMike)
    {
        base.PlayerAct(isMike);

        SummonEffect(isMike);
        playeractAudio.Play();

        playeractRange.radius = isMike ? actRangeStorage.y : actRangeStorage.x;
        StartCoroutine(this.TurnOffReactRangeCollier());
    }

    /// <summary>
    /// 能力を使う時に楽器に会うイフェクトを召喚
    /// </summary>
    /// <param name="isMike"></param>
    private void SummonEffect(bool isMike)
    {
        var insturment = GameManager.Instance.GetCurrentInstrument();
        var effectobj = GameObject.Instantiate(insturment.effect, this.transform);
        var effects = effectobj.GetComponentsInChildren<ParticleSystem>();
        foreach (var item in effects)
        {
            item.startSize *= (isMike) ? insturment.mikeEffectSize : 1;
        }
        Destroy(effectobj, effects[0].startLifetime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.gameObject.TryGetComponent(out IPlayerReact react))
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

    //楽器のイフェクトをプレイ
    public override void PlayInstrumentParticle()
    {
        var particles = this.gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (var item in particles)
        {
            item.Play();
        }
        transformAudio.Play();
    }

    public override void SetPlayerActAudio()
    {
        playeractAudio.clip = GameManager.Instance.GetCurrentInstrument().actAudio;
    }


    protected override void Drop()
    {
        rigid.velocity = new Vector3(0, -10, 0);
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
