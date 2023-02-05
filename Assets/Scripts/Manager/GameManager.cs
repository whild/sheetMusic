using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtual;
    private CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] public Transform player3D;
    [SerializeField] public Transform player2D;
    public static GameObject _3Dplayer;
    public static GameObject _2Dplayer;

    protected override void Awake()
    {
        base.Awake();
        _3Dplayer = player3D.gameObject;
        _2Dplayer = player2D.gameObject;

        cinemachineTargetGroup = cinemachineVirtual.Follow.GetComponent<CinemachineTargetGroup>();
        //TryGetComponent(out mainCamera);
        //DontDestroyOnLoad(this.gameObject);
    }

    public void MoveCameraTo(bool player3d)
    {
        cinemachineTargetGroup.m_Targets[0].target = (player3d) ? player3D : player2D;
    }

    private void DestroyEventSystem()
    {
        var eventsystems = GameObject.FindObjectsOfType<EventSystem>();
        Destroy(eventsystems[0].gameObject);
    }

    public static T CheckNull<T>(Transform from) where T : Component
    {
        Transform trans = from;
        if (trans != null)
        {
            T type = trans.GetComponent<T>();
            if (type != null)
            {
                return type;
            }
            else
            {
                trans.gameObject.AddComponent<T>();
                Debug.Log($"There is Not Component So I Added Component for you");
                return type;
            }
        }
        return null;
    }
}

public class TagManager 
{
    public static string player = "Player";
    public static string ground = "Ground";
    public static string ladder = "Ladder";



    public static bool IsRightTag(string[] targetTags, string targetTag)
    {
        string tag = Array.Find(targetTags, tag => tag == targetTag);
        if (tag != null)
        {
            return true;
        }
        return false;
    }
}

public enum Dimension
{
    _2D,
    _3D
}