using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MeshRenderer _wall;

    public static MeshRenderer _walls;

    private void Awake()
    {
        GameManager._walls = _wall;
        DontDestroyOnLoad(this.gameObject);
    }
}
