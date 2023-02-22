using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class With2DEvent : MonoBehaviour
{
    public void SetWith2D(bool val)
    {
        GameManager.Instance.data.withPaper = val;
    }
}
