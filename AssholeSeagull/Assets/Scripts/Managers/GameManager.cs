using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float gameTimer = 0f;
    [SerializeField] float gameDuration = 60f;
    public float GameDuration => gameDuration;

    [SerializeField] string SceneName;

    public int score = 0;
    bool isGameOver = false;
    SceneLoader sceneLoader;
    Plate plate;

    bool freeRoam;
    public bool FreeRoam => freeRoam;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        plate = FindObjectOfType<Plate>();

        if(sceneLoader.GetSceneName() == SceneName)
        {
            freeRoam = true;
        }
    }

    private void Update()
    {
        if(isGameOver)
        {
            return;
        }

        if(freeRoam)
        {
            return;
        }

        gameTimer += Time.deltaTime;

        if(gameTimer > gameDuration)
        {
            Debug.Log("Time Over!");
            isGameOver = true;
            FinishSandwich(false);
        }
    }

    public void FinishSandwich(bool Finished)
    {
        foreach (var food in plate.SandwichPieces)
        {
            score += food.GetScore();
        }

        if(freeRoam)
        {
            PlayerPrefs.SetInt("newHighscore", 0);
            PlayerPrefs.SetInt("currentFreeRoamScore", score);
            int highscore = PlayerPrefs.GetInt("freeRoamHighscore", 0);

            if (!Finished)
            {
                score -= 1;
                score = (int)Mathf.Clamp(score, 0, Mathf.Infinity);
            }

            if (score > highscore)
            {
                PlayerPrefs.SetInt("newHighscore", 1);
                PlayerPrefs.SetInt("freeRoamHighscore", score);
            }

            Debug.Log("Score: " + score);

            sceneLoader.LoadScene("FreeRoamEndScene");
        }
        else
        {
            
        PlayerPrefs.SetInt("newHighscore", 0);
        PlayerPrefs.SetInt("currentScore", score);
        int highscore = PlayerPrefs.GetInt("highscore", 0);

        if(!Finished)
        {
            score -= 1;
            score = (int)Mathf.Clamp(score, 0, Mathf.Infinity);
        }

        if(score > highscore)
        {
            PlayerPrefs.SetInt("newHighscore", 1);
            PlayerPrefs.SetInt("highscore", score);
        }

        Debug.Log("Score: " + score);

        sceneLoader.LoadScene("EndScene");
        }
    }
}
