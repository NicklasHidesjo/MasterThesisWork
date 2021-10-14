using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public int highscore;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        highscore = PlayerPrefs.GetInt("highscore", highscore);
    }

    void Update()
    {
        if(gameManager.score > highscore)
        {
            highscore = gameManager.score;
            Debug.Log("score: " + gameManager.score);
            Debug.Log("highscore: " + highscore);
        }

        PlayerPrefs.SetInt("highscore", highscore);
    }

    public void AddPoints(int pointsToAdd)
    {
        gameManager.score += pointsToAdd;
    }
}
