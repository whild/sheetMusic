using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class PlayerInputController : Manager<PlayerInputController>
{
    [SerializeField] PlayerInput _input;
    [SerializeField] IMoveable _move;
    [SerializeField] MoveCore moveCore;

    [SerializeField] GameObject player_3d;
    [SerializeField] GameObject player_2d;
    [SerializeField] IMoveable Imove3d;
    [SerializeField] IMoveable Imove2d;
    [SerializeField] IAnimatorControll I3DAni;
    [SerializeField] IAnimatorControll I2DAni;
 
    [SerializeField] IInteract interact;
    [SerializeField] public List<IPlayerReact> playerReact = new List<IPlayerReact>();

    [SerializeField] public ReactiveProperty<Instrument> currentInstrument = new ReactiveProperty<Instrument>();

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out _input);

        player_3d = GameManager.Instance.player3D.gameObject;
        player_2d = GameManager.Instance.player2D.gameObject;

        player_3d.TryGetComponent(out Imove3d);
        player_2d.TryGetComponent(out Imove2d);

        player_3d.TryGetComponent(out I3DAni);
        player_2d.TryGetComponent(out I2DAni);

        currentInstrument.Value = Instrument.Automaton;

        currentInstrument
            .Subscribe(val =>
            {
                if (I3DAni != null)
                {
                    I3DAni.ChangeAnimation();
                }
                //I2DAni.ChangeAnimation();

                GameManager.Instance.data.currentInstrument = (int)val;
                GameManager.Instance.SaveGameData();
                //모델링 바꿔야함
                //애니메이션 바뀌는 거 확인 해야함
                //
            });

    }

    private void Start()
    {

        _move = Imove3d;
        moveCore = player_3d.GetComponent<MoveCore>();
    }

    private void OnEnable()
    {
        SetPlayerInput();
        SetOptionInput();
        SetChangeInstrumentInput();
    }

    private void SetPlayerInput()
    {
        var playerInput = _input.actions.FindActionMap("Player");
        playerInput["Move"].performed += OnMove;
        playerInput["Move"].canceled += OnMoveStop;
        playerInput["Jump"].started += OnJump;
        playerInput["Dash"].performed += OnDash;
        playerInput["Dash"].canceled += OnDashStop;
        playerInput["ChangeCharacter"].performed += OnChangeCharacter;
        playerInput["Interact"].started += OnInteract;
        playerInput["PlayerAct"].started += OnPlayerAct;
        playerInput["Option"].started += TurnOnOption;
        playerInput["Option"].started += SwitchActionMapEvent;
        playerInput["ChangeInstrument"].started += ChangeInstrument;
        playerInput["ChangeInstrument"].started += SwitchActionMapEvent;
    }

    private void SetOptionInput()
    {
        var optionInput = _input.actions.FindActionMap("Option");
        optionInput["MoveUp"].started += OptionMoveUp;
        optionInput["MoveDown"].started += OptionMoveDown;
        optionInput["Decide"].started += OptionDecide;
        optionInput["Cancle"].started += OptionCancle;
        optionInput["Cancle"].started += SwitchActionMapEvent;
    }


    private void SetChangeInstrumentInput()
    {
        var changeInstrument = _input.actions.FindActionMap("ChangeInstrument");
        changeInstrument["SelectInstrument"].performed += SelectInstrument;
        changeInstrument["Decide"].started += DecideInstrument;
        changeInstrument["Decide"].started += SwitchActionMapEvent;
        changeInstrument["Cancle"].started += ChangeCancle;
        changeInstrument["Cancle"].started += SwitchActionMapEvent;
    }

    private void ChangeInstrument(InputAction.CallbackContext obj)
    {
        _input.SwitchCurrentActionMap("ChangeInstrument");
        ChangeInstrumentWindow.Instance.ShowChangeInstrument(true);
    }

    private void TurnOnOption(InputAction.CallbackContext obj)
    {
        OptionWindowCore.Instance.ShowOption(true);
        _input.SwitchCurrentActionMap("Option");
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
        _input.actions["Jump"].started -= OnJump;
        _input.actions["Dash"].performed -= OnDash;
        _input.actions["Dash"].canceled -= OnDashStop;
        _input.actions["Interact"].started -= OnInteract;
        _input.actions["PlayerAct"].started -= OnPlayerAct;
    }

    #region Player
    private void OnMove(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        var direction = new Vector3(value.x, 0, value.y);

        if(MoveCore.isLadder && !moveCore.isGround)
        {
            direction.y = direction.z;
            direction.z = 0;
        }
        _move.SetDirection(direction);
    }
    private void OnMoveStop(InputAction.CallbackContext obj)
    {
        _move.SetDirection(Vector3.zero);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _move.Jump();
    }

    private void OnDash(InputAction.CallbackContext obj)
    {
        _move.Dash(Move3D.dashSpeed);
    }
    private void OnDashStop(InputAction.CallbackContext obj)
    {
        _move.Dash(Move3D.normalSpeed);
    }
    private void OnChangeCharacter(InputAction.CallbackContext obj)
    {
        Vector3 dir = _move.GetDirection();
        _move.SetDirection(Vector3.zero);
        _move = (_move.Equals(Imove3d) ? Imove2d : Imove3d);
        moveCore = (_move.Equals(Imove3d) ? player_2d.GetComponent<MoveCore>() : player_3d.GetComponent<MoveCore>());
        _move.SetDirection(dir);

        //MoveCamera
        CinemachineController.Instance.MoveCameraTo(_move.Equals(Imove3d)); 
    }
    private void OnInteract(InputAction.CallbackContext obj)
    {
        if (interact != null)
        {
            interact.Interact();
            Debug.Log("Interact");
        }
    }

    private void OnPlayerAct(InputAction.CallbackContext obj)
    {
        foreach (var reacts in playerReact)
        {
            reacts.PlayerReact((int)this.currentInstrument.Value);
        }
    }
    public void SetInteract(IInteract interact)
    {
        this.interact = interact;
    }

    public bool AlreadyHaveInteract(IInteract interact)
    {
        if (this.interact == interact)
        {
            return true;
        }
        return false;
    }

    public void SetPlayerReact(IPlayerReact playerReact)
    {
        this.playerReact.Add(playerReact);
    }

    #endregion

    #region Option
    private void OptionDecide(InputAction.CallbackContext obj)
    {
        OptionWindowCore.Instance.DecideCurrentOption();
        Debug.Log("Select Current Option");
    }

    private void OptionMoveDown(InputAction.CallbackContext obj)
    {
        OptionWindowCore.Instance.MoveOptionDown();
        Debug.Log("OptionDown");
    }

    private void OptionMoveUp(InputAction.CallbackContext obj)
    {
        OptionWindowCore.Instance.MoveOptionUp();
        Debug.Log("OptionUp");
    }
    public void OptionCancle(InputAction.CallbackContext obj)
    {
        TurnOption(false);
        Debug.Log("Switch ActionMap to Player");
    }

    public void TurnOption(bool val)
    {
        OptionWindowCore.Instance.ShowOption(val);
        _input.SwitchCurrentActionMap("Player");
    }
    #endregion

    #region ChangeInstrument
    private void DecideInstrument(InputAction.CallbackContext obj)
    {
        ChangeInstrumentWindow.Instance.DecideInstrument();
        Debug.Log("Decide Instrument");
    }

    private void SelectInstrument(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        ChangeInstrumentWindow.Instance.ShowSelection(value);
        Debug.Log("SelectInstrument");
    }

    private void ChangeCancle(InputAction.CallbackContext obj)
    {
        _input.SwitchCurrentActionMap("Player");
        ChangeInstrumentWindow.Instance.ShowChangeInstrument(false);
        Debug.Log("Switch ActionMap to Player");
    }
    #endregion


    private void SwitchActionMapEvent(InputAction.CallbackContext obj)
    {
        _move.SetDirection(Vector3.zero);

        Debug.Log("SwitchActionMap");
    }
}
