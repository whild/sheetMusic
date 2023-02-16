using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Manager<AudioManager>
{

    [SerializeField] AudioMixer audioMixer;

    public readonly static string BGM = "BGM";
    public readonly static string Effect = "Effect";
    public readonly static string Mike = "Mike";

    protected override void Awake()
    {
        base.Awake();
        MixerSetup();
    }

    private void MixerSetup()
    {
        audioMixer.SetFloat(BGM, GetAudioValue(BGM));
        audioMixer.SetFloat(Effect, GetAudioValue(Effect));
        audioMixer.SetFloat(Mike, GetAudioValue(Mike));
    }

    public void ChangeVolume(string groupName, float volume)
    {
        PlayerPrefs.SetInt(groupName, (int)volume);
        audioMixer.SetFloat(groupName, volume);
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
            return Mathf.Lerp(-40, 0, (value * 0.01f));
        }
    }

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

}
