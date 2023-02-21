using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingEvent : MonoBehaviour
{
    public void Ending()
    {
        Transform mainCanvas = GameObject.Find("MainCanvas").transform;
        GameObject ending = Instantiate(ResourceData<GameObject>.GetData("Prefab/EndingCutscene"),mainCanvas);
    }
}
