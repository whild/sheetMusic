using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Jsonでセーブされます。
/// </summary>
[SerializeField]
public class SaveData
{
    //現在のステージ
    public int currentStage = 1;
    //2Dのキャラクターと会えたのか
    public bool withPaper = false;
    //得た楽器をセーブします。
    public bool[] instrumentData = new bool[4] { true, false, false, false };
}
