// Main Menu script allows buttons to do things
// when changing scenes use this: UnityEngine.SceneManagement
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Method for the button PlayGame
    public void PlayGame()
    {
        // Loads next scene in the build list
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } // PlayGame

    // Method for quit button
    public void QuitGame()
    {
        Debug.Log("QUIT ACTIVATED");
        Application.Quit();
    } // QuitGame
} // Class MainMenu
