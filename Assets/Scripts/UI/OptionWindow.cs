using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class OptionWindow : Manager<OptionWindow>
{
    [Range(0, 4)]
    private IntReactiveProperty optionValue = new IntReactiveProperty();

    [SerializeField] Transform selection;
    [SerializeField] List<Button> optionButtons = new List<Button>();

    protected override void Awake()
    {
        base.Awake();

        optionButtons = new List<Button>();
        optionButtons.AddRange(selection.GetComponentsInChildren<Button>());

        optionValue
            .Subscribe(val =>
            {
                if (val < 0)
                {
                    optionValue.Value = optionButtons.Count - 1;
                    return;
                }
                else if (val > optionButtons.Count - 1)
                {
                    optionValue.Value = 0;
                    return;
                }

                ShowSelectedOption();
            });

        ShowOption(false);
    }

    public void MoveOptionUp()
    {
        optionValue.Value--;
    }
    public void MoveOptionDown()
    {
        optionValue.Value++;
    }

    public void ShowOption(bool val)
    {
        this.gameObject.SetActive(val);
        optionValue.Value = 0;
    }

    private void ShowSelectedOption()
    {
        for (int i = 0; i < optionButtons.Count; i++)
        {
            int temp = i;
            DOTween.Kill(optionButtons[temp].transform);
            optionButtons[temp].transform.localScale = new Vector3(1, 1, 1);
            if(optionValue.Value == temp)
            {
                optionButtons[temp].transform
                    .DOScale(1.1f, 0.2f);
            }
        }
    }

    public void DecideCurrentOption()
    {
        Debug.Log("You Decide\t" + optionValue.Value + "  Option");
    }

}
