using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MicrophoneListener : MonoBehaviour
{
    public AudioClip clip;
    int sampleRate = 44100;
    private float[] samples;
    public float rmsValue;
    public float modulate;
    public float resultValue;
    private void Start()
    {
        samples = new float[sampleRate];
        clip = Microphone.Start(Microphone.devices[1].ToString(), true, 1, sampleRate);
    }

    private void Update()
    {
        clip.GetData(samples, 0);//-1~ 1
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            int temp = i;
            sum += samples[temp] * samples[temp];
        }
        rmsValue = Mathf.Sqrt(sum / samples.Length);
        rmsValue = rmsValue * modulate;
        rmsValue = Mathf.Clamp(rmsValue, 0, 100);
        resultValue = Mathf.RoundToInt(rmsValue);
        if(resultValue < 100)
        {
            AudioManager.Instance.currentLoud.Value = -1;
            AudioManager.isMike = false;
            resultValue = 0;
            return;
        }
        AudioManager.Instance.currentLoud.Value = (int)resultValue;

    }
}