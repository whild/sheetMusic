using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour, IAnimatorControll
{

    [Header("*Animation")]
    [SerializeField] public Animator Animator;
    [SerializeField] AnimatorOverrideController AnimatorOverrideController;
    [SerializeField] AnimationClipOverrides ClipOverrides;

    private readonly string idle_ = "Idle";
    private readonly string walk_ = "Walk";
    private readonly string dash_ = "Dash";
    private readonly string jump_ = "Jump";
    private readonly string playeract_ = "Playeract";

    private void Awake()
    {
        this.Animator = this.GetComponent<Animator>();
        AnimationInit();
    }

    public void AnimationInit()
    {
        AnimatorOverrideController = new AnimatorOverrideController(Animator.runtimeAnimatorController);
        Animator.runtimeAnimatorController = AnimatorOverrideController;

        ClipOverrides = new AnimationClipOverrides(AnimatorOverrideController.overridesCount);
        AnimatorOverrideController.GetOverrides(ClipOverrides);

    }

    public void ChangeAnimation()
    {
        var instrument = ResourceData<InstrumentBase>.GetData("Instrument/" + PlayerInputController.Instance.currentInstrument.Value.ToString());

        SetAnimation(instrument.idle, idle_);
        SetAnimation(instrument.walk, walk_);
        SetAnimation(instrument.dash, dash_);
        SetAnimation(instrument.jump, jump_);
        SetAnimation(instrument.playerAct, playeract_);
    }

    private void SetAnimation(AnimationClip clip, string name)
    {
        AnimatorOverrideController[name] = clip;
    }

}


public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}
