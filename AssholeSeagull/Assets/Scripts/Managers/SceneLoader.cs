using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

	public void ReloadScene ()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	// load a specific scene using its name
	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void Quit()
	{
		Application.Quit();
	}

	// returns the name of the currently active scene.
	public string GetSceneName()
    {
		return SceneManager.GetActiveScene().name;
    }
}
