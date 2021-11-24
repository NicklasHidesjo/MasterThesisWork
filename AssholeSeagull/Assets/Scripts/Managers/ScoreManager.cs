using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private int unfinishedPunishment = 10;
	private Plate plate;

    void Start()
    {
		plate = FindObjectOfType<Plate>();
	}
	public void FinishSandwich(bool Finished)
	{
		int score = 0;

		// go trough every food that is on the plate on the sandwich
		foreach (var food in plate.SandwichPieces)
		{
			// get the score for each food.
			food.GetComponent<FoodLayerTracker>().SetFoodAboveAndBelow();
			score += food.GetComponent<FoodScore>().GetScore();
		}

		if (!Finished)
		{
			score -= unfinishedPunishment;
		}

		GameManager.Score = score;
		// load our EndScene
		StartCoroutine(SceneLoader.LoadSceneAsync("NewEndScene"));
	}
}
