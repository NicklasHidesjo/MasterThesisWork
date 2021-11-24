using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
	// load a specific scene using its name
	public static IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void Quit()
	{
		Application.Quit();
	}

	// returns the name of the currently active scene.
	public static string GetSceneName()
    {
		return SceneManager.GetActiveScene().name;
    }
}
