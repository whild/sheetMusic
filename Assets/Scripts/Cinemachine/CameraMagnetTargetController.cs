using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMagnetTargetController : Manager<CameraMagnetTargetController>
{
    public CinemachineTargetGroup targetGroup;

    private int playerIndex;
    private FocusObject[] cameraMagnets = new FocusObject[] { };
    // Start is called before the first frame update
    void Start()
    {
        playerIndex = 0;
    }

    public void AddTargetGruop(Transform trans, FocusType focusType)
    {
        Array.Resize(ref targetGroup.m_Targets, targetGroup.m_Targets.Length + 1);
        targetGroup.m_Targets[targetGroup.m_Targets.Length - 1].target = trans;

        FocusEffect(trans, focusType);

    }

    public void DeleteTargetGruop(Transform trans)
    {
        var container = targetGroup.m_Targets.ToList();
        container.Remove(container.Find(val => val.target == trans));
        targetGroup.m_Targets = container.ToArray();
        targetGroup.m_Targets[playerIndex].weight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetGroup.m_Targets[playerIndex].weight != 0 && cameraMagnets.Length >= 0)
        {
            for (int i = 1; i < targetGroup.m_Targets.Length; ++i)
            {
                float distance = (targetGroup.m_Targets[playerIndex].target.position - targetGroup.m_Targets[i].target.position).magnitude;
                if (distance < cameraMagnets[i - 1].Proximity)
                {
                    targetGroup.m_Targets[i].weight = cameraMagnets[i - 1].MagnetStrength * (1 - (distance / cameraMagnets[i - 1].Proximity));
                }
                else
                {
                    targetGroup.m_Targets[i].weight = 0;
                }
            }
        }
    }

    private void FocusEffect(Transform target, FocusType focusType)
    {
        switch (focusType)
        {
            case FocusType.Together:
                Together(target);
                break;
            case FocusType.Once:
                Focus(target);
                break;
            default:
                break;
        }
    }

    private void Together(Transform target)
    {
        var container = (cameraMagnets.Length != 0) ? cameraMagnets.ToList() : new List<FocusObject>();
        container.Add(target.GetComponent<FocusObject>());
        this.cameraMagnets = container.ToArray();
    }

    private void Focus(Transform target)
    {
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            targetGroup.m_Targets[i].weight = 0;
            if(targetGroup.m_Targets[i].target == target)
            {
                targetGroup.m_Targets[i].weight = 1;
            }
        }
    }
}
