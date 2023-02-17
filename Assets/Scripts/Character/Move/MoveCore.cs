using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCore : MonoBehaviour, IMoveable
{
    [SerializeField] public bool isGround;
    [SerializeField] public static bool isLadder = false;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float jumpPower = 8;

    public static float normalSpeed = 5;
    public static float dashSpeed = 10;
    [SerializeField] protected Vector3 direction;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AnimatorController animatorController;

    protected virtual void Awake()
    {
        isLadder = false;

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animatorController = this.gameObject.GetComponent<AnimatorController>();

    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    public virtual void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public Vector3 GetDirection()
    {
        return this.direction;
    }

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
            animatorController.Walk(false);
        }
    }

    public virtual void Jump() 
    {
        animatorController.JumpTrigger();
    }

    public void Dash(float val)
    {
        animatorController.Dash((val == normalSpeed) ? false : true);
        this.speed = val;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ground))
        {
            if (this.transform.position.y > collision.transform.position.y)
            {
                isGround = true;
                animatorController.CheckGround();
            }
        }
        if (collision.gameObject.CompareTag(TagManager.ladder))
        {
            isLadder = true;
            SetLadderMove(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ground))
        {
            if (this.transform.position.y > collision.transform.position.y)
            {
                isGround = true;
                animatorController.CheckGround();
            }
        }
        if (collision.gameObject.CompareTag(TagManager.ladder))
        {
            isLadder = true;
            SetLadderMove(false);
        }
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

    public virtual void PlayerAct()
    {
        animatorController.PlayerAct();
    }

    protected virtual void SetLadderMove(bool userGravity)
    {
        direction.y = direction.z;
        direction.z = 0;
        isGround = false;
    }
    
    public virtual void InstrumentParticle()
    {

    }

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