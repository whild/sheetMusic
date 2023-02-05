using System;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FocusType 
{
    Together,
    Once,
}


public class FocusObject : MonoBehaviour
{
    [SerializeField] FocusType focusType;
    [ShowIf("showTargetTrasnform")]
    [SerializeField] Transform targetTransform;
    [SerializeField] float duration;
    [SerializeField] bool isFocused = false;

    private Collider collider;

    [Range(0.1f, 50.0f)]
    public float MagnetStrength = 5.0f;

    [Range(0.1f, 50.0f)]
    public float Proximity = 5.0f;

    private bool showTargetTrasnform { get { return focusType == FocusType.Once; } }
    private void Awake()
    {
        if(!TryGetComponent(out collider))
        {
            Debug.LogError("Thereis not Colider for trigger, Check it!");
        }
        if(focusType == FocusType.Together && targetTransform != this.transform)
        {
            this.targetTransform = this.transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isFocused)
        {
            StartCoroutine(Focus());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isFocused)
        {
            CameraMagnetTargetController.Instance.DeleteTargetGruop(targetTransform);
        }
    }

    protected IEnumerator Focus()
    {
        CameraMagnetTargetController.Instance.AddTargetGruop(targetTransform, focusType);
        if (focusType == FocusType.Once)
        {
            isFocused = true;
            yield return new WaitForSeconds(duration);
            CameraMagnetTargetController.Instance.DeleteTargetGruop(targetTransform);
        }
    }

}
