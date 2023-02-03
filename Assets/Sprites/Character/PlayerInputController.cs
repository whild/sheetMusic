using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : Manager<PlayerInputController>
{
    [SerializeField] PlayerInput _input;
    [SerializeField] IMoveable _move;

    [SerializeField] GameObject player_3d;
    [SerializeField] GameObject player_2d;
    [SerializeField] IMoveable move3d;
    [SerializeField] IMoveable move2d;

    [SerializeField] IInteract interact;
    [SerializeField] IPlayerReact playerReact;

    protected void Awake()
    {
        base.Awake();
        TryGetComponent(out _input);

    }

    private void Start()
    {
        player_3d = GameManager.Instance.player3D.gameObject;
        player_2d = GameManager.Instance.player2D.gameObject;

        player_3d.TryGetComponent(out move3d);
        player_2d.TryGetComponent(out move2d);

        _move = move3d;
    }

    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMoveStop;
        _input.actions["Jump"].started += OnJump;
        _input.actions["Dash"].performed += OnDash;
        _input.actions["Dash"].canceled += OnDashStop;
        _input.actions["ChangeCharacter"].performed += OnChangeCharacter;
        _input.actions["Interact"].started += OnInteract;
        _input.actions["PlayerAct"].started += OnPlayerAct;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
        _input.actions["Jump"].started -= OnJump;
        _input.actions["Dash"].performed -= OnDash;
        _input.actions["Dash"].canceled -= OnDashStop;
        _input.actions["Interact"].started -= OnInteract;
        _input.actions["PlayerAct"].started -= OnPlayerAct;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        var direction = new Vector3(value.x, 0, value.y);

        if(MoveCore.isLadder)
        {
            direction.y = direction.z;
            direction.z = 0;
        }
        _move.SetDirection(direction);
    }
    private void OnMoveStop(InputAction.CallbackContext obj)
    {
        _move.SetDirection(Vector3.zero);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _move.Jump();
    }

    private void OnDash(InputAction.CallbackContext obj)
    {
        _move.Dash(Move3D.dashSpeed);
    }
    private void OnDashStop(InputAction.CallbackContext obj)
    {
        _move.Dash(Move3D.normalSpeed);
    }
    private void OnChangeCharacter(InputAction.CallbackContext obj)
    {
        _move = (_move.Equals(move3d) ? move2d : move3d);
        GameManager.Instance.MoveCameraTo(_move.Equals(move3d));
        //MoveCamera
    }
    private void OnInteract(InputAction.CallbackContext obj)
    {
        if (interact != null)
        {
            interact.Interact();
            Debug.Log("Interact");
        }
    }

    private void OnPlayerAct(InputAction.CallbackContext obj)
    {
        if (this.playerReact != null)
        {
            this.playerReact.PlayerReact();
        }
    }
    public void SetInteract(IInteract interact)
    {
        this.interact = interact;
    }

    public bool AlreadyHaveInteract(IInteract interact)
    {
        if (this.interact == interact)
        {
            return true;
        }
        return false;
    }

    public void SetPlayerReact(IPlayerReact playerReact)
    {
        this.playerReact = playerReact;
    }

    public bool AlreadyHaveReact(IPlayerReact react)
    {
        if (this.playerReact == react)
        {
            return true;
        }
        return false;
    }
}
