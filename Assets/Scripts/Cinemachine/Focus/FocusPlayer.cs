using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPlayer : FocusCore
{
    [SerializeField] private bool isAuto = true;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void FocusEffect(CinemachineTargetGroup group)
    {
        CinemachineController.Instance.PlayerZoom(true, isAuto);
        this.gameObject.transform.position = GameManager._3Dplayer.transform.position + (Vector3.up * 5);
        Destroy(transform.GetComponentInChildren<ParticleSystem>().gameObject);
        StartCoroutine(WaitEndFocus());
    }

    public override void FocusEffect()
    {
        CinemachineController.Instance.PlayerZoom(true, isAuto);
        StartCoroutine(WaitEndFocus());
    }

    IEnumerator WaitEndFocus()
    {
        GameManager.InputEnable(false);

        yield return new WaitForSeconds(duration/2);

        var effect = GameObject.Instantiate(ResourceData<GameObject>.GetData("Effect/GetEffect"), this.transform);

        yield return new WaitForSeconds(duration/2);

        GameManager.InputEnable(true);
        CinemachineController.Instance.PlayerZoom(false, isAuto);
        Destroy(this.gameObject);
    }
}
