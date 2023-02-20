using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class OptionWindowCore : MonoBehaviour
{
    protected IntReactiveProperty optionValue = new IntReactiveProperty();
    protected IntReactiveProperty horizontalOptionValue = new IntReactiveProperty();
    protected IntReactiveProperty verticalOptionValue = new IntReactiveProperty();

    [SerializeField] protected bool showFirst;
    [SerializeField] protected GameObject container;
    [SerializeField] protected Transform selection;
    [SerializeField] protected List<Transform> optionTransforms = new List<Transform>();

    [SerializeField] protected List<Transform> horizontalParent = new List<Transform>();
    protected List<Transform> horizontalOptions = new List<Transform>();

    [SerializeField] protected List<Transform> verticalParent = new List<Transform>();
    protected List<Transform> verticalOptions = new List<Transform>();


    [SerializeField] AudioClip audioClip;
    AudioSource optionChangeSound;

    protected virtual void Awake()
    {
        AddAudio();

        AddOptions(ref optionTransforms, selection);

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
                optionChangeSound.Play();
                ShowSelectedOption(optionTransforms,val);
            });

        horizontalOptionValue
            .Subscribe(val =>
            {
                if (val < 0)
                {
                    horizontalOptionValue.Value = horizontalOptions.Count - 1;
                    return;
                }
                else if (val > horizontalOptions.Count - 1)
                {
                    horizontalOptionValue.Value = 0;
                    return;
                }
                optionChangeSound.Play();
                ShowSelectedOption(horizontalOptions,val);
            });

        verticalOptionValue
            .Subscribe(val =>
            {
                if (val < 0)
                {
                    verticalOptionValue.Value = verticalOptions.Count - 1;
                    return;
                }
                else if (val > verticalOptions.Count - 1)
                {
                    verticalOptionValue.Value = 0;
                    return;
                }
                optionChangeSound.Play();
                ShowSelectedOption(verticalOptions,val);
            });



        ShowOption(showFirst);
    }

    protected void AddOptions(ref List<Transform> container, Transform parent)
    {
        container = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            int temp = i;
            container.Add(parent.GetChild(temp));
        }
    }

    private void AddAudio()
    {
        optionChangeSound = this.gameObject.AddComponent<AudioSource>();
        optionChangeSound.playOnAwake = false;
        optionChangeSound.maxDistance = 2000;
        optionChangeSound.clip = audioClip;
    }

    public virtual void Move(Vector2 input)
    {
        if(input == Vector2.up)
        {
            MoveOptionUp();
            return;
        }
        if(input == Vector2.down)
        {
            MoveOptionDown();
            return;
        }
        if(input == Vector2.left)
        {
            MoveOptionLeft();
            return;
        }
        if(input == Vector2.right)
        {
            MoveOptionRight();
            return;
        }
    }

    public virtual void MoveOptionUp()
    {
        optionValue.Value--;
    }
    public virtual void MoveOptionDown()
    {
        optionValue.Value++;
    }

    public virtual void MoveOptionLeft()
    {

    }

    public virtual void MoveOptionRight()
    {

    }

    public void ShowOption(bool val)
    {
        this.container.SetActive(val);
        optionValue.Value = 0;
    }

    private void ShowSelectedOption(List<Transform> options, int value)
    {
        AudioManager.PlayAudio(optionChangeSound, AudioManager.Effect);
        for (int i = 0; i < options.Count; i++)
        {
            int temp = i;
            DOTween.Kill(options[temp].transform);
            options[temp].transform.localScale = new Vector3(1, 1, 1);
            if(value == temp)
            {
                options[temp].transform
                    .DOScale(1.1f, 0.2f);
            }
        }
    }
    protected virtual void ReturnGame(OptionWindowCore core)
    {
        PlayerInputController.Instance.TurnOption(false);
        PlayerInputController.Instance.SetOptionWindow(core);
    }
    public virtual void DecideCurrentOption()
    {
        var effectObj = GameObject.Instantiate(ResourceData<GameObject>.GetData("Effect/UiDicideEffect"), this.transform.parent);
        var effect = effectObj.GetComponent<ParticleSystem>();
        effect.Play();
        effectObj.transform.position = optionTransforms[optionValue.Value].position;
        StartCoroutine(DeleteEffect(effect));
    }

    IEnumerator DeleteEffect(ParticleSystem effect)
    {
        yield return new WaitForSeconds(effect.startLifetime);
        GameObject.Destroy(effect.gameObject);
    }
}
