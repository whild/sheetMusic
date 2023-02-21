using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleInput : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] TitleOptionWindow titleWindow;
    [SerializeField] OptionWindow optionWindow;
    [SerializeField] GameObject copyLight;

    OptionWindowCore core;

    private void Awake()
    {
        core = titleWindow;
        copyLight.SetActive(false);
        var optionInput = _input.actions.FindActionMap("Title");
        optionInput["Decide"].started += OptionDecide;
        optionInput["Cancle"].started += OptionCancle;
        optionInput["Move"].started += OptionMove;
        optionInput["Copylight"].started += OpenCopyLight;
    }
    private void OptionMove(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        core.Move(value);
    }
    private void OptionDecide(InputAction.CallbackContext obj)
    {
        core.DecideCurrentOption();
        core = optionWindow;
        Debug.Log("Select Current Option");
    }

    public void OptionCancle(InputAction.CallbackContext obj)
    {
        if(core == optionWindow)
        {
            optionWindow.ShowOption(false);
            core = titleWindow;
        }
        copyLight.SetActive(false);
    }
    private void OpenCopyLight(InputAction.CallbackContext obj)
    {
        copyLight.SetActive(copyLight.activeSelf ? false : true);
    }
}
