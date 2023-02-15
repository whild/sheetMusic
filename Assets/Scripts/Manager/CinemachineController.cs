using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController : Manager<CinemachineController>
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtual;
    [SerializeField] CinemachineVirtualCamera AutomatonCamera;
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
    public void CameraZoom(int pov)
    {
        cinemachineVirtual.m_Lens.FieldOfView = pov;
        cinemachineVirtual.LookAt = CameraMagnetTargetController.Instance.targetGroup.Transform;
        cinemachineVirtual.transform.rotation = Quaternion.Euler(30, 0, 0);
    }

    public void PlayerZoom(bool isFocus)
    {
        AutomatonCamera.Priority = (isFocus) ? 100 : 5;
    }
}
