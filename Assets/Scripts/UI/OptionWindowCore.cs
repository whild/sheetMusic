using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class OptionWindowCore : Manager<OptionWindowCore>
{
    protected IntReactiveProperty optionValue = new IntReactiveProperty();

    [SerializeField] protected bool showFirst;
    [SerializeField] protected GameObject container;
    [SerializeField] protected Transform selection;
    [SerializeField] protected List<Transform> optionTransforms = new List<Transform>();

    [SerializeField] AudioSource optionChangeSound;

    protected override void Awake()
    {
        base.Awake();

        optionTransforms = new List<Transform>();
        for (int i = 0; i < selection.childCount; i++)
        {
            int temp = i;
            optionTransforms.Add(selection.GetChild(temp));
        }

        optionValue
            .Subscribe(val =>
            {
                if (val < 0)
                {
                    optionValue.Value = optionTransforms.Count - 1;
                    return;
                }
                else if (val > optionTransforms.Count - 1)
                {
                    optionValue.Value = 0;
                    return;
                }

                ShowSelectedOption();
            });

        ShowOption(showFirst);
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
        this.container.SetActive(val);
        optionValue.Value = 0;
    }

    private void ShowSelectedOption()
    {
        AudioManager.PlayAudio(optionChangeSound);
        for (int i = 0; i < optionTransforms.Count; i++)
        {
            int temp = i;
            DOTween.Kill(optionTransforms[temp].transform);
            optionTransforms[temp].transform.localScale = new Vector3(1, 1, 1);
            if(optionValue.Value == temp)
            {
                optionTransforms[temp].transform
                    .DOScale(1.1f, 0.2f);
            }
        }
    }

    public virtual void DecideCurrentOption()
    {

    }
}
