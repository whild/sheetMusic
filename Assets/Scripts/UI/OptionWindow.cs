using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    [Header("*Container")]
    [SerializeField] GameObject container;

    [Header("*SoundSlider")]
    [SerializeField] Slider bgm;
    [SerializeField] Slider effect;
    [SerializeField] Slider mike;

    private void Awake()
    {
        SetSoundEvent();

        container.SetActive(false);
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
}
