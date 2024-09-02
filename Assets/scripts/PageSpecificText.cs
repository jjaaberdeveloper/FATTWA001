using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSpecificText : MonoBehaviour
{
    public Book bookController; // Reference to your Book script
    public Button interactableTextButton; // Reference to the Button component on the interactable text
    public int targetPageNumber; // The page number where this text should be interactable

    void Update()
    {
        // Check if the current page is the target page
        if (bookController.currentPage == targetPageNumber)
        {
            // Enable the interactable text
            interactableTextButton.interactable = true;
        }
        else
        {
            // Disable the interactable text
            interactableTextButton.interactable = false;
        }
    }
}
