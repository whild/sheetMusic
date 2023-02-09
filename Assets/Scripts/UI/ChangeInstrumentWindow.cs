using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ChangeInstrumentWindow : Manager<ChangeInstrumentWindow>
{
    [SerializeField] Transform container;
    List<Transform> Selections = new List<Transform>();
    [SerializeField] IntReactiveProperty currentInstrument = new IntReactiveProperty();

    protected override void Awake()
    {
        base.Awake();
        ShowChangeInstrument(false);

        currentInstrument
            .Where(val => val >= 0 && val <= GameManager.Instance.data.instrumentData.Length - 1)
            .Subscribe(val =>
            {
                Debug.Log(val);
                //대충 인트값에 따라서 해당 영역이 밝아진다는 내용
            });
    }

    public void ShowChangeInstrument(bool val)
    {
        this.container.gameObject.SetActive(val);
        if (val)
        {
            currentInstrument.Value = (int)PlayerInputController.Instance.currentInstrument.Value;
        }
    }

    public void DecideInstrument()
    {

        PlayerInputController.Instance.currentInstrument.Value = (Instrument)currentInstrument.Value;
        ShowChangeInstrument(false);
    }

    public void ShowSelection(Vector2 val)
    {
        if (val == Vector2.up) currentInstrument.Value = 0;
        if (val == Vector2.right) currentInstrument.Value = 1;
        if (val == Vector2.left) currentInstrument.Value= 2;
        if (val == Vector2.down) currentInstrument.Value = 3;
    }
}
