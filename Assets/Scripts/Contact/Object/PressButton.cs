using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressButton : ContactInteractWithObjectCore
{
    [SerializeField] Collider _collider;

    [SerializeField] private bool isContact;
    [SerializeField] private bool onceTest;
    [SerializeField] private float Y;
    [SerializeField] private float pressValue;
    /// <summary>
    /// 0일 시 즉시 발동
    /// </summary>
    [SerializeField] private float duration;
    [SerializeField] private float currentDuration;


    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out _collider);
        Y = this.transform.position.y;
    }

    public override void OnContact(Collision collision)
    {
        this.isContact = true;
        this.onceTest = true;
        float current = Mathf.Lerp(pressValue,0, Y - this.transform.position.y);
        float duration_ = (duration != 0) ? current * duration : 0;

        var down = DOTween.Sequence();
        down.Append(this.transform
            .DOMoveY(Y - pressValue, duration_)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                currentDuration = (Y - transform.position.y) / pressValue;
                this.objectEvent.Event(currentDuration);
                if (!isContact)
                {
                    down.Kill();
                }
            })
            )
            .OnComplete(() =>
            {
                currentDuration = 1;
                this.objectEvent.Event(currentDuration);
            });
    }

    public override void OnUnContact(Collision collision)
    {
        this.isContact = false;
        StartCoroutine(WaitUnContact());
    }

    private IEnumerator WaitUnContact()
    {
        yield return new WaitForSeconds(0.2f);

        if (isContact)
        {
            yield break;
        }

        float current = Mathf.Lerp(pressValue, 0, Y - this.transform.position.y);
        float duration_ = (duration != 0) ? current * duration : 0;

        var up = DOTween.Sequence();
        up.Append(this.transform
            .DOMoveY(Y, duration_)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                currentDuration = (Y - transform.position.y) / pressValue;
                this.objectEvent.Event(currentDuration);
                if (isContact)
                {
                    up.Kill();
                }
            })
            .OnComplete(() =>
           {
               currentDuration = 0;
               this.objectEvent.Event(currentDuration);
           }));
    }
}
