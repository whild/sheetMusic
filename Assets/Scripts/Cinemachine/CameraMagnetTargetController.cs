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
    private List<FocusCore> focusCores = new List<FocusCore>();

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        playerIndex = 0;
    }

    public void AddTargetGruop(Transform trans)
    {
        var container = targetGroup.m_Targets.ToList();
        container.Add(container[0]);
        targetGroup.m_Targets = container.ToArray();
        targetGroup.m_Targets[targetGroup.m_Targets.Length - 1].target = trans;
    }

    public void DeleteTargetGruop(Transform trans)
    {
        var container = targetGroup.m_Targets.ToList();
        container.Remove(container.Find(val => val.target == trans));
        targetGroup.m_Targets = container.ToArray();
        targetGroup.m_Targets[playerIndex].weight = 1;
        GameManager.Instance.CameraZoom(60, null);
    }
    public void AddForcusObjects(FocusCore target)
    {
        GameManager.Instance.CameraZoom(30, targetGroup.Transform);
        this.focusCores.Add(target);
    }
    // Update is called once per frame
    void Update()
    {
        if (targetGroup.m_Targets[playerIndex].weight != 0 && focusCores.Count >= 0)
        {
            for (int i = 1; i < targetGroup.m_Targets.Length; ++i)
            {
                float distance = (targetGroup.m_Targets[playerIndex].target.position - targetGroup.m_Targets[i].target.position).magnitude;
                if (distance < focusCores[i - 1].Proximity)
                {
                    targetGroup.m_Targets[i].weight = focusCores[i - 1].MagnetStrength * (1 - (distance / focusCores[i - 1].Proximity));
                }
                else
                {
                    targetGroup.m_Targets[i].weight = 0;
                }
            }
        }
    }
}
