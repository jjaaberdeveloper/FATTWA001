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
    private bool isPlaying = false;        // Tracks whether the audio is currently playing

    void Start()
    {
        // Get the Button component on this GameObject and add a listener to it
        GetComponent<Button>().onClick.AddListener(ToggleAudio);

        // Set the initial button image to the play icon
        buttonImage.sprite = playIcon;
    }

    void ToggleAudio()
    {
        if (isPlaying)
        {
            audioSource.Stop();            // Stop the audio if it is playing
            buttonImage.sprite = playIcon; // Change to play icon
        }
        else
        {
            audioSource.Play();            // Play the audio if it is not playing
            buttonImage.sprite = pauseIcon; // Change to pause icon
        }
        isPlaying = !isPlaying;            // Toggle the state
    }
}
