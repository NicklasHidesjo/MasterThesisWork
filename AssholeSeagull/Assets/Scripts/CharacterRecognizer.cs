using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CharacterRecognizer : MonoBehaviour
{
	private KeywordRecognizer keywordRecognizer;

	private Dictionary<string, string> inputs = new Dictionary<string, string>();

	private NameHandler nameHandler;

	private void Awake()
	{
		GenerateKeyWords();
		nameHandler = GetComponent<NameHandler>();
		keywordRecognizer = new KeywordRecognizer(inputs.Keys.ToArray(), ConfidenceLevel.Medium);
	}

	private void OnEnable()
    {
		InitializeSpeechRecognition();
	}

	private void GenerateKeyWords()
	{
		inputs.Add("A", "A");
		inputs.Add("B", "B");
		inputs.Add("C", "C");
		inputs.Add("D", "D");
		inputs.Add("E", "E");
		inputs.Add("F", "F");
		inputs.Add("G", "G");
		inputs.Add("H", "H");
		inputs.Add("I", "I");
		inputs.Add("J", "J");
		inputs.Add("K", "K");
		inputs.Add("L", "L");
		inputs.Add("M", "M");
		inputs.Add("N", "N");
		inputs.Add("O", "O");
		inputs.Add("P", "P");
		inputs.Add("Q", "Q");
		inputs.Add("R", "R");
		inputs.Add("S", "S");
		inputs.Add("T", "T");
		inputs.Add("U", "U");
		inputs.Add("V", "V");
		inputs.Add("W", "W");
		inputs.Add("X", "X");
		inputs.Add("Y", "Y");
		inputs.Add("Z", "Z");

		inputs.Add("Enter", "Enter");
		inputs.Add("Clear", "Clear");
		inputs.Add("Delete", "Delete");
		inputs.Add("BackSpace", "BackSpace");
		inputs.Add("Space", "Space");
		inputs.Add("Dash", "Dash");
	}

	private void InitializeSpeechRecognition()
	{
		keywordRecognizer.OnPhraseRecognized += WordRecognized;
		keywordRecognizer.Start();
	}

	private void WordRecognized(PhraseRecognizedEventArgs speech)
	{
		Debug.Log(speech.text);
		nameHandler.HandleChangingName(inputs[speech.text]);
	}

	private void OnDisable()
    {
        keywordRecognizer.OnPhraseRecognized -= WordRecognized;
		keywordRecognizer.Stop();
    }
}
