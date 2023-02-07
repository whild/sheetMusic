using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class PlayerInputController : Manager<PlayerInputController>
{
    [SerializeField] PlayerInput _input;
    [SerializeField] IMoveable _move;
    [SerializeField] MoveCore moveCore;

    [SerializeField] GameObject player_3d;
    [SerializeField] GameObject player_2d;
    [SerializeField] IMoveable Imove3d;
    [SerializeField] IMoveable Imove2d;

    [SerializeField] IInteract interact;
    [SerializeField] public List<IPlayerReact> playerReact = new List<IPlayerReact>();

    [SerializeField] public ReactiveProperty<instrument> currentInstrument = new ReactiveProperty<instrument>();

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out _input);

    }

    private void Start()
    {
        player_3d = GameManager.Instance.player3D.gameObject;
        player_2d = GameManager.Instance.player2D.gameObject;

        player_3d.TryGetComponent(out Imove3d);
        player_2d.TryGetComponent(out Imove2d);
        
        _move = Imove3d;
        moveCore = player_3d.GetComponent<MoveCore>();
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

        if(MoveCore.isLadder && !moveCore.isGround)
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
        Vector3 dir = _move.GetDirection();
        _move.SetDirection(Vector3.zero);
        _move = (_move.Equals(Imove3d) ? Imove2d : Imove3d);
        moveCore = (_move.Equals(Imove3d) ? player_2d.GetComponent<MoveCore>() : player_3d.GetComponent<MoveCore>());
        _move.SetDirection(dir);

        //MoveCamera
        GameManager.Instance.MoveCameraTo(_move.Equals(Imove3d)); 
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
        foreach (var reacts in playerReact)
        {
            reacts.PlayerReact((int)this.currentInstrument.Value);
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
        this.playerReact.Add(playerReact);
    }

}
