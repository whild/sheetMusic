using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] Camera mainCamera;

    [SerializeField] public Transform player3D;
    [SerializeField] public Transform player2D;

    private void Awake()
    {
        base.Awake();
        //TryGetComponent(out mainCamera);
        DontDestroyOnLoad(this.gameObject);
    }

    public void MoveCameraTo(bool player3d)
    {
        if (player3d)
        {
            mainCamera.transform.LookAt(player3D);
        }
        else
        {
            mainCamera.transform.LookAt(player2D);
        }
    }


}
