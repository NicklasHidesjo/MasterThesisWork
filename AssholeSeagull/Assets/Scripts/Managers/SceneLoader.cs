using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}
	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public string GetSceneName()
    {
		return SceneManager.GetActiveScene().name;
    }
}
