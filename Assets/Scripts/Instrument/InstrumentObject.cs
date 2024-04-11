using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 楽器オブジェクト
/// </summary>
public class InstrumentObject : TriggerInteractCore
{
    [SerializeField] float duration;
    [SerializeField] Instrument instrument;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnTrigger(Collider collision)
    {
        //楽器を得たデータをセーブ
        GameManager.Instance.data.instrumentData[(int)instrument] = true;

        this.gameObject.transform.position = GameManager._3Dplayer.transform.position + (Vector3.up * 5);
        Destroy(transform.GetComponentInChildren<ParticleSystem>().gameObject);
        StartCoroutine(WaitEnd());
    }

    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
