using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : TriggerInteractCore
{
    [SerializeField] StageDataBase nextStageBase;
    [SerializeField] bool needKey;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnTrigger(Collider collision)
    {
        if (needKey)
        {
            if (!StageManager.Instance.isGetKey)
            {
                Debug.Log("This stage need Key for go to next stage. but you don't have key!");
                return;
            }
        }
        StageManager.Instance.NextStage(this.nextStageBase);
    }
}
