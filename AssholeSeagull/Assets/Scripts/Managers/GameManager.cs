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

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameSettings settings = null;
	private int score = 0;

	public int Score
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

	public GameSettings Settings
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
	private void Awake()
	{
		GameManager[] gameManagers = FindObjectsOfType<GameManager>();
		if (gameManagers.Length > 1)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

	}
}
