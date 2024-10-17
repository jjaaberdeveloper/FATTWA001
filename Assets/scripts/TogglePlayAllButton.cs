using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePlayAllButton : MonoBehaviour
{
    public AudioSource audioSource;        // Reference to the AudioSource component
    public Image buttonImage2;              // Reference to the Image component on the Button
    public Sprite playIcon2;                // The play icon sprite
    public Sprite pauseIcon2;               // The pause icon sprite
    private bool isPlaying = false;        // Tracks whether the audio is currently playing

    void Start()
    {
        

        // Get the Button component on this GameObject and add a listener to it
        GetComponent<Button>().onClick.AddListener(ToggleAudio);

        // Set the initial button image to the play icon
        buttonImage2.sprite = playIcon2;
    }

    void ToggleAudio()
    {
        if (isPlaying)
        {
            audioSource.Stop();            // Stop the audio if it is playing
            buttonImage2.sprite = playIcon2; // Change to play icon
        }
        else
        {
            audioSource.Play();            // Play the audio if it is not playing
            buttonImage2.sprite = pauseIcon2; // Change to pause icon
        }
        isPlaying = !isPlaying;            // Toggle the state
    }
}
