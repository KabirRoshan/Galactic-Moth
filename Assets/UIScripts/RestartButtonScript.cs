// This script is for the restart button to reset the scene and score
// also used to return to main menu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour {

    public void restartScene()
    {
        SceneManager.LoadScene("UfoGame");
        ScoreScript.scoreValue = 0;
        AudioListener.pause = false;
    } // restartScene

    // Method for the Main menu button
    public void returnMainMenuScene()
    {
        // Loads Main Menu scene
        SceneManager.LoadScene("Menu");
        ScoreScript.scoreValue = 0;
        AudioListener.pause = false;
    } // returnMainMenu
}
