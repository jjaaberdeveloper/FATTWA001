using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadFlipbookScene()
    {
        SceneManager.LoadScene("FlipbookScene");
    }

    public void LoadTOCScene()
    {
        SceneManager.LoadScene("FlipbookScene");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void LoadAlbumScene()
    {
        SceneManager.LoadScene("AlbumScene");
    }
}
