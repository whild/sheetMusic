using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerªòç±ªë
/// </summary>
public class FocusPlayer : FocusCore
{
    [SerializeField] private bool isAuto = true;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override IEnumerator Focus(CinemachineTargetGroup targetGroup)
    {
        GameManager.InputEnable(false);

        yield return StartCoroutine(GameManager.Instance.SummonGetEffect(duration, targetTransform));

        GameManager.InputEnable(true);
        CinemachineController.Instance.PlayerZoom(false, isAuto);
        Destroy(this);
    }
}
