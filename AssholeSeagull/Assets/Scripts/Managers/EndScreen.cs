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

    void Start()
    {
        int highscore = 0;
        bool newHighscore = false;

        switch (GameManager.Settings.GameMode)
        {
            case GameModes.normal:
                highscore = PlayerPrefs.GetInt("highscore");
                if (GameManager.Score > highscore)
                {
                    highscore = GameManager.Score;
                    PlayerPrefs.SetInt("highscore", highscore);
                    newHighscore = true;
                }
                break;
            case GameModes.peaceful:
                highscore = PlayerPrefs.GetInt("freeRoamHighscore");
                if (GameManager.Score > highscore)
                {
                    highscore = GameManager.Score;
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
                Debug.LogError("gamemode not found in switch " + GameManager.Settings.GameMode);
                break;
        }

        highscoreText.text = highscore.ToString();
        scoreText.text = GameManager.Score.ToString();

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
