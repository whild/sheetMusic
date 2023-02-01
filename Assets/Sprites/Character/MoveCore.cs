using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCore : MonoBehaviour, IMoveable
{
    [SerializeField] protected bool isGround;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float jumpPower = 8;

    public static float normalSpeed = 5;
    public static float dashSpeed = 10;
    protected Vector3 direction;

    public virtual void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    public virtual void Jump() { }

    public void Dash(float val)
    {
        this.speed = val;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
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
        if (PlayerInputController.Instance.AlreadyHaveReact(PlayerReactContianer))
        {
            PlayerInputController.Instance.SetPlayerReact(PlayerReactContianer);
        }
    }

    public virtual void PlayerAct()
    {

    }
}
