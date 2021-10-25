using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    /// <TODO>
    /// new high score handler, add that it manages s/fx, for both high score and no new high score
    /// </summary>

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;

    [SerializeField] AudioClip noNewRecordSound;
    [SerializeField] AudioSource noNewRecordPlayer;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        int highscore = 0;
        bool newHighscore = false;

        switch (gameManager.Settings.GameMode)
        {
            case GameModes.normal:
                highscore = PlayerPrefs.GetInt("highscore");
                if (gameManager.Score > highscore)
                {
                    highscore = gameManager.Score;
                    PlayerPrefs.SetInt("highscore", highscore);
                    newHighscore = true;
                }
                break;
            case GameModes.peaceful:
                highscore = PlayerPrefs.GetInt("freeRoamHighscore");
                if (gameManager.Score > highscore)
                {
                    highscore = gameManager.Score;
                    PlayerPrefs.SetInt("freeRoamHighscore", highscore);
                    newHighscore = true;
                }
                break;
            case GameModes.sandbox:
                Debug.Log("Gamemode not done in end screen");
                break;
            case GameModes.chaos:
                Debug.Log("Gamemode not done in end screen");
                break;
            default:
                Debug.LogError("gamemode not found in switch " + gameManager.Settings.GameMode);
                break;
        }

        highscoreText.text = highscore.ToString();
        scoreText.text = gameManager.Score.ToString();

        if (newHighscore)
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
