using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] CharacterController _characterController;

    [SerializeField] IMoveable _move;

    private void Awake()
    {
        TryGetComponent(out _input);
        TryGetComponent(out _move);
    }

    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMoveStop;
        _input.actions["Jump"].started += OnJump;
        _input.actions["Dash"].performed += OnDash;
        _input.actions["Dash"].canceled += OnDashStop;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
        _input.actions["Jump"].started -= OnJump;
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
        Debug.Log("Jump");
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
}
