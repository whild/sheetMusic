using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class ChangeInstrumentWindow : Manager<ChangeInstrumentWindow>
{
    [SerializeField] Transform container;
    [SerializeField] List<RectTransform> Highlights = new List<RectTransform>();
    [SerializeField] List<RectTransform> Icons = new List<RectTransform>();
    [SerializeField] IntReactiveProperty currentInstrument = new IntReactiveProperty();

    private Vector2 orisinalPos = new Vector2(50, 50);
    private Vector2 endPos = new Vector2(80, 80);

    protected override void Awake()
    {
        base.Awake();
        ShowChangeInstrument(false);

        currentInstrument
            .Where(val => val >= 0 && val <= GameManager.Instance.data.instrumentData.Length - 1)
            .Subscribe(val =>
            {
                Debug.Log(val);
                Highlight(val);
                //대충 인트값에 따라서 해당 영역이 밝아진다는 내용
            });
    }

    public void ShowChangeInstrument(bool val)
    {
        this.container.gameObject.SetActive(val);
        if (val)
        {
            currentInstrument.Value = (int)PlayerInputController.Instance.currentInstrument.Value;
            Highlight(currentInstrument.Value);
        }
    }

    public void DecideInstrument()
    {
        PlayerInputController.Instance.currentInstrument.Value = (Instrument)currentInstrument.Value;
        ShowChangeInstrument(false);
    }

    public void ShowSelection(Vector2 val)
    {
        SaveData data = GameManager.Instance.data;
        if (val == Vector2.up && data.instrumentData[0]) currentInstrument.Value = 0;
        if (val == Vector2.right && data.instrumentData[1]) currentInstrument.Value = 1;
        if (val == Vector2.left && data.instrumentData[2]) currentInstrument.Value= 2;
        if (val == Vector2.down && data.instrumentData[3]) currentInstrument.Value = 3;
    }

    private void Highlight(int val)
    {
        for (int i = 0; i < Highlights.Count; i++)
        {
            int temp = i;
            Highlights[temp].gameObject.SetActive(false);
            Image icon = Icons[temp].GetComponent<Image>();
            icon.color = Color.gray;

            DOTween.Complete(Icons[temp]);
            Icons[temp].anchoredPosition = orisinalPos;
            Icons[temp].localScale = Vector3.one;

            if (val == temp)
            {
                Highlights[temp].gameObject.SetActive(true);
                icon.color = Color.white;
                Icons[temp].DOAnchorPos(endPos, 0.5f);
                Icons[temp].DOScale(1.25f, 0.5f);
            }
        }
    }
}
