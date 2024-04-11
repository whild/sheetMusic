using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2Dキャラクターと会えたのかを担当
/// </summary>
public class With2DEvent : MonoBehaviour
{
    public void SetWith2D(bool val)
    {//バートンで使っている
        GameManager.Instance.data.withPaper = val;
    }
}
