using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UniRx;

public class AudioManager : Manager<AudioManager>
{

    [SerializeField] public AudioMixer audioMixer;

    public readonly static string BGM = "BGM";
    public readonly static string Effect = "Effect";
    public readonly static string Player = "Player";

    public IntReactiveProperty currentLoud = new IntReactiveProperty();

    public static bool isMike;

    #region　最初設定
    protected override void Awake()
    {
        base.Awake();

        currentLoud
            .Where(val => val >= 100)
            .ThrottleFirst(System.TimeSpan.FromSeconds(2))
            .Subscribe(val =>
            {
                Debug.Log(val);
                isMike = true;
                PlayerInputController.Instance.AutoActWithMike();
            });

        MixerSetup();
    }

    private void MixerSetup()
    {
        audioMixer.SetFloat(BGM, GetAudioValue(BGM));
        audioMixer.SetFloat(Effect, GetAudioValue(Effect));
        audioMixer.SetFloat(Player, GetAudioValue(Player));
    }
    #endregion


    public void ChangeVolume(string groupName, float volume)
    {//groupNameサウンドの音量をvolumeに変更します。
        PlayerPrefs.SetInt(groupName, (int)volume);
        audioMixer.SetFloat(groupName, GetAudioValue(groupName));
    }

    private float GetAudioValue(string name)
    {
        if (!PlayerPrefs.HasKey(name))
        {
            PlayerPrefs.SetInt(name, 100);
        }
        float value = PlayerPrefs.GetInt(name);
        if(value == 0)
        {
            return -80;
        }
        else
        {
            return Mathf.Lerp(-40, 0, (value * 0.1f));
        }
    }
    #region サウンド再生
    public static void PlayAudio(AudioSource audioSource, string groupName)
    {
        if(audioSource == null)
        {
            Debug.LogError("There isn't audio");
            return;
        }
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups(groupName)[0];

        audioSource.Play();
    }

    public static void PlayAudio(AudioSource audioSource, AudioClip clip)
    {
        if(audioSource == null)
        {
            Debug.LogError("There isn't audio");
            return;
        }
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = clip;

        audioSource.Play();
    }
    public static void PlayAudio(AudioSource audioSource)
    {
        if(audioSource == null)
        {
            Debug.LogError("There isn't audio");
            return;
        }
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.Play();
    }
    #endregion
    public static void SetUiAudio(Transform from, ref AudioSource source, AudioClip clip)
    {
        source = from.gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.maxDistance = 2000;
        source.clip = clip;
    }

}
