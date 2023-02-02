using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpring : ContactIInteractCore
{
    [SerializeField] private float jumpPower;
    [SerializeField] Collider collider;

    private void Awake()
    {
        if(!TryGetComponent<Collider>(out collider))
        {
            collider = this.gameObject.AddComponent<BoxCollider>();
        }
    }

    public override void OnContact()
    {
        Vector3 jumpValue = ObjectMoveInteract.GetDirection(contactDirection) * jumpPower;
        GameManager.Instance.player3D.GetComponent<Rigidbody>().AddForce(jumpValue, ForceMode.VelocityChange);
    }
}
