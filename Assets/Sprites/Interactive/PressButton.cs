using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressButton : ContactIInteractCore
{
    [SerializeField] GameObject targetObj;
    [SerializeField] Collider _collider;

    private IInteract interact;
    [SerializeField] private bool isContact;
    [SerializeField] private float Y;
    [SerializeField] private float duration;
    [SerializeField] private float currentDuration;


    private void Awake()
    {
        this.interact = targetObj.GetComponent<IInteract>();
        TryGetComponent(out _collider);
        Y = this.transform.position.y;
    }

    public override void OnContact()
    {
        this.isContact = true;

        var down = DOTween.Sequence();
        down.Append(this.transform
            .DOMoveY(Y - 0.5f, this.transform.position.y - (Y - 0.5f) / duration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                currentDuration = (Y - transform.position.y) / 0.5f;
                this.interact.Interact(currentDuration);
                if (!isContact)
                {
                    down.Kill();
                }
            })
            )
            .OnComplete(() =>
            {
                currentDuration = 1;
            });
    }

    public override void OnUnContact()
    {
        this.isContact = false;

        var up = DOTween.Sequence();
        up.Append(this.transform
            .DOMoveY(Y, Y - this.transform.position.y / duration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                currentDuration = (Y - transform.position.y) / 0.5f;
                this.interact.Interact(currentDuration);
                if (isContact)
                {
                    up.Kill();
                }
            })
            .OnComplete(() =>
           {
                currentDuration = 0;
            }));
    }
}
