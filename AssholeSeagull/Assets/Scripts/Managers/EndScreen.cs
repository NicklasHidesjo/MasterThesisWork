using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    /// <TODO>
    /// want to combine end screen with free roam screen script
    /// get current score from game manager
    /// what game mode on game manager --> get high score (use a switch)
    /// do we have new high score?
    /// new high score handler, add that it manages s/fx, for both high score and no new high score
    /// update tmp, score texts
    /// </summary>


    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;

    [SerializeField] AudioClip noNewRecordSound;
    [SerializeField] AudioSource noNewRecordPlayer;
    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("currentScore").ToString();
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();

        if (PlayerPrefs.GetInt("newHighscore") == 1)
        {
            FindObjectOfType<NewHighScoreHandler>().NewHighScoreCelebration();
        }
        else
        {
            noNewRecordPlayer.clip = noNewRecordSound;
            noNewRecordPlayer.Play();
        }
    }
}
