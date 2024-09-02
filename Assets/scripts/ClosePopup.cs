using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ClosePopup : MonoBehaviour
{
    public GameObject popupPanel; // Reference to the Popup Panel

    public void ClosePanel()
    {
        popupPanel.SetActive(false); // Hide the popup panel
    }
}
