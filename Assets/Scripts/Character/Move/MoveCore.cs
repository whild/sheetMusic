using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの基本的な動き
/// </summary>
public class MoveCore : MonoBehaviour, IMoveable
{
    [SerializeField] public bool isGround;
    [SerializeField] public static bool isLadder = false;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float jumpPower = 8;

    public static float normalSpeed = 5;
    public static float dashSpeed = 8;
    [SerializeField] protected Vector3 direction;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AnimatorController animatorController;

    [Header("*Sound")]
    [SerializeField] AudioSource playerMoveAudio;
    [SerializeField] AudioSource playerJumpAudio;
    [SerializeField] AudioClip walkClip;
    [SerializeField] AudioClip dashClip;

    protected virtual void Awake()
    {
        isLadder = false;

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animatorController = this.gameObject.GetComponent<AnimatorController>();
    }

    protected virtual void FixedUpdate()
    {
        //動きだけ毎プレーム呼び出す。
        Move();
    }

    public virtual void SetDirection(Vector3 direction)//IMoveable
    {
        this.direction = direction;
        playerMoveAudio.mute = (direction != Vector3.zero) ? false : true;
    }

    public Vector3 GetDirection()//IMoveable
    {
        return this.direction;
    }
    /// <summary>
    /// 動きの実装
    /// </summary>
    public virtual void Move()
    {
        if (direction != Vector3.zero)
        {
            spriteRenderer.flipX = (direction.x < 0) ? true : false;
            animatorController.Walk(true);
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            playerMoveAudio.Play();
            animatorController.Walk(false);
        }
    }

    public virtual void Jump() //IMoveable
    {
        animatorController.JumpTrigger();
        AudioManager.PlayAudio(playerJumpAudio);
    }

    public void Dash(float val) //IMoveable
    {
        animatorController.Dash((val == normalSpeed) ? false : true);
        playerMoveAudio.clip = (val == normalSpeed) ? walkClip : dashClip;
        playerMoveAudio.Play();
        this.speed = val;
    }

    #region 衝突判定
    //2024.04.11 もっといい方があると思います。
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ground))
        {
            if (this.transform.position.y > collision.transform.position.y)
            {
                isGround = true;
                animatorController.CheckGround();
                return;
            }
            Drop();
            return;
        }
        if (collision.gameObject.CompareTag(TagManager.ladder))
        {
            isLadder = true;
            SetLadderMove(false);
            return;
        }
        Drop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ground))
        {
            if (this.transform.position.y > collision.transform.position.y)
            {
                isGround = true;
                animatorController.CheckGround();
                return;
            }
            Drop();
            return;
        }
        if (collision.gameObject.CompareTag(TagManager.ladder))
        {
            isLadder = true;
            SetLadderMove(false);
            return;
        }
        Drop();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ladder))
        {
            isLadder = false;
            isGround = true;
            SetLadderMove(true);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ladder))
        {
            isLadder = false;
            isGround = true;
            SetLadderMove(true);
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        var interactContianer = other.GetComponent<IInteract>();
        if (interactContianer != null)
        {
            PlayerInputController.Instance.SetInteract(interactContianer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactContianer = other.GetComponent<IInteract>();
        if (PlayerInputController.Instance.AlreadyHaveInteract(interactContianer))
        {
            PlayerInputController.Instance.SetInteract(null);
        }
    }
    #endregion

    public virtual void PlayerAct(bool isMike)//IMoveable
    {
        animatorController.PlayerAct();
    }

    #region 2Dと3Dで実装する機能
    protected virtual void SetLadderMove(bool userGravity)
    {
        direction.y = direction.z;
        direction.z = 0;
        isGround = false;
    }

    public virtual void PlayInstrumentParticle() { }

    public virtual void SetPlayerActAudio() { }

    protected virtual void Drop() { }
    #endregion

    public static Vector3 GetDirection(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.forward:
                return Vector3.forward;
            case MoveDirection.back:
                return Vector3.back;
            case MoveDirection.up:
                return Vector3.up;
            case MoveDirection.down:
                return Vector3.down;
            case MoveDirection.right:
                return Vector3.right;
            case MoveDirection.left:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }

}