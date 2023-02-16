using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionWindow : MonoBehaviour
{
    [Header("*Container")]
    [SerializeField] GameObject container;

    [Header("*SoundSlider")]
    [SerializeField] Slider bgm;
    [SerializeField] Slider effect;
    [SerializeField] Slider mike;

    [Header("*Type")]
    [SerializeField] Toggle playType;
    [SerializeField] RectTransform blockImage_type;
    [SerializeField] Toggle windowMode;
    [SerializeField] RectTransform blockImage_mode;

    private readonly string _playType = "PlayType";
    private readonly string _windowMode = "WindowMode";
    private Vector2 blockPos = new Vector2(-150, 150);


    private void Start()
    {
        Init();
        SetSoundEvent();
        setToggleEvent();
        Refresh();
        container.SetActive(false);
    }

    private void Init()
    {
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
        if (!PlayerPrefs.HasKey(_windowMode))
        {
            PlayerPrefs.SetInt(_windowMode, 1);
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
        windowMode.onValueChanged.RemoveAllListeners();

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

        windowMode.onValueChanged.AddListener((isFullScreen) =>
        {
            DOTween.Complete(blockImage_mode);
            if (isFullScreen)
            {
                blockImage_mode.anchoredPosition = new Vector2(blockPos.y, blockImage_type.anchoredPosition.y);
                blockImage_mode.DOAnchorPosX(blockPos.x, 1f);
            }
            else
            {
                blockImage_mode.anchoredPosition = new Vector2(blockPos.x, blockImage_type.anchoredPosition.y);
                blockImage_mode.DOAnchorPosX(blockPos.y, 1f);
            }
            PlayerPrefs.SetInt(_playType, (isFullScreen) ? 1 : 0);
        });
    }

    private void Refresh()
    {
        bgm.value = PlayerPrefs.GetInt(AudioManager.BGM);
        effect.value = PlayerPrefs.GetInt(AudioManager.Effect);
        mike.value = PlayerPrefs.GetInt(AudioManager.Mike);

        playType.isOn = (PlayerPrefs.GetInt(_playType) == 1 ? true : false);
        windowMode.isOn = (PlayerPrefs.GetInt(_windowMode) == 1 ? true : false);
    }

    public void OpenOptionWindow()
    {
        Refresh();
        this.container.gameObject.SetActive(true);
    }
}
