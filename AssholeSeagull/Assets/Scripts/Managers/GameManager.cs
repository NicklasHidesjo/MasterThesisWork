using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <TODO>
/// Use the GameStatus enum to track what state our game is in.
/// </summary>

public enum GameStatus
{
	menu,
	pause,
	ingame,
	gameover
}

public static class GameManager
{
	private static GameSettings settings;
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
			if(settings == null)
			{
				settings = (GameSettings)ScriptableObject.CreateInstance("GameSettings");
			}
			return settings;
		}
		set
		{
			Debug.Log(value);
			settings = value;
		}
	}
}
