using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÐñÜâîÜªÊÞÅª¤Û°
/// </summary>
public class FocusExample : FocusCore
{
    protected override void Awake()
    {//there is base init
        base.Awake();
    }

    protected override IEnumerator Focus(CinemachineTargetGroup targetGroup)
    {//write focus effect here!
        yield return null;
    }
}
