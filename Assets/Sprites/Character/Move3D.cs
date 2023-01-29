using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MonoBehaviour, IMoveable
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] CharacterController _characterController;
    public static float normalSpeed = 5;
    public static float dashSpeed = 10;
    private float speed = 5;
    private Vector3 direction;

    private void Awake()
    {
        TryGetComponent(out rigid);
        TryGetComponent(out _characterController);
    }

    private void FixedUpdate()
    {
        if(direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            Debug.Log("Jump");
        }
    }

    public void Dash(float val)
    {
        this.speed = val;
    }
}
