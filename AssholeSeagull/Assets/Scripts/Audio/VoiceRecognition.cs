using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public delegate void Pause(bool value);
public delegate void Restart();
public delegate void MainMenu();
public delegate void Recenter();
public delegate void Quit();
public class VoiceRecognition : MonoBehaviour
{
	/// <Todo>
	/// Use events in here to call on different things, like scareSeagull, pause/play, 
	/// recenter, etc.
	/// </summary>
	/// 
	public static event Pause pause;
	public static event Restart Restart;
	public static event MainMenu MainMenu;
	public static event Recenter Recenter;
	public static event Quit Quit;


	private KeywordRecognizer actionRecognizer;

	private Dictionary<string, Action> actions = new Dictionary<string, Action>();

	private void Start()
	{
		GenerateKeyWords();
		InitializeSpeechRecognition();
	}

	private void GenerateKeyWords()
	{
		actions.Add("Pause", CallPause);
		actions.Add("Stop", CallPause);
		actions.Add("Resume", CallResume);
		actions.Add("Play", CallResume);

		actions.Add("Recenter", CallRecenter);
		actions.Add("Restart", CallRestart);

		actions.Add("Quit", CallQuit);
		actions.Add("MainMenu", CallLoadMainMenu);
		actions.Add("Main", CallLoadMainMenu);
		actions.Add("Menu", CallLoadMainMenu);
	}

	private void InitializeSpeechRecognition()
	{
		actionRecognizer = new KeywordRecognizer(actions.Keys.ToArray(),ConfidenceLevel.Low);

		actionRecognizer.OnPhraseRecognized += WordRecognized;
		actionRecognizer.Start();
	}

	private void WordRecognized(PhraseRecognizedEventArgs speech)
	{
		Debug.Log(speech.text);
		actions[speech.text].Invoke();
	}

	private void CallPause()
	{
		pause?.Invoke(true);
	}

	private void CallResume()
	{
		pause.Invoke(false);
	}

	private void CallRestart()
	{
		Restart?.Invoke();
	}

	private void CallRecenter()
	{
		Recenter?.Invoke();
	}

	private void CallLoadMainMenu()
    {
		MainMenu?.Invoke();
    }

	private void CallQuit()
    {
		Quit?.Invoke();
    }

}
