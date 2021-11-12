using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public delegate void DownloadDone(List<HighScore> highScores);
public static class NormalScoreBoard
{
	const string webURL = "http://dreamlo.com/lb/";

	public static event DownloadDone DownloadDone;

	public static async void GetScoreBoardResult(string publicCode)
	{
		HttpClient www = new HttpClient();

		var result = await www.GetStringAsync(webURL + publicCode + "/pipe/0/10");

		List<HighScore> highScores = null;

		if (string.IsNullOrEmpty(result))
		{
			Debug.Log("Error uploading: " + result);
		}
		else
		{
			highScores = new List<HighScore>();
			
			// create the highscore list using our result string.
			string[] entries = result.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

			foreach (var line in entries)
			{
				string[] entryInfo = line.Split(new char[] { '|' });

				string[] entryName = entryInfo[0].Split(new char[] { '#' });
				string userName = entryName[0];

				int score = int.Parse(entryInfo[1]);

				highScores.Add(new HighScore(userName, score));
			}
			
			Debug.Log("Download Successful!");
		}
		
		DownloadDone?.Invoke(highScores);
	}

	public static async void AddToScoreBoard(HighScore highScore, string privateCode)
	{
		HttpClient www = new HttpClient();

		var result = await www.GetStringAsync(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(highScore.name) + "/" + highScore.score);

		if (string.IsNullOrEmpty(result))
		{
			Debug.Log("Error uploading: " + result);
		}
		else
		{
			Debug.Log("Upload Successful: " + result);
		}
	}
}


