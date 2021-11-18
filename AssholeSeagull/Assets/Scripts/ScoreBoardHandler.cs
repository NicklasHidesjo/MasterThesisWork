using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardHandler : MonoBehaviour
{
	const string normalPrivateCode = "kvTPuUPhA0ej7pKCVPGXIw5AVLBzaeFEOIcIsPdjfNWg";
	const string normalPublicCode = "618e3af78f40bb127867c5c0";

	const string sandboxPrivateCode = "R3M4oftPNkmHIjVxu9E8QQ8ZOn9mBVpUGjojSMdjn23g";
	const string sandboxPublicCode = "61922c9e8f40bb12786f1c21";

	[SerializeField] private TextMeshProUGUI scoreBoardText;

	[Header("Names")]
	[SerializeField] private TextMeshProUGUI[] names;

	[Header("Scores")]
	[SerializeField] private TextMeshProUGUI[] scores;

	NewHighScoreHandler newHighScoreHandler;
	void Start()
	{
		newHighScoreHandler = FindObjectOfType<NewHighScoreHandler>();
		GetScore.DownloadDone += ShowScoreBoard;
		LoadScoreBoard();
	}

	private static void LoadScoreBoard()
	{
		switch (GameManager.Settings.GameMode)
		{
			case GameModes.Normal:
				GetScore.GetScoreBoardResult(normalPublicCode);
				break;

			case GameModes.Sandbox:
				GetScore.GetScoreBoardResult(sandboxPublicCode);
				break;

			case GameModes.Peaceful:
				break;

			case GameModes.Chaos:

				break;

			default:
				Debug.LogError("The scoreboard for this gamemode has not been added to the switch: " + GameManager.Settings.GameMode);
				break;
		}
	}

	// refactor this shizz
	private void ShowScoreBoard(List<HighScore> highScores)
	{
		string scoreBoardName = GameManager.Settings.GameMode.ToString();

		scoreBoardText.text = scoreBoardName;

		string uniqueName = GetUniqueName();
		// if highscore isnt null.
		if(highScores != null)
		{
			// (if we check highscore in main menu, display all highscore lists)
			// (this is further development)
			int position = -1;

			// get what name the scoreboard should have (based on gamemode) 

			int score = GameManager.Score;

			// make name unique for leaderboard (otherwise it will override and not show properly)

			if (highScores.Count >= 10)
			{
				if (score > highScores[highScores.Count - 1].score)
				{
					Debug.Log("New Highscore");
					// what position are we in.
					position = highScores.Count -1;
					for (int i = 0; i < highScores.Count; i++)
					{
						if (score > highScores[i].score)
						{
							position = i;
							break;
						}
					}

					HighScore replacement = new HighScore(uniqueName, score);

					// Replace yourself with that position and move everyone else down.
					for (int i = position; i < highScores.Count; i++)
					{
						HighScore toBeReplaced = highScores[i];
						highScores[i] = replacement;
						replacement = toBeReplaced;
					}

					// if new record placement make that slot shine and do fancy stuff.

					for (int i = 0; i < highScores.Count; i++)
					{
						if (highScores[i].name == uniqueName)
						{
							uniqueName = GetUniqueName();
							i = -1;
						}
					}

					// Upload our score to online scoreboard.
					AddScoreToBoard(new HighScore(uniqueName, score));
				}
				Debug.Log("No new highscore");
			}
			else
			{
				position = highScores.Count;
				AddScoreToBoard(new HighScore(uniqueName, score));
				highScores.Add(new HighScore(uniqueName, score));
			}

			// if not show your score under the highscores.
			if(position >= 0)
            {
				newHighScoreHandler.NewHighScoreCelebration();
            }
			else
            {
				newHighScoreHandler.NoNewHighscore();
				// display our score under all the other scores and do no new highscore stuff.
            }

            for (int i = 0; i < highScores.Count; i++)
            {
				string userName = highScores[i].name;
				if(i == position)
                {

					string[] name = userName.Split(new char[] { '#' });
					userName = name[0];
					// change the border and make it fancy!
					// Effects and scale changes aswell as colours?
				}
				names[i].text = userName;
				scores[i].text = highScores[i].score.ToString();
            }
		}
		else
		{
			// let player know that they couldnt retrieve scoreboard
			Debug.Log("Could not retrieve highscore");
		}
	}

	private static string GetUniqueName()
	{
		string uniqueName = GameManager.Name;

		int number1 = Random.Range(0, 10);
		int number2 = Random.Range(0, 10);
		int number3 = Random.Range(0, 10);
		int number4 = Random.Range(0, 10);

		uniqueName += "#" + number1 + number2 + number3 + number4;
		return uniqueName;
	}

	private static void AddScoreToBoard(HighScore highScore)
	{
		switch (GameManager.Settings.GameMode)
		{
			case GameModes.Normal:
				GetScore.AddToScoreBoard(highScore, normalPrivateCode);
				break;

			case GameModes.Sandbox:
				GetScore.AddToScoreBoard(highScore, sandboxPrivateCode);
				break;

			case GameModes.Peaceful:
				break;

			case GameModes.Chaos:
				break;

			default:
				Debug.LogError("The scoreboard for this gamemode has not been added to the switch: " + GameManager.Settings.GameMode);
				break;
		}
	}

	private void OnDestroy()
	{
		GetScore.DownloadDone -= ShowScoreBoard;
	}
}

public struct HighScore
{
	public string name;
	public int score;

	public HighScore(string name, int score)
	{
		this.name = name;
		this.score = score;
	}
}
