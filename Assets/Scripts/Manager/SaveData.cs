using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SaveData
{
    public int currentStage;
    public bool withPaper;
    public bool[] instrumentData = new bool[4] { true, false, false, false };
}
