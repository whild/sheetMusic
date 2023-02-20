using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentObject : TriggerInteractCore
{
    [SerializeField] Instrument instrument;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnTrigger(Collider collision)
    {
        GameManager.Instance.data.instrumentData[(int)instrument] = true;
    }
}
