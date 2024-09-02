using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class InteractiveText : MonoBehaviour
{
    public GameObject popupPanel; // Reference to the Popup Panel
    public TextMeshProUGUI popupText; // Reference to the Text component in the Popup Panel
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip audioClip; // The audio clip to play

    public string additionalText; // Additional text to display in the popup

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnTextClick);
    }

    void OnTextClick()
    {
        popupPanel.SetActive(true); // Show the popup panel
        popupText.text = additionalText; // Set the additional text
        if (audioClip != null)
        {
            audioSource.clip = audioClip; // Set the audio clip
            audioSource.Play(); // Play the audio
        }
    }

    
}
