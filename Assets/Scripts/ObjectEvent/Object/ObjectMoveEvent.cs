using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class ObjectMoveEvent : ObjectEventCore
{
    [SerializeField] FloatReactiveProperty currentValue = new FloatReactiveProperty();

    [SerializeField] MoveDirection direction;
    [SerializeField] float endValue;
    [SerializeField] float duration;

    private Vector3 startPos;
    private Vector3 endPos;

    private void Awake()
    {
        startPos = this.transform.position;
        endPos = startPos + (MoveCore.GetDirection(direction) * endValue);
        currentValue
            .Subscribe(val =>
            {
                this.transform.position = startPos + ((val * endValue) * MoveCore.GetDirection(direction));
            });
    }

    public override void Event(float val)
    {
        currentValue.Value = val;
    }

    public override void Event()
    {
        this.transform
            .DOMove(endPos, duration)
            .SetEase(Ease.Linear);
    }

}
