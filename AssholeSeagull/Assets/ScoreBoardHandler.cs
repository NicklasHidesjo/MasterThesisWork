using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardHandler : MonoBehaviour
{
    const string normalPrivateCode = "kvTPuUPhA0ej7pKCVPGXIw5AVLBzaeFEOIcIsPdjfNWg";
    const string normalPublicCode = "618e3af78f40bb127867c5c0";

    void Start()
    {
        //GameManager.Score = 1000;

        NormalScoreBoard.DownloadDone += ShowScoreBoard;

        LoadScoreBoard();
    }

    private static void LoadScoreBoard()
    {
        switch (GameManager.Settings.GameMode)
        {
            case GameModes.normal:
                NormalScoreBoard.GetScoreBoardResult(normalPublicCode);
                break;
            case GameModes.sandbox:
                break;
            case GameModes.peaceful:
                break;
            case GameModes.chaos:
                break;
        }
    }

    // refactor this shizz
    private void ShowScoreBoard(List<HighScore> highScores)
    {
        // if highscore isnt null.
        if(highScores != null)
        {
            // (if we check highscore in main menu, display all highscore lists)
            // (this is further development)
            
            // get what name the scoreboard should have (based on gamemode) 
            string scoreBoardName = GameManager.Settings.GameMode.ToString();

            int score = GameManager.Score;
            
            // make name unique for leaderboard (otherwise it will override and not show properly)
            string uniqueName = GetUniqueName();
            
            if (highScores.Count > 0)
            {
                if (score > highScores[highScores.Count - 1].score)
                {
                    // what position are we in.
                    int position = highScores.Count;
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
            }
            else
            {
                AddScoreToBoard(new HighScore(uniqueName, score));
            }
            // if not show your score under the highscores.
        }
        else
        {
            Debug.Log("Could not retrieve highscore");
        }
        // else
        // let player know that they couldnt retrieve scoreboard
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
            case GameModes.normal:
                NormalScoreBoard.AddToScoreBoard(highScore, normalPrivateCode);
                break;
            case GameModes.sandbox:
                break;
            case GameModes.peaceful:
                break;
            case GameModes.chaos:
                break;
        }
    }

    private void OnDestroy()
    {
        NormalScoreBoard.DownloadDone -= ShowScoreBoard;
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
