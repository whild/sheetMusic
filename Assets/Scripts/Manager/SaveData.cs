using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SaveData
{
    public int currentStage = 1;
    public bool withPaper = false;
    public bool[] instrumentData = new bool[4] { true, false, false, false };
}
