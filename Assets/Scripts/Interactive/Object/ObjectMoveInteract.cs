using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 反応して動くオブジェクト
/// </summary>
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
                this.transform.position = init + ((val * endValue) * MoveCore.GetDirection(direction));
            });
    }

    public override void Interact(float val)
    {
        currentValue.Value = val;
    }

}
