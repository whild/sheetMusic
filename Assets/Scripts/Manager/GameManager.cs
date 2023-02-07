using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class GameManager : Manager<GameManager>
{
    private readonly string fileName = "saveData.json";
    public SaveData data = new SaveData();

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

        SaveGameData();
        LoadGameData();
    }

    public void MoveCameraTo(bool player3d)
    {
        cinemachineTargetGroup.m_Targets[0].target = (player3d) ? player3D : player2D;
    }

    public void CameraZoom(int pov, Transform lookat)
    {
        cinemachineVirtual.m_Lens.FieldOfView = pov;
        cinemachineVirtual.LookAt = lookat;
        cinemachineVirtual.transform.rotation = Quaternion.Euler(30, 0, 0);
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
                type = trans.gameObject.AddComponent<T>();
                Debug.Log($"There is Not Component So I Added Component for you");
                return type;
            }
        }
        return null;
    }

    public static T AddColliderTriger<T>(GameObject gameObject) where T : Collider
    {
        var col = gameObject.AddComponent<T>();
        col.isTrigger = true;
        return col;
    }
    
    public static T AddColliderTriger2D<T>(GameObject gameObject) where T : Collider2D
    {
        var col = gameObject.AddComponent<T>();
        col.isTrigger = true;
        return col;
    }



    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SaveData>(FromJsonData);
            print("�ҷ����� �Ϸ�");
        }
    }


    // �����ϱ�
    public void SaveGameData()
    {
        // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + fileName;

        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
        File.WriteAllText(filePath, ToJsonData);

        // �ùٸ��� ����ƴ��� Ȯ�� (�����Ӱ� ����)
        Debug.Log(ToJsonData);
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
public enum MoveDirection
{
    forward,
    back,
    up,
    down,
    right,
    left,
}