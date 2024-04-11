using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームをクリアした時にの確認
/// </summary>
public class EndingEvent : MonoBehaviour
{
    public void Ending()
    {
        Transform mainCanvas = GameObject.Find("MainCanvas").transform;
        GameObject ending = Instantiate(ResourceData<GameObject>.GetData("Prefab/EndingCutscene"),mainCanvas);
    }
}
