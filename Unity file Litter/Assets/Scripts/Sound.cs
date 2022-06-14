using UnityEngine.Audio;
using UnityEngine;


public enum SoundType
{
    Default,
    Player,
    Music
}

[System.Serializable] //On unity under audio manager a list of audio should show up

public class Sound {

    public string name; // The name for each of audio used 
    public SoundType type;

    public AudioClip clip; // Referencing to our audio clip

    //Slider to volume and pitch where a minimum and maximum value is inputted
    [Range(0f,1f)]
    public float volume; //referencing to our volume
    [Range(.1f,3f)]
    public float pitch; //referencing to our pitch 

    public bool loop; //To repeat the audio you play
    public bool overrideClip;

    [HideInInspector] //Eventhough the variable is public this method will not show
    public AudioSource source; 

}

