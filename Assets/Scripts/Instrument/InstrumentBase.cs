using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum instrument 
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
    public int instrumentIndex;

    public AnimationClip idle_;
    public AnimationClip walk_;
    public AnimationClip dash_;
    public AnimationClip jump_;
    public AnimationClip playerAct_;

}
