// Control which data is stored in each sound
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

    public string name;

    public AudioClip clip;

    // range attribute adds sliders to volume and pitch
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    // Keeps it hidden in inspector
    [HideInInspector]
    public AudioSource source;

}
