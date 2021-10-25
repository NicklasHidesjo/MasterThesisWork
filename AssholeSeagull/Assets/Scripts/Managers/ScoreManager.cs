using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private int unfinishedPunishment = 10;
	private Plate plate;
	private GameManager gameManager;

    void Start()
    {
		plate = FindObjectOfType<Plate>();
		gameManager = FindObjectOfType<GameManager>();
	}
	public void FinishSandwich(bool Finished)
	{
		int score = 0;
		gameManager.Score = 0;

		// go trough every food that is on the plate on the sandwich
		foreach (var food in plate.SandwichPieces)
		{
			// get the score for each food.
			score += food.GetComponent<FoodScore>().GetScore();
		}

		if (!Finished)
		{
			score -= unfinishedPunishment;
		}

		gameManager.Score = score;
		// load our EndScene
		SceneLoader.LoadScene("EndScene");
	}
}
