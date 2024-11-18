using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton instance

    [Serializable]
    public class Sound
    {
        public string name; // Name to identify the sound
        public AudioClip clip; // The audio clip to play
        [Range(0f, 1f)] public float volume = 1f; // Volume control
        [Range(0.1f, 3f)] public float pitch = 1f; // Pitch control
        public bool loop; // Should the sound loop
        public bool is3D = false; // Determines if the sound is 3D or 2D

        [HideInInspector] public AudioSource source; // Associated AudioSource
    }

    public Sound[] sounds; // Array of sounds to manage

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Persist across scenes

        // Initialize AudioSources for each sound
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.is3D ? 1f : 0f; // 3D sound (spatialBlend = 1), 2D sound (spatialBlend = 0)
        }
    }

    // Play a sound by name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        s.source.Play();
    }

    // Stop a sound by name
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        s.source.Stop();
    }

    // Play a sound at a specific position (for 3D sounds)
    public void PlayAtPosition(string name, Vector3 position)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }

        AudioSource.PlayClipAtPoint(s.clip, position, s.volume);
    }
}
