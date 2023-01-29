using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] IMoveable _move;

    [SerializeField] GameObject player_3d;
    [SerializeField] GameObject player_2d;
    [SerializeField] IMoveable move3d;
    [SerializeField] IMoveable move2d;

    [SerializeField] bool test;


    private void Awake()
    {
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
    }


    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
        _input.actions["Jump"].started -= OnJump;
        _input.actions["Dash"].performed -= OnDash;
        _input.actions["Dash"].canceled -= OnDashStop;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        Debug.Log("Move");
        var value = obj.ReadValue<Vector2>();
        var direction = new Vector3(value.x, 0, value.y);
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
        test = _move.Equals(move3d);
        GameManager.Instance.MoveCameraTo(_move.Equals(move3d));
        //MoveCamera
    }
}
