using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Instrument 
{
    Automaton = 0,
    Violin = 1,
    Horn = 2,
}


[CreateAssetMenu(fileName = "Instrument", menuName = "SheetMusic/Create NewI nstrument", order = 10001)]
public class InstrumentBase : ScriptableObject
{
    /// <summary>
    /// Automaton = 0, Violin = 1, Horn = 2,
    /// </summary>
    [SerializeField] private int instrumentIndex_;

    [SerializeField] private AnimationClip idle_;
    [SerializeField] private AnimationClip walk_;
    [SerializeField] private AnimationClip dash_;
    [SerializeField] private AnimationClip jump_;
    [SerializeField] private AnimationClip playerAct_;

    [SerializeField] private AudioClip actAudio_;

    public int instrumentIndex
    {
        get { return instrumentIndex_; }
    }

    public AnimationClip idle
    {
        get { return idle_; }
    }

    public AnimationClip walk
    {
        get { return walk_; }
    }
    public AnimationClip dash
    {
        get { return dash_; }
    }

    public AnimationClip jump
    {
        get { return jump_; }
    }
    public AnimationClip playerAct
    {
        get { return playerAct_; }
    }

    public AudioClip actAudio
    {
        get { return actAudio_; }
    }

}
