using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <TODO>
/// make scripts get & set currentGameStatus
/// </summary>

public static class GameManager
{
	private static GameStatus currentGameStatus = GameStatus.none;
    private static GameSettings settings = (GameSettings)ScriptableObject.CreateInstance("GameSettings");
	private static int score = 0;

	public static int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
		}
	}

	public static GameSettings Settings
	{
		get
		{
			return settings;
		}
		set
		{
			Debug.Log(value);
			settings = value;
		}
	}

	public static GameStatus CurrentGameStatus
    {
		get 
		{
            if (currentGameStatus == GameStatus.none)
            {
                SetCurrentGameStatus();
            }
            return currentGameStatus;
		}
		set
        {
			currentGameStatus = value;
        }
    }

    private static void SetCurrentGameStatus()
    {
        switch (SceneLoader.GetSceneName())
        {
            case "MainMenu":
                currentGameStatus = GameStatus.menu;
                break;
            case "GameScene":
                currentGameStatus = GameStatus.ingame;
                break;
            case "EndScene":
                currentGameStatus = GameStatus.gameover;
                break;
            default:
                Debug.LogError("Scene " + SceneLoader.GetSceneName() + " not found");
                break;
        }
    }
}
