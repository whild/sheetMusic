using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] public Vector3 direction;

    private void Awake()
    {
        this.gameObject.tag = "Mirror";
    }
}
