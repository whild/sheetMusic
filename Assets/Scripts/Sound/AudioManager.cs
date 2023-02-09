using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Manager<AudioManager>
{

    [SerializeField] AudioMixer audioMixer;

    protected override void Awake()
    {
        base.Awake();
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


}
