// Control all audio in game
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

	// Loop through the list and for each sound add an audio source
    // awake method is similar to start except its called right before
	void Awake () {
        // Also some code to not destroy sound when changing scene
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        } // else

        DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        } // foreach
	} // Awake

    void Start()
    {
        Play("Theme");
    } // Start

    // method to play sound 
    public void Play (string name)
    {
        // Find the sounds in the sounds array and we want to find the sound where
        // sound.name is equal to the name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found...");
            return;
        } // if
        s.source.Play();
    } // Play

} // class AudioManager
