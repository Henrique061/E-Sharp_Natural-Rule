using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // the name of the sound in the array

    public AudioClip clip; // reference to the audio clip

    public AudioMixerGroup audioMixerGroup;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source; // to store the AudioSource in a variable
}
