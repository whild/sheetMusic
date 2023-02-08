using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleInput : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] TitleOptionWindow titleManager;

    private void Awake()
    {
        titleManager = this.GetComponent<TitleOptionWindow>();
        _input.actions["ControllOptions"].performed += ControllOptions;
        _input.actions["Decide"].performed += Decide;
    }

    private void ControllOptions(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        if (value == Vector2.up)
        {
            OptionWindowCore.Instance.MoveOptionUp();
            return;
        }
        if (value == Vector2.down)
        {
            OptionWindowCore.Instance.MoveOptionDown();
            return;
        }
    }
    private void Decide(InputAction.CallbackContext obj)
    {
        OptionWindowCore.Instance.DecideCurrentOption();
        Debug.Log("Decide");
    }

}
