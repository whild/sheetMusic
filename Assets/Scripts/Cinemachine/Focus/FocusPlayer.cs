using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPlayer : FocusCore
{
    [SerializeField] private bool isAuto = true;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void FocusEffect(CinemachineTargetGroup group)
    {
        CinemachineController.Instance.PlayerZoom(true, isAuto);
        StartCoroutine(WaitEndFocus());
    }

    public override void FocusEffect()
    {
        CinemachineController.Instance.PlayerZoom(true, isAuto);
        StartCoroutine(WaitEndFocus());
    }

    IEnumerator WaitEndFocus()
    {
        GameManager.InputEnable(false);

        yield return StartCoroutine(GameManager.Instance.SummonGetEffect(duration, targetTransform));

        GameManager.InputEnable(true);
        CinemachineController.Instance.PlayerZoom(false, isAuto);
        Destroy(this);
    }
}
