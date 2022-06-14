using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterSoundPlayer : MonoBehaviour
{
    public bool playOnEnable = false;
    public bool stopOnDisable = false;
    public bool playOnlyOnce = false;
    public string soundToPlay;

    private bool hasPlayed = false;

    private void OnEnable()
    {
        if (playOnEnable)
            Play();
    }

    private void OnDisable()
    {
        if (stopOnDisable)
            Stop();
    }

    public void Play()
    {
        if (playOnlyOnce && hasPlayed)
            return;

        if (LitterGameManager.instance.isGameReady)
            hasPlayed = true;

        SoundManagerScript.instance.Play(soundToPlay);
    }

    public void Stop()
    {
        SoundManagerScript.instance.Stop(soundToPlay);
    }
}
