using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <TODO>
/// we will be redoing this to a DontDestroyOnLoad or perhaps a Singleton
/// game duration
/// move game timer to its own script
/// convert scene loader to DontDestroyOnLoad or Singleton
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
	private float gameTimer = 0f;

	[SerializeField] private GameSettings settings;
	private int score = 0;
	private bool isGameOver = false;
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
	private void Update()
	{
        if (settings.TimerOff)
        {
			return;
        }
		// check if times up 
		if (isGameOver) // this might not be needed.
		{
			// return/ exit and don't run the code below.
			return;
		}

		// increase our gameTimer
		gameTimer += Time.deltaTime;

		// check if gameTimer is larger then gameDuration
		if (gameTimer > settings.GameDuration)
		{
			Debug.Log("Time Over!");

			// set game over.
			isGameOver = true;

			// Finish the sandwich
			FinishSandwich(false);
		}
	}

	public void FinishSandwich(bool Finished)
    {
        // go trough every food that is on the plate on the sandwich
        foreach (var food in plate.SandwichPieces)
        {
            // get the score for each food.
            score += food.GetComponent<FoodScore>().GetScore();
        }

        if (!Finished)
        {
            score -= 1; // make this a changable variable in a score related script.
            score = (int)Mathf.Clamp(score, 0, Mathf.Infinity); // remove this clamp (as we want to allow for negative scores)
        }

        // load our EndScene
        SceneLoader.LoadScene("EndScene");
    }
}
