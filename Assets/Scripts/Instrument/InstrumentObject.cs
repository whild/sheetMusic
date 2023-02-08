using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentObject : TriggerInteractCore
{
    [SerializeField] Instrument instrument;
    public override void OnTrigger(Collider collision)
    {
        GameManager.Instance.data.instrumentData[(int)instrument] = true;
        GameManager.Instance.SaveGameData();
    }
}
