using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの操作を担当します。
/// </summary>
public class CinemachineController : Manager<CinemachineController>
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtual;
    [SerializeField] CinemachineVirtualCamera automatonCamera;
    [SerializeField] CinemachineVirtualCamera paperCamera;
    private CinemachineTargetGroup cinemachineTargetGroup;

    protected override void Awake()
    {
        base.Awake();
        cinemachineTargetGroup = cinemachineVirtual.Follow.GetComponent<CinemachineTargetGroup>();
    }
    public void MoveCameraTo(bool player3d)
    {
        cinemachineTargetGroup.m_Targets[0].target = (player3d) ? GameManager.Instance.player3D : GameManager.Instance.player2D;
    }

    public void CameraZoom(int pov, Transform lookat)
    {
        cinemachineVirtual.m_Lens.FieldOfView = pov;
        cinemachineVirtual.LookAt = lookat;
        cinemachineVirtual.transform.rotation = Quaternion.Euler(30, 0, 0);
    }
    public void PlayerZoom(bool isFocus, bool isAutomaton)
    {
        var target = isAutomaton ? automatonCamera : paperCamera;
        target.Priority = (isFocus) ? 100 : 5; ;
    }
}
