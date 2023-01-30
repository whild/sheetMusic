using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeInteract : ObjectInteracCore
{
    protected void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        this.transform
            .DOMoveY(this.transform.position.y + 5, 1)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
    }


}
