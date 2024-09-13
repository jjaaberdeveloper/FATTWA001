using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudioButton : MonoBehaviour
{
    public AudioSource audioSource;        // Reference to the AudioSource component
    public Image buttonImage;              // Reference to the Image component on the Button
    public Sprite playIcon;                // The play icon sprite
    public Sprite pauseIcon;               // The pause icon sprite

    private static AudioSource currentlyPlayingAudio = null; // Static variable to track the currently playing audio
    private static ToggleAudioButton currentlyActiveButton = null; // Static variable to track the currently active button

    void Start()
    {
        // Get the Button component on this GameObject and add a listener to it
        GetComponent<Button>().onClick.AddListener(ToggleAudio);

        // Set the initial button image to the play icon
        buttonImage.sprite = playIcon;
    }

    void ToggleAudio()
    {
        // If this audio is already playing, stop it
        if (currentlyPlayingAudio == audioSource)
        {
            StopAudio();
        }
        else
        {
            // Stop the currently playing audio if any
            if (currentlyPlayingAudio != null)
            {
                currentlyActiveButton.StopAudio();
            }

            // Play this audio
            PlayAudio();
        }
    }

    void PlayAudio()
    {
        audioSource.Play();            // Play the audio
        buttonImage.sprite = pauseIcon; // Change to pause icon

        // Update the static references
        currentlyPlayingAudio = audioSource;
        currentlyActiveButton = this;
    }

    void StopAudio()
    {
        audioSource.Stop();            // Stop the audio
        buttonImage.sprite = playIcon; // Change to play icon

        // Reset the static references
        if (currentlyPlayingAudio == audioSource)
        {
            currentlyPlayingAudio = null;
            currentlyActiveButton = null;
        }
    }

    // Method to update the button icon when stopping an audio from another button
    public void UpdateButtonIcon(bool playing)
    {
        buttonImage.sprite = playing ? pauseIcon : playIcon;
    }
}
