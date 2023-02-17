using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoint : MonoBehaviour, IPlayerReact
{
    public int syncroIndex;
    private List<React_Break> reacts = new List<React_Break>();


    private void Start()
    {
        reacts = SyncroOjbect.GetSyncroObjects<React_Break>().ToList();
    }

    public void PlayerReact(int instrumentValue)
    {
        foreach (var react in reacts)
        {
            if (react.syncroIndex == this.syncroIndex)
            {
                react.PlayerReact(instrumentValue);
            }
        }
    }
}
