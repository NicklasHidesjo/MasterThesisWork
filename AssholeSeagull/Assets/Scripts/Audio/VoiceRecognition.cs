using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
	/// <Todo>
	/// Use events in here to call on different things, like scareSeagull, pause/play, 
	/// recenter, etc.
	/// </summary>

	private KeywordRecognizer keywordRecognizer;

	private Dictionary<string, Action> actions = new Dictionary<string, Action>();

	private void Start()
	{
		GenerateKeyWords();
		InitializeSpeechRecognition();
	}

	private void GenerateKeyWords()
	{
		actions.Add("Pause", Pause);
		actions.Add("Stop", Pause);
		actions.Add("Resume", Resume);
		actions.Add("Play", Resume);

		actions.Add("Recenter", Recenter);
		actions.Add("Restart", Restart);
	}

	private void InitializeSpeechRecognition()
	{
		keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(),ConfidenceLevel.Low);

		keywordRecognizer.OnPhraseRecognized += WordRecognized;
		keywordRecognizer.Start();
	}

	private void WordRecognized(PhraseRecognizedEventArgs speech)
	{
		Debug.Log(speech.text);
		actions[speech.text].Invoke();
	}

	private void Pause()
	{
		Time.timeScale = 0;
		Debug.Log("Pausing");
	}

	private void Resume()
	{
		Time.timeScale = 1;
		Debug.Log("Resuming");
	}

	private void Restart()
	{
		SceneLoader.ReloadScene();
	}

	private void Recenter()
	{

	}

}
