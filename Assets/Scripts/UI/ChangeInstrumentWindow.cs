using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ChangeInstrumentWindow : Manager<ChangeInstrumentWindow>
{
    [SerializeField] Transform trnas;
    List<Transform> Selections = new List<Transform>();
    [SerializeField] int currentInstrument;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ShowChangeInstrument(bool val)
    {
        this.gameObject.SetActive(val);
        currentInstrument = (int)PlayerInputController.Instance.currentInstrument.Value;
    }

    public void DecideInstrument()
    {
        PlayerInputController.Instance.currentInstrument.Value = (Instrument)currentInstrument;
        ShowChangeInstrument(false);
    }

    public void ShowSelection(Vector2 val)
    {
        if (val == Vector2.up) currentInstrument = 0;
        if (val == Vector2.right) currentInstrument = 1;
        if (val == Vector2.left) currentInstrument = 2;
        if (val == Vector2.down) currentInstrument = 3;


        //대충 인트값에 따라서 해당 영역이 밝아진다는 내용
    }
}
