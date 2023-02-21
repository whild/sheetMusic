using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoint : MonoBehaviour, IPlayerReact
{
    public int syncroIndex;
    private List<React_Break> reacts = new List<React_Break>();

    private bool isPlayeract;

    private void Start()
    {
        isPlayeract = false;
        reacts = SyncroOjbect.GetSyncroObjects<React_Break>().ToList();
    }

    public void PlayerReact(int instrumentValue)
    {
        if (isPlayeract)
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

    private void OnTriggerEnter(Collider other)
    {
        if(other == other.GetComponent<SphereCollider>())
        {
            isPlayeract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == other.GetComponent<SphereCollider>())
        {
            isPlayeract = false;
        }
    }

}
