using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikeManager : Manager<MikeManager>
{

    AudioSource audioSource;
    string input;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        audioSource.loop = true;
        audioSource.mute = true;
        input = Microphone.devices[0].ToString();
    }

}