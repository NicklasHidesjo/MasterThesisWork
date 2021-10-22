using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
	private float gameTimer = 0f;
	private float gameDuration = 0f;

	private void Start()
	{
		GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager.Settings.TimerOff)
        {
			enabled = false;
        }
		gameDuration = gameManager.Settings.GameDuration;
	}
	private void Update()
	{
		// increase our gameTimer
		gameTimer += Time.deltaTime;

		// check if gameTimer is larger then gameDuration
		if (gameTimer > gameDuration)
		{
			FindObjectOfType<ScoreManager>().FinishSandwich(false);
		}
	}
}
