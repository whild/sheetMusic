using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum MoveDirection
{
    forward,
    back,
    up,
    down,
    right,
    left,
}

public class ObjectMoveInteract : ObjectInteractCore
{
    [SerializeField] FloatReactiveProperty currentValue = new FloatReactiveProperty();

    [SerializeField] MoveDirection direction;
    [SerializeField] float endValue;
    private void Awake()
    {
        Vector3 init = this.transform.position;

        currentValue
            .Subscribe(val =>
            {
                this.transform.position = init + ((val * endValue) * GetDirection());
            });
    }

    public override void Interact(float val)
    {
        currentValue.Value = val;
    }

    private Vector3 GetDirection()
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
