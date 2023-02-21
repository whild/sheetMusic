using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressButton : ContactInteractWithObjectCore
{
    [SerializeField] private bool isContact;
    [SerializeField] private bool contactDelay;
    [SerializeField] private bool activeOnce;
    [SerializeField] private float Y;
    [SerializeField] private float pressValue;
    /// <summary>
    /// 0일 시 즉시 발동
    /// </summary>
    [SerializeField] private float duration;
    [SerializeField] private float currentDuration;
    AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        if (!TryGetComponent(out audioSource))
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups(AudioManager.Effect)[0];
            audioSource.clip = ResourceData<AudioClip>.GetData("Sound/Field_button");
        }
        Y = this.transform.position.y;
        isContact = false;
        contactDelay = false;
    }

    public override void OnContact(Collision collision)
    {
        Press();
    }

    public override void OnUnContact(Collision collision)
    {
        Detach();
    }

    public override void OnContact(Collision2D collision)
    {
        Press();
    }


    public override void OnUnContact(Collision2D collision)
    {
        Detach();
    }

    private void Press()
    {
        this.contactDelay = true;
        StopCoroutine(WaitUnContact());
        if (isContact == false)
        {
            this.isContact = true;
            float current = Mathf.Lerp(pressValue, 0, Y - this.transform.position.y);
            float duration_ = (duration != 0) ? current * duration : 0;

            audioSource.Play();
            var down = DOTween.Sequence();
            down.Append(this.transform
                .DOMoveY(Y - pressValue, duration_)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    currentDuration = (Y - transform.position.y) / pressValue;
                    InvokeEvent(currentDuration);
                    if (!isContact)
                    {
                        down.Kill();
                    }
                })
                )
                .OnComplete(() =>
                {
                    currentDuration = 1;
                    InvokeEvent(currentDuration);
                });
        }
    }

    private void Detach()
    {
        if (activeOnce)
        {
            return;
        }
        StopCoroutine(WaitUnContact());
        StartCoroutine(WaitUnContact());
    }

    private IEnumerator WaitUnContact()
    {
        yield return new WaitForSeconds(0.1f);
        contactDelay = false;
        yield return new WaitForSeconds(0.2f);

        if (contactDelay)
        {
            yield break;
        }
        isContact = false;

        float current = Mathf.Lerp(pressValue, 0, Y - this.transform.position.y);
        float duration_ = (duration != 0) ? current * duration : 0;

        var up = DOTween.Sequence();
        up.Append(this.transform
            .DOMoveY(Y, duration_)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                currentDuration = (Y - transform.position.y) / pressValue;
                InvokeEvent(currentDuration);
                if (isContact)
                {
                    up.Kill();
                }
            })
            .OnComplete(() =>
           {
               currentDuration = 0;
               InvokeEvent(currentDuration);
           }));
    }
}
