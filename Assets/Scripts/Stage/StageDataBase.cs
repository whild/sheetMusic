using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "SheetMusic/StageData", order = 10000)]
[System.Serializable]
public class StageDataBase: ScriptableObject
{
    public int stageIndex;
    public GameObject stage_3D;
    public GameObject stage_2D;
    public Vector3 spawnPos_3D;
    public Vector3 spawnPos_2D;

    public bool goal3d;
    public bool goal2d;
    public bool needKey;
}
