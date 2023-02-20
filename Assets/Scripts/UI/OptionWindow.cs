using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Audio;
using TMPro;
using DG.Tweening;

public class OptionWindow : OptionWindowCore
{
    [Header("*SoundSlider")]
    [SerializeField] Slider bgm;
    [SerializeField] Slider effect;
    [SerializeField] Slider mike;
    private Slider controllSlider;

    [Header("*Type")]
    [SerializeField] Toggle playType;
    [SerializeField] RectTransform blockImage_type;
    [SerializeField] Dropdown mikeDropdown;

    private readonly string _playType = "PlayType";
    private readonly string _currnetMike = "Mike";
    private Vector2 blockPos = new Vector2(-150, 150);

    private MicrophoneListener microphoneListener;

    bool controllOption;

    private void Start()
    {
        Init();
        SetSoundEvent();
        setToggleEvent();
        Refresh();
        container.SetActive(false);
        microphoneListener = AudioManager.Instance.gameObject.GetComponent<MicrophoneListener>();
    }

    public void Init()
    {
        controllOption = false;
        controllSlider = null;

        //horizontalOptions = new List<Transform>();
        horizontalOptionValue.Value = 0;

        //verticalOptions = new List<Transform>();
        verticalOptionValue.Value = 0;

        if (!PlayerPrefs.HasKey(AudioManager.BGM))
        {
            PlayerPrefs.SetInt(AudioManager.BGM, 100);
        }
        if (!PlayerPrefs.HasKey(AudioManager.Effect))
        {
            PlayerPrefs.SetInt(AudioManager.Effect, 100);
        }
        if (!PlayerPrefs.HasKey(AudioManager.Mike))
        {
            PlayerPrefs.SetInt(AudioManager.Mike, 100);
        }
        if (!PlayerPrefs.HasKey(_playType))
        {
            PlayerPrefs.SetInt(_playType, 1);
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
        mike.onValueChanged.RemoveAllListeners();

        bgm.onValueChanged.AddListener((val) =>
        {
            AudioManager.Instance.ChangeVolume(AudioManager.BGM, (int)val);
        });

        effect.onValueChanged.AddListener((val) =>
        {
            AudioManager.Instance.ChangeVolume(AudioManager.Effect, (int)val);
        });

        mike.onValueChanged.AddListener((val) =>
        {
            AudioManager.Instance.ChangeVolume(AudioManager.Mike, (int)val);
        });
    }

    private void setToggleEvent()
    {
        playType.onValueChanged.RemoveAllListeners();
        mikeDropdown.onValueChanged.RemoveAllListeners();

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

        mikeDropdown.options = new List<Dropdown.OptionData>();
        foreach (var item in Microphone.devices)
        {
            mikeDropdown.options.Add(new Dropdown.OptionData(item));
        }
        mikeDropdown.value = PlayerPrefs.GetInt(_currnetMike);
        mikeDropdown.onValueChanged.AddListener((val) =>
        {
            PlayerPrefs.SetInt(_currnetMike, val);
            microphoneListener.ChangeMke();
        });
    }

    private void Refresh()
    {
        bgm.value = PlayerPrefs.GetInt(AudioManager.BGM);
        effect.value = PlayerPrefs.GetInt(AudioManager.Effect);
        mike.value = PlayerPrefs.GetInt(AudioManager.Mike);

        playType.isOn = (PlayerPrefs.GetInt(_playType) == 1 ? true : false);

    }

    public override void DecideCurrentOption()
    {
        base.DecideCurrentOption();
        if (!controllOption)
        {
            switch (optionValue.Value)
            {
                case 0:
                    controllSlider = bgm;
                    break;
                case 1:
                    controllSlider = effect;
                    break;
                case 2:
                    controllSlider = mike;
                    break;
                case 3:
                    AddOptions(ref horizontalOptions, horizontalParent[optionValue.Value]);
                    break;
                case 4:
                    AddOptions(ref verticalOptions, verticalParent[optionValue.Value]);
                    break;
                default:
                    break;
            }
            controllOption = true;
        }
        else
        {
            if(horizontalOptions != null)
            {
                playType.isOn = (horizontalOptionValue.Value == 0) ? true : false;
            }
            if(verticalOptions != null)
            {
                
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
