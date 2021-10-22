using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <TODO>
/// score manager that sets game managers score (so we can remove plate reference)
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
	[SerializeField] private GameSettings settings;
	private int score = 0;
	private Plate plate;

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
	private void Start()
	{
		plate = FindObjectOfType<Plate>();
	}
}
