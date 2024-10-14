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
    private bool wasPlayingBeforeMinimize = false;   // Tracks if audio was playing before minimization

    void Start()
    {
        if (mediaPlayerPanel != null)
        {
            mediaPlayerPanel.SetActive(false);
        }

        GetComponent<Button>().onClick.AddListener(ToggleAudio);
        skipForwardButton.onClick.AddListener(SkipForward);
        skipBackwardButton.onClick.AddListener(SkipBackward);
        closeButton.onClick.AddListener(CloseMediaPlayer);

        if (progressBar != null)
        {
            progressBar.onValueChanged.AddListener(OnProgressBarChanged);
        }

        SetTrack(0);
    }

    void Update()
    {
        if (isPlaying && !isDragging && audioSource.clip != null && audioSource.clip.length > 0)
        {
            progressBar.value = Mathf.Clamp01(audioSource.time / audioSource.clip.length);

            if (audioSource.time >= audioSource.clip.length && !audioSource.isPlaying)
            {
                SkipForward();
            }
        }
    }

    public void ToggleAudio()
    {
        if (!mediaPlayerPanel.activeSelf)
        {
            SetTrack(0);
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
        if (audioSource.clip != null && audioSource.clip.length > 0)
        {
            audioSource.Play();
            playPauseButtonImage.sprite = pauseIcon;
            isPlaying = true;

            mediaPlayerPanel.SetActive(true);
            trackNameText.text = audioSource.clip.name;

            if (soundbarAnimation != null)
            {
                soundbarAnimation.Play();
            }
        }
    }

    public void PauseAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            playPauseButtonImage.sprite = playIcon;
            isPlaying = false;

            if (soundbarAnimation != null)
            {
                soundbarAnimation.Pause();
            }
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
        if (isSkipping) return;
        isSkipping = true;

        audioSource.Stop();
        currentTrackIndex = (currentTrackIndex + 1) % audioClips.Count;

        SetTrack(currentTrackIndex);
        PlayAudio();

        StartCoroutine(ResetSkippingFlag());
    }

    public void SkipBackward()
    {
        if (isSkipping) return;
        isSkipping = true;

        audioSource.Stop();
        currentTrackIndex = (currentTrackIndex - 1 + audioClips.Count) % audioClips.Count;

        SetTrack(currentTrackIndex);
        PlayAudio();

        StartCoroutine(ResetSkippingFlag());
    }

    private void SetTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < audioClips.Count)
        {
            audioSource.clip = audioClips[trackIndex];
            trackNameText.text = audioClips[trackIndex].name;
            progressBar.value = 0;
        }
        else
        {
            Debug.LogError("Track index out of bounds");
        }
    }

    private IEnumerator ResetSkippingFlag()
    {
        yield return new WaitForSeconds(0.5f);
        isSkipping = false;
    }

    public void OnProgressBarChanged(float value)
    {
        if (audioSource != null && audioSource.clip != null && audioSource.clip.length > 0 && !isDragging)
        {
            float newTime = value * audioSource.clip.length;
            newTime = Mathf.Clamp(newTime, 0, audioSource.clip.length);

            audioSource.time = newTime;
        }
    }

    void CloseMediaPlayer()
    {
        audioSource.Stop();
        if (soundbarAnimation != null)
        {
            soundbarAnimation.Stop();
        }

        if (mediaPlayerPanel != null)
        {
            mediaPlayerPanel.SetActive(false);
        }

        playPauseButtonImage.sprite = playIcon;
        isPlaying = false;
    }

    // Called when the app gains or loses focus
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // The app has focus, update UI or resume processes if needed
            Debug.Log("App is in focus");

            // Resume audio if it was playing before minimization
            if (wasPlayingBeforeMinimize)
            {
                PlayAudio();
            }
        }
        else
        {
            // The app has lost focus (minimized or switched to another app)
            Debug.Log("App lost focus");

            // Continue playing audio in the background (do nothing if the audio is playing)
            wasPlayingBeforeMinimize = isPlaying; // Store the audio state
        }
    }

    // Called when the app is paused (minimized or backgrounded)
    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // App is being minimized or backgrounded
            Debug.Log("App is minimized or backgrounded");

            // Ensure that audio continues playing in the background
            if (isPlaying)
            {
                // Keep playing audio, so no action needed here
                Debug.Log("Audio continues playing in the background.");
            }
        }
        else
        {
            // App is resumed from the background
            Debug.Log("App has resumed from the background");

            // Resume audio if it was playing before minimization
            if (wasPlayingBeforeMinimize)
            {
                PlayAudio();
            }
        }
    }


}
