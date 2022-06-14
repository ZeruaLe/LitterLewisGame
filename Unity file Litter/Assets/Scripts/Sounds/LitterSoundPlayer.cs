using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterSoundPlayer : MonoBehaviour
{
    public bool playOnEnable = false;
    public string soundToPlay;

    private void OnEnable()
    {
        if (playOnEnable)
            Play();
    }

    public void Play()
    {
        SoundManagerScript.instance.Play(soundToPlay);
    }
}
