using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private List<AudioSource> audioSources;

    void Awake()
    {
        // Singleton pattern to ensure there's only one AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSources = new List<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterAudioSource(AudioSource source)
    {
        if (!audioSources.Contains(source))
        {
            audioSources.Add(source);
        }
    }

    public void PlayAudio(AudioSource source)
    {
        // Stop all other audio sources
        foreach (var audioSource in audioSources)
        {
            if (audioSource != source && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Play the requested audio source
        source.Play();
    }
}
