using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PageNavigation : MonoBehaviour
{
    public Book bookController; // Reference to the Book script
    public AudioSource audioSource; // Reference to the AudioSource
    public Button interactableTextButton; // Reference to the Button component on the interactable text
    public int targetPageNumber; // The page number where this text should be interactable

    /*
    private void Update()
    {
        // Check if the current page is the target page
        if (bookController.currentPage == targetPageNumber)
        {
            // Enable the interactable text
            interactableTextButton.interactable = true;
        }
       
    }
    */


    public void GoToPage(int pageNumber)
    {
        // Ensure the page number is within the valid range
        if (pageNumber >= 0 && pageNumber < bookController.TotalPageCount)
        {
            // Set the current page
            bookController.currentPage = pageNumber;

            // Update the book to display the correct page
            bookController.UpdateSprites();
        }
    }

    public void NextPage()
    {
        if (bookController.currentPage < bookController.bookPages.Length - 1)
        {
            bookController.currentPage++;
            bookController.UpdateSprites();

            // Play the page flip sound
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

    }

    public void PreviousPage()
    {
        if (bookController.currentPage > 1)
        {
            bookController.DragLeftPageToPoint(bookController.EndBottomRight);
            bookController.ReleasePage();

            // Play the page flip sound
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    public void JumpToPage(int pageNumber)
    {
        if (pageNumber >= 0 && pageNumber < bookController.TotalPageCount)
        {
            bookController.currentPage = pageNumber;
            bookController.UpdateSprites();
        }
    }
}
