using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの能力で浄化できるオブジェクト
/// </summary>
public class React_Purification : PlayerReactCore
{
    [SerializeField] public int syncroIndex;

    private int instrumentIndex = 1;
    [SerializeField] private List<PurificationObject> targets = new List<PurificationObject>();

    private bool isPlayeract;

    protected override void Awake()
    {
        isPlayeract = false;
        base.Awake();
        StartCoroutine(SyncroObjects());
    }


    protected override void Setup2D()
    {

    }

    protected override void Setup3D()
    {

    }

    public override void PlayerReact(int instrumentValue)
    {
        if (isPlayeract)
        {
            if (instrumentIndex == instrumentValue)
            {
                foreach (var item in targets)
                {
                    item.Purification();
                }
            }
        }
    }

    IEnumerator SyncroObjects()
    {
        yield return new WaitForSeconds(0.5f);
        this.targets = SyncroOjbect.GetSyncroObjects<PurificationObject>().ToList();
        targets.RemoveAll(x => x.SyncroIndex != syncroIndex);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == other.GetComponent<SphereCollider>())
        {
            isPlayeract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == other.GetComponent<SphereCollider>())
        {
            isPlayeract = false;
        }
    }
}
