using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class GameManager : Manager<GameManager>
{
    public static readonly string fileName = "saveData.json";
    public SaveData data = new SaveData();

    [SerializeField] public Transform player3D;
    [SerializeField] public Transform player2D;
    public static GameObject _3Dplayer;
    public static GameObject _2Dplayer;

    protected override void Awake()
    {
        base.Awake();
        _3Dplayer = player3D.gameObject;
        _2Dplayer = player2D.gameObject;

        //TryGetComponent(out mainCamera);
        //DontDestroyOnLoad(this.gameObject);

        LoadGameData();
        SaveGameData();
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

    public static void CheckDemansionComponent<T,C>(Transform trans,ref T d3d,ref C d2d) where T : Component where C : Component
    {
        if(trans.GetComponent<SpriteRenderer>() != null)
        {// 2D
            if(!trans.TryGetComponent(out d2d))
            {
                d2d = trans.gameObject.AddComponent<C>();
            }
            return;
        }
        else
        {// 3D
            if (!trans.TryGetComponent(out d3d))
            {
                d3d = trans.gameObject.AddComponent<T>();
            }
            return;
        }
    }

    public static void InputEnable(bool val)
    {
        PlayerInputController.Instance.gameObject.SetActive(val);
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

    public InstrumentBase GetCurrentInstrument()
    {
        return ResourceData<InstrumentBase>.GetData("Instrument/" + PlayerInputController.Instance.currentInstrument.Value.ToString());
    }

    [ContextMenu("ResetPlayerPrefs")]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

}

public class TagManager 
{
    public static string player = "Player";
    public static string ground = "Ground";
    public static string ladder = "Ladder";
    public static string laserEvent = "LaserEvent";
    public static string mirror = "Mirror";
    public static string wall = "Wall";

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