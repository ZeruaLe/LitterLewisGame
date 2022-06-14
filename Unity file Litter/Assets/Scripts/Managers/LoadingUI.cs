using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadingUI : MonoBehaviour
{
    public Animation animator;
    public AnimationClip fadeIn;
    public AnimationClip fadeOut;

    private UnityAction onComplete;

    public void FadeIn(UnityAction complete)
    {
        onComplete = complete;

        animator.Play(fadeIn.name);
    }

    public void FadeOut(UnityAction complete)
    {
        onComplete = complete;

        animator.Play(fadeOut.name);
    }

    public void OnFadeComplete()
    {
        onComplete?.Invoke();
    }
}
