using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	// add a reload scene method 
	// allowing players to restart.


	// loads a specific scene using it's buildindex
	public void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}

	// load a specific scene using it's name
	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	// quits the application
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
