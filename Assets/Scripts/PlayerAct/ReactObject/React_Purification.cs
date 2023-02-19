using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React_Purification : PlayerReactCore
{
    [SerializeField] public int syncroIndex;

    private int instrumentIndex = 1;
    [SerializeField] private List<PurificationObject> targets = new List<PurificationObject>();

    protected override void Awake()
    {
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
        if (instrumentIndex == instrumentValue)
        {
            foreach (var item in targets)
            {
                item.Purification();
            }
        }
    }

    IEnumerator SyncroObjects()
    {
        yield return new WaitForSeconds(0.5f);
        this.targets = SyncroOjbect.GetSyncroObjects<PurificationObject>().ToList();
        targets.RemoveAll(x => x.SyncroIndex != syncroIndex);
    }
}
