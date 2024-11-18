// This script contains the txt and value of the score
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public static int scoreValue = 0;
    Text score;
    public Text highScore;

	// Use this for initialization
	void Start () {
        // Making a reference to our score text game object.
        score = GetComponent<Text>();
        // Uses PlayerPrefs to store high score or for new device set to 0
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    } // Start
	
	// Update is called once per frame
	void Update () {
        score.text = "Score: " + scoreValue; 

        // Update high score if score is greater
        if (scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            highScore.text = "High Score: " + scoreValue.ToString();
        } // if

	} // Update

    // Reset High Score
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        //highScore.text = "0";
    }
} // Class ScoreScript
