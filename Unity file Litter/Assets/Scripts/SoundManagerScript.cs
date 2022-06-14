using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    #region Singleton

    public static SoundManagerScript instance { get; private set; }

    #endregion


    //The main idea of this audio manager is to have a list of sounds where you can easily add or remove any sounds as you go
    public Sound[] sounds;   //this is public sound array named sound
    public AudioSource playerAudioSource;
    public AudioSource musicAudioSource;
    public float pitchVariationRange = 0.1f;
    public float volumeVariationRange = 0.1f;

    void Awake() //To play sounds at the start 
    {
        // Init our instance
        if (instance == null)
            instance = this;

        if (instance == this)
        {

        }
        else
        {
            // Destroy ourselves if we are not the correct manager
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)// to loop through a sound to go for each sounds and call the sounds that we want
        {
            switch (s.type)
            {
                case SoundType.Player:
                    s.source = playerAudioSource;
                    break;
                case SoundType.Music:
                    s.source = musicAudioSource;
                    break;
                default:
                    s.source = gameObject.AddComponent<AudioSource>(); //To add an audio source component. 
                    s.source.clip = s.clip; // The sound we are looking at with the source will be equal to the audio source component
                    s.source.loop = s.loop; //To loop the audio
                    break;
            }
            //Later when you want to play the sound you can call the play method on the AUdio Source                            
        }

        playerAudioSource.loop = false;
        musicAudioSource.loop = true;
    }

    public void Play(string name) // A new public method which will take in a string with any of our sounds
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //This will find the sound with the appropriate name
        //We store the sound that we found in the variable s
        if (s == null) //This code will end the current game object
        {
            Debug.LogWarning("Sound:" + name + "not found!"); //to not play a sound that it there
            return;
        }
        if (s.type == SoundType.Music)
        {
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.Play();
        }
        else
        {
            if (s.overrideClip || (!s.overrideClip && !s.source.isPlaying))
            {
                if (s.type != SoundType.Default)
                {
                    s.source.clip = s.clip;
                }

                s.source.pitch = UnityEngine.Random.Range(s.pitch - pitchVariationRange, s.pitch + pitchVariationRange);
                s.source.volume = Mathf.Max(UnityEngine.Random.Range(s.volume - volumeVariationRange, s.volume + volumeVariationRange), 0.01f);
                s.source.Play();
            }
        }


    }

    public void Stop(string name) // A new public method which will take in a string with any of our sounds
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //This will find the sound with the appropriate name
        //We store the sound that we found in the variable s
        if (s == null) //This code will end the current game object
        {
            Debug.LogWarning("Sound:" + name + "not found!"); //to not play a sound that it there
            return;
        }

        if(s.source != null)
            if(s.source.isPlaying)
                s.source.Stop();
    }
}

























/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound, collisionSound, looseSound, winSound, runSound;
    static AudioSource audioSrc; 

    // Start is called before the first frame update
    void Start()
    {
        collisionSound = Resources.Load<AudioClip> ("collision");
        jumpSound = Resources.Load<AudioClip> ("jump");
        looseSound = Resources.Load<AudioClip> ("loose");
        winSound = Resources.Load<AudioClip> ("win");
        runSound = Resources.Load<AudioClip> ("run");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip) {
            case "collision":
                audioSrc.PlayOneShot(collisionSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "loose":
                audioSrc.PlayOneShot(looseSound);
                break;
            case "win":
                audioSrc.PlayOneShot(winSound);
                break;
            case "run":
                audioSrc.PlayOneShot(runSound);
                break;
        }

    }
}
*/
