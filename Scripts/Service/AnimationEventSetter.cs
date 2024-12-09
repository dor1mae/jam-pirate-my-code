using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventSetter
{
    public Func<bool> AnimationComplete;

    private Animator _animator;

    private bool _isPlaying;

    public bool IsPlaying => _isPlaying;

    public AnimationEventSetter(Animator animator)
    {
        _animator = animator;

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = _animator.runtimeAnimatorController.animationClips[i];

            AnimationEvent animationEndEvent = new AnimationEvent();
            animationEndEvent.time = clip.length;
            animationEndEvent.functionName = "AnimationCompleteHandler";
            animationEndEvent.stringParameter = clip.name;

            AnimationEvent animationStartEvent = new AnimationEvent();
            animationStartEvent.time = 0f;
            animationStartEvent.functionName = "AnimationStartHandler";
            animationStartEvent.stringParameter = clip.name;

            clip.AddEvent(animationStartEvent);
            clip.AddEvent(animationEndEvent);
        }
    }

    public void AnimationCompleteHandler(string name)
    {
        Debug.Log($"{name} animation complete.");
        _isPlaying = false;
    }

    public void AnimationStartHandler(string name)
    {
        Debug.Log($"{name} animation start.");
        _isPlaying = true;
    }
}
