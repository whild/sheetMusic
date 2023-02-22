using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class OptionWindow : OptionWindowCore
{
    [Header("*SoundSlider")]
    [SerializeField] Slider player;
    [SerializeField] Slider bgm;
    [SerializeField] Slider effect;
    private Slider controllSlider;

    [Header("*Type")]
    [SerializeField] Toggle playType;
    [SerializeField] RectTransform blockImage_type;

    private readonly string _playType = "PlayType";
    private readonly string _currnetMike = "currentMike";
    private Vector2 blockPos = new Vector2(-150, 150);

    private MicrophoneListener microphoneListener;
    [SerializeField] Text mikeNames;

    bool controllOption;

    private void Start()
    {
        Init();
        SetSoundEvent();
        SetToggleEvent();
        SetMikeEvent();
        Refresh();
        container.SetActive(false);
        microphoneListener = AudioManager.Instance.gameObject.GetComponent<MicrophoneListener>();

        /*
        this.horizontalOptionValue = new IntReactiveProperty();
        horizontalOptionValue
            .Subscribe(val =>
            {
                if(val )
            });
        */
    }

    public void Init()
    {
        controllOption = false;
        controllSlider = null;

        horizontalOptions = new List<Transform>();
        horizontalOptionValue.Value = 0;

        verticalOptions = new List<Transform>();
        verticalOptionValue.Value = 0;

        if (!PlayerPrefs.HasKey(AudioManager.BGM))
        {
            PlayerPrefs.SetInt(AudioManager.BGM, 10);
        }
        if (!PlayerPrefs.HasKey(AudioManager.Effect))
        {
            PlayerPrefs.SetInt(AudioManager.Effect, 10);
        }
        if (!PlayerPrefs.HasKey(AudioManager.Player))
        {
            PlayerPrefs.SetInt(AudioManager.Player, 10);
        }
        if (!PlayerPrefs.HasKey(_playType))
        {
            PlayerPrefs.SetInt(_playType, 0);
        }
        if (!PlayerPrefs.HasKey(_currnetMike))
        {
            PlayerPrefs.SetInt(_currnetMike, 0);
        }
    }

    private void SetSoundEvent()
    {
        bgm.onValueChanged.RemoveAllListeners();
        effect.onValueChanged.RemoveAllListeners();
        player.onValueChanged.RemoveAllListeners();

        bgm.onValueChanged.AddListener((val) =>
        {
            AudioManager.Instance.ChangeVolume(AudioManager.BGM, (int)val);
        });

        effect.onValueChanged.AddListener((val) =>
        {
            AudioManager.Instance.ChangeVolume(AudioManager.Effect, (int)val);
        });

        player.onValueChanged.AddListener((val) =>
        {
            AudioManager.Instance.ChangeVolume(AudioManager.Player, (int)val);
        });
    }

    private void SetToggleEvent()
    {
        playType.onValueChanged.RemoveAllListeners();

        playType.onValueChanged.AddListener((isMike) =>
        {
            DOTween.Complete(blockImage_type);
            if (isMike)
            {
                blockImage_type.anchoredPosition = new Vector2(blockPos.y, blockImage_type.anchoredPosition.y);
                blockImage_type.DOAnchorPosX(blockPos.x, 1f);
                Debug.Log("MikeEvent");
            }
            else
            {
                blockImage_type.anchoredPosition = new Vector2(blockPos.x, blockImage_type.anchoredPosition.y);
                blockImage_type.DOAnchorPosX(blockPos.y, 1f);
                Debug.Log("ButtonEvent");
            }
            PlayerPrefs.SetInt(_playType, (isMike) ? 1 : 0);
        });
    }

    private void SetMikeEvent()
    {
        int current = PlayerPrefs.GetInt(_currnetMike);
        mikeNames.text = Microphone.devices[current];//center
    }

    private void Refresh()
    {
        bgm.value = PlayerPrefs.GetInt(AudioManager.BGM);
        effect.value = PlayerPrefs.GetInt(AudioManager.Effect);
        player.value = PlayerPrefs.GetInt(AudioManager.Player);

        playType.isOn = (PlayerPrefs.GetInt(_playType) == 1 ? true : false);

    }

    public override void DecideCurrentOption()
    {
        base.DecideCurrentOption();
        valueChangeEvent.RemoveAllListeners();
        if (!controllOption)
        {
            switch (optionValue.Value)
            {
                case 0:
                    controllSlider = player;
                    break;
                case 1:
                    controllSlider = bgm;
                    break;
                case 2:
                    controllSlider = effect;
                    break;
                case 3:
                    AddOptions(ref horizontalOptions, horizontalParent[optionValue.Value]);
                    break;
                case 4:
                    foreach (var item in Microphone.devices)
                    {
                        horizontalOptions.Add(mikeNames.transform);
                    }
                    valueChangeEvent.AddListener(() =>
                    {
                        mikeNames.text = Microphone.devices[horizontalOptionValue.Value];
                    });
                    break;
                default:
                    break;
            }
            controllOption = true;
        }
        else
        {
            if(optionValue.Value == 3)
            {
                playType.isOn = (horizontalOptionValue.Value == 0) ? true : false;
            }
            if(optionValue.Value == 4)
            {
                PlayerPrefs.SetInt(_currnetMike, horizontalOptionValue.Value);
                GameObject.FindObjectOfType<MicrophoneListener>().ChangeMke();
            }
            Init();
        }
    }

    public override void MoveOptionUp()
    {
        if (!controllOption)
        {
            base.MoveOptionUp();
        }
        else
        {
            verticalOptionValue.Value--;
        }
    }

    public override void MoveOptionDown()
    {
        if (!controllOption)
        {
            base.MoveOptionDown();
        }
        else
        {
            verticalOptionValue.Value++;
        }
    }

    public override void MoveOptionLeft()
    {
        if (controllOption)
        {
            ChangeSliderValue(false);
            horizontalOptionValue.Value--;
        }
    }

    public override void MoveOptionRight()
    {
        if (controllOption)
        {
            ChangeSliderValue(true);
            horizontalOptionValue.Value++;
        }
    }

    private void ChangeSliderValue(bool isUp)
    {
        if (controllSlider != null)
        {
            controllSlider.value += (isUp) ? 1 : -1;
        }
    }

    public void OpenOptionWindow()
    {
        Refresh();
        this.container.gameObject.SetActive(true);
    }
}
