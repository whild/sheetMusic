using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ì£ªÄªÎObjectªòç±ªë
/// </summary>
public class FocusTogether : FocusCore
{
    protected override void Awake()
    {
        base.Awake();
        this.targetTransform = this.transform;
    }

    protected override IEnumerator Focus(CinemachineTargetGroup targetGroup)
    {
        CameraMagnetTargetController.Instance.AddTargetGruop(targetTransform);
        CameraMagnetTargetController.Instance.AddForcusObjects(this);
        yield return null;
    }
}
