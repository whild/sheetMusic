using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : TriggerInteractCore
{
    [SerializeField] StageDataBase nextStageBase;
    [SerializeField] bool needKey;

    public static bool goal3D;
    public static bool goal2D;
    public static bool haveKey;

    protected override void Awake()
    {
        base.Awake();
    }

    public static void ReData(bool d3, bool d2, bool key)
    {
        goal3D = !d3;
        goal2D = !d2;
        haveKey = !key;
    }

    public override void OnTrigger(Collider collision)
    {
        CheckKey();
        if (collision.CompareTag(TagManager.player))
        {
            goal3D = true;
        }
        CheckNextStage();
    }

    public override void OnTrigger(Collider2D collision)
    {
        CheckKey();
        if (collision.CompareTag(TagManager.player))
        {
            goal2D = true;
        }
        CheckNextStage();
    }

    private void CheckKey()
    {
        if (needKey)
        {
            if (!StageManager.Instance.isGetKey)
            {
                Debug.Log("This stage need Key for go to next stage. but you don't have key!");
                return;
            }
            haveKey = true;
        }
        CheckNextStage();
    }

    private void CheckNextStage()
    {
        if (goal3D && goal2D && haveKey)
        {
            StageManager.Instance.NextStage(this.nextStageBase);
        }
    }
}
