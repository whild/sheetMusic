using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ß¾ù»ªËÔÑª¯«ª«Ö«¸«§«¯«È
/// </summary>
public class Elevator : ContactInteractWithObjectCore
{
    [SerializeField] private int criteriaValue = 0;
    [SerializeField] private IntReactiveProperty count = new IntReactiveProperty();

    protected override void Awake()
    {
        base.Awake();
        count.Value = 0;
        count
            .Subscribe(val =>
            {
                if(val >= criteriaValue)
                {
                    InvokeEvent();
                }
            });
    }

    public override void OnContact(Collision collision)
    {
        count.Value++;
    }

    public override void OnUnContact(Collision collision)
    {
        count.Value--;
    }

}
