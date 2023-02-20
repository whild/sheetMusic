using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleInput : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] TitleOptionWindow titleManager;

    OptionWindowCore optionWindow;

    private void Awake()
    {
        optionWindow = FindObjectOfType<OptionWindowCore>();
        titleManager = this.GetComponent<TitleOptionWindow>();
        _input.actions["ControllOptions"].performed += ControllOptions;
        _input.actions["Decide"].performed += Decide;
    }

    private void ControllOptions(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        if (value == Vector2.up)
        {
            optionWindow.MoveOptionUp();
            return;
        }
        if (value == Vector2.down)
        {
            optionWindow.MoveOptionDown();
            return;
        }
    }
    private void Decide(InputAction.CallbackContext obj)
    {
        optionWindow.DecideCurrentOption();
        Debug.Log("Decide");
    }

}
