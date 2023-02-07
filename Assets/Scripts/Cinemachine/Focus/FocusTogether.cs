using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTogether : FocusCore
{
    protected override void Awake()
    {
        base.Awake();
        this.targetTransform = this.transform;
    }

    public override void FocusEffect(CinemachineTargetGroup group)
    {
        CameraMagnetTargetController.Instance.AddTargetGruop(targetTransform);
        CameraMagnetTargetController.Instance.AddForcusObjects(this);
    }
}
