using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLookAt : MonoBehaviour
{
    void Update()
    {
        this.transform.LookAt(Camera.main.transform);
    }
}
