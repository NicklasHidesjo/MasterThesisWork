using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    // check if this script is being used (remove if it's not)


    public int highscore;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        highscore = PlayerPrefs.GetInt("highscore", highscore);
    }

    void Update()
    {
        // check if our score for the round is higher then our highscore.
        if(gameManager.score > highscore)
        {
            highscore = gameManager.score;
            Debug.Log("score: " + gameManager.score);
            Debug.Log("highscore: " + highscore);
        }

        // set our highscore.
        PlayerPrefs.SetInt("highscore", highscore);
    }

    // add points so our gameManagers score.
    public void AddPoints(int pointsToAdd)
    {
        gameManager.score += pointsToAdd;
    }
}
