using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPlayer : FocusCore
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void FocusEffect(CinemachineTargetGroup group)
    {
        CinemachineController.Instance.PlayerZoom(true);

        StartCoroutine(WaitEndFocus());
    }

    IEnumerator WaitEndFocus()
    {
        yield return new WaitForSeconds(duration);

        CinemachineController.Instance.PlayerZoom(false);
        Destroy(this);
    }
}
