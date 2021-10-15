using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	float gameTimer = 0f;
	[SerializeField] float gameDuration = 60f;
	public float GameDuration => gameDuration;

	[SerializeField] string SceneName;

	public int score = 0; // make this private
	bool isGameOver = false;
	SceneLoader sceneLoader;
	Plate plate;

	bool freeRoam;
	public bool FreeRoam => freeRoam;

	private void Start()
	{
		sceneLoader = FindObjectOfType<SceneLoader>();
		plate = FindObjectOfType<Plate>();

		// check if we are in FreeRoam
		if(sceneLoader.GetSceneName() == SceneName)
		{
			freeRoam = true;
		} // make this not rely on a string 
	}

	private void Update()
	{
		// check if times up 
		if(isGameOver) // this might not be needed.
		{
			// return/ exit and don't run the code below.
			return;
		}

		// check if we are in free roam.
		if(freeRoam)
		{
			// return/ exit and don't run the code below.
			return;
		}

		// increase our gameTimer
		gameTimer += Time.deltaTime;

		// check if gameTimer is larger then gameDuration
		if(gameTimer > gameDuration)
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
			score += food.GetScore();
		}

		// check if we are in freeRoam
		if (freeRoam)
		{
			// set our playerprefs for new highscore and also our currentScore.
			PlayerPrefs.SetInt("newHighscore", 0);
			PlayerPrefs.SetInt("currentFreeRoamScore", score);

			// get the highscore
			int highscore = PlayerPrefs.GetInt("freeRoamHighscore", 0);

			// check if we didnt finish our sandwich
			if (!Finished)
			{
				score -= 1; // make this a changable variable in a score related script.
				score = (int)Mathf.Clamp(score, 0, Mathf.Infinity); // remove this clamp (as we want to allow for negative scores)
			}

			// check if our score is higher then our highscore
			if (score > highscore)
			{
				// set the newHighscore flag and also our highscore for freeroam.
				PlayerPrefs.SetInt("newHighscore", 1);
				PlayerPrefs.SetInt("freeRoamHighscore", score);
			}

			Debug.Log("Score: " + score); // remove this.

			// load our FreeRoamEndScene.
			sceneLoader.LoadScene("FreeRoamEndScene");
		}
		else
		{
			// set our playerprefs for new highscore and also our currentScore.
			PlayerPrefs.SetInt("newHighscore", 0);
			PlayerPrefs.SetInt("currentScore", score);

			// get our highscore
			int highscore = PlayerPrefs.GetInt("highscore", 0);

			if (!Finished)
			{
				score -= 1; // make this a changable variable in a score related script.
				score = (int)Mathf.Clamp(score, 0, Mathf.Infinity); // remove this clamp (as we want to allow for negative scores)
			}

			// check if our score is higher then our highscore
			if (score > highscore)
			{
				// set the newHighscore flag and also our highscore.
				PlayerPrefs.SetInt("newHighscore", 1);
				PlayerPrefs.SetInt("highscore", score);
			}

			Debug.Log("Score: " + score); // remove this.

			// load our EndScene
			sceneLoader.LoadScene("EndScene");
		}
	}
}
