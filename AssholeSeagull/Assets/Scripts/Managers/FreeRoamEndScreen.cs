using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeRoamEndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;

    [SerializeField] AudioClip noNewRecordSound;
    [SerializeField] AudioSource noNewRecordPlayer;
    void Start()
    {
        // Get both the current and highscore of freeroam mode.
        scoreText.text = PlayerPrefs.GetInt("currentFreeRoamScore").ToString();
        highscoreText.text = PlayerPrefs.GetInt("freeRoamHighscore").ToString();

        // check if we have a new highscore
        if (PlayerPrefs.GetInt("newHighscore") == 1)
        {
            // call a method in NewHighScoreHandler to handle a new highscore
            FindObjectOfType<NewHighScoreHandler>().NewHighScoreCelebration();
        }
        else
        {
            // set the soundclip to that of no new record.
            noNewRecordPlayer.clip = noNewRecordSound;
            // play the soundclip.
            noNewRecordPlayer.Play();
        }
    }
}
