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

    protected virtual void Awake()
    {
        isLadder = false;
    }

    private void FixedUpdate()
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
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public virtual void Jump() { }

    public void Dash(float val)
    {
        this.speed = val;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ground))
        {
            isGround = true;
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
            isGround = true;
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

    private void OnTriggerEnter(Collider other)
    {
        var interactContianer = other.GetComponent<IInteract>();
        if (interactContianer != null)
        {
            PlayerInputController.Instance.SetInteract(interactContianer);
        }
        var PlayerReactContianer = other.GetComponent<IPlayerReact>();
        if(PlayerReactContianer != null)
        {
            PlayerInputController.Instance.SetPlayerReact(PlayerReactContianer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactContianer = other.GetComponent<IInteract>();
        if (PlayerInputController.Instance.AlreadyHaveInteract(interactContianer))
        {
            PlayerInputController.Instance.SetInteract(null);
        }

        var PlayerReactContianer = other.GetComponent<IPlayerReact>();
        if (PlayerInputController.Instance.playerReact.Contains(PlayerReactContianer))
        {
            PlayerInputController.Instance.playerReact.Remove(PlayerReactContianer);
        }
    }

    public virtual void PlayerAct()
    {

    }

    protected virtual void SetLadderMove(bool userGravity)
    {
        direction.y = direction.z;
        direction.z = 0;
        isGround = false;
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

public class GameUtiles 
{ 

}
