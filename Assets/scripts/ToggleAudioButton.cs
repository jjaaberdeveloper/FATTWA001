using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Timeline;

public class ToggleAudioButton : MonoBehaviour
{
    public List<AudioClip> audioClips;         // List of audio tracks
    public AudioSource audioSource;            // Reference to the AudioSource component
    public Image playPauseButtonImage;         // Reference to the Play/Pause button image
    public Sprite playIcon;                    // Play icon sprite
    public Sprite pauseIcon;                   // Pause icon sprite
    public GameObject mediaPlayerPanel;        // Reference to the media player panel
    public TextMeshProUGUI trackNameText;      // Reference to the track name text
    public Slider progressBar;                 // Reference to the progress bar
    public VideoPlayer soundbarAnimation;      // VideoPlayer for the soundbar animation
    public Button closeButton;                 // Reference to the close button
    public Button skipForwardButton;           // Reference to the skip forward button
    public Button skipBackwardButton;          // Reference to the skip backward button

    private bool isPlaying = false;            // Tracks whether audio is playing
    private bool isDragging = false;           // Tracks if the progress bar is being dragged
    private int currentTrackIndex = 0;         // Tracks the current playing track index
    private bool isSkipping = false;           // Prevents double skipping

    void Start()
    {
        // Ensure mediaPlayerPanel is inactive initially
        if (mediaPlayerPanel != null)
        {
            mediaPlayerPanel.SetActive(false);
        }

        // Assign button listeners
        GetComponent<Button>().onClick.AddListener(ToggleAudio);
        skipForwardButton.onClick.AddListener(SkipForward);
        skipBackwardButton.onClick.AddListener(SkipBackward);
        closeButton.onClick.AddListener(CloseMediaPlayer);

        // Initialize the progress bar and its events
        if (progressBar != null)
        {
            progressBar.onValueChanged.AddListener(OnProgressBarChanged);
        }

        // Ensure that the first track is set initially
        SetTrack(0);
    }

    void Update()
    {
        if (isPlaying && !isDragging)
        {
            // Update the progress bar based on the audio playback time
            progressBar.value = audioSource.time / audioSource.clip.length;

            // Automatically skip to the next track when the current track finishes
            if (!audioSource.isPlaying && audioSource.time >= audioSource.clip.length)
            {
                SkipForward();
            }
        }
    }

    public void ToggleAudio()
    {
        // Ensure the media player always opens with the first track
        if (!mediaPlayerPanel.activeSelf)
        {
            SetTrack(0);  // Always start with the first track when opening the media player
        }

        if (isPlaying)
        {
            PauseAudio();
        }
        else
        {
            PlayAudio();
        }
    }

    void PlayAudio()
    {
        // Play the audio
        audioSource.Play();
        playPauseButtonImage.sprite = pauseIcon;
        isPlaying = true;

        // Show the media player and update the track name
        if (mediaPlayerPanel != null)
        {
            mediaPlayerPanel.SetActive(true);
        }
        if (trackNameText != null && audioSource.clip != null)
        {
            trackNameText.text = audioSource.clip.name;
        }

        // Play the soundbar animation
        if (soundbarAnimation != null)
        {
            soundbarAnimation.Play();
        }
    }

    public void PauseAudio()
    {
        // Pause the audio
        audioSource.Pause();
        playPauseButtonImage.sprite = playIcon;
        isPlaying = false;

        // Pause the soundbar animation
        if (soundbarAnimation != null)
        {
            soundbarAnimation.Pause();
        }
    }

    // Fast forward by 5 seconds
    public void FastForward()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.time = Mathf.Min(audioSource.time + 5f, audioSource.clip.length);
        }
    }

    // Rewind by 5 seconds
    public void Rewind()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.time = Mathf.Max(audioSource.time - 5f, 0f);
        }
    }

    public void SkipForward()
    {
        if (isSkipping) return; // Prevent skipping again before the previous skip is done
        isSkipping = true;

        // Stop the current track
        audioSource.Stop();

        // Move to the next track, but don't exceed the list size
        currentTrackIndex = (currentTrackIndex + 1) % audioClips.Count;

        // Set and play the new current track
        SetTrack(currentTrackIndex);
        PlayAudio();

        // Reset skipping flag after the new track starts playing
        StartCoroutine(ResetSkippingFlag());
    }

    public void SkipBackward()
    {
        if (isSkipping) return; // Prevent skipping again before the previous skip is done
        isSkipping = true;

        // Stop the current track
        audioSource.Stop();

        // Move to the previous track, but don't go below 0
        currentTrackIndex = (currentTrackIndex - 1 + audioClips.Count) % audioClips.Count;

        // Set and play the new current track
        SetTrack(currentTrackIndex);
        PlayAudio();

        // Reset skipping flag after the new track starts playing
        StartCoroutine(ResetSkippingFlag());
    }

    private void SetTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < audioClips.Count)
        {
            audioSource.clip = audioClips[trackIndex];
            trackNameText.text = audioClips[trackIndex].name;

            // Reset the progress bar
            progressBar.value = 0;
        }
        else
        {
            Debug.LogError("Track index out of bounds");
        }
    }

    private IEnumerator ResetSkippingFlag()
    {
        yield return new WaitForSeconds(0.5f);  // Adjust delay to a reasonable time to prevent double skipping
        isSkipping = false;
    }

    public void OnProgressBarChanged(float value)
    {
        if (audioSource != null && audioSource.clip != null && !isDragging)
        {
            audioSource.time = value * audioSource.clip.length;
        }
    }

    void CloseMediaPlayer()
    {
        // Stop the audio and animation when the panel is closed
        audioSource.Stop();
        if (soundbarAnimation != null)
        {
            soundbarAnimation.Stop();
        }

        // Hide the media player panel
        if (mediaPlayerPanel != null)
        {
            mediaPlayerPanel.SetActive(false);
        }

        // Reset play/pause icon to play state
        playPauseButtonImage.sprite = playIcon;
        isPlaying = false;
    }

}
