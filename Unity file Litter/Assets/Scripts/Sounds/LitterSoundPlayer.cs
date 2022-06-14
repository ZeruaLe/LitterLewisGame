using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterSoundPlayer : MonoBehaviour
{
    public bool playOnEnable = false;
    public bool stopOnDisable = false;
    public string soundToPlay;

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
        SoundManagerScript.instance.Play(soundToPlay);
    }

    public void Stop()
    {
        SoundManagerScript.instance.Stop(soundToPlay);
    }
}
