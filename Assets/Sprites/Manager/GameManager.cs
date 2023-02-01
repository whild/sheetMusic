using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Manager<GameManager>
{
    [SerializeField] Camera mainCamera;

    [SerializeField] public Transform player3D;
    [SerializeField] public Transform player2D;
    public static GameObject _3Dplayer;
    public static GameObject _2Dplayer;

    private void Awake()
    {
        base.Awake();
        _3Dplayer = player3D.gameObject;
        _2Dplayer = player2D.gameObject;
        //TryGetComponent(out mainCamera);
        //DontDestroyOnLoad(this.gameObject);
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

    private void DestroyEventSystem()
    {
        var eventsystems = GameObject.FindObjectsOfType<EventSystem>();
        Destroy(eventsystems[0].gameObject);
    }


}

public class TagManager 
{
    public static string player = "Player";
}
