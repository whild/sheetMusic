using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnce : FocusCore
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void FocusEffect(CinemachineTargetGroup group)
    {
        StartCoroutine(Focus(group));
    }

    private IEnumerator Focus(CinemachineTargetGroup targetGroup)
    {
        isOneTime = true;
        CameraMagnetTargetController.Instance.AddTargetGruop(targetTransform);

        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            targetGroup.m_Targets[i].weight = 0;
            if (targetGroup.m_Targets[i].target == targetTransform)
            {
                targetGroup.m_Targets[i].weight = 1;
            }
        }
        yield return new WaitForSeconds(duration);
        CameraMagnetTargetController.Instance.DeleteTargetGruop(targetTransform);
    }
}
