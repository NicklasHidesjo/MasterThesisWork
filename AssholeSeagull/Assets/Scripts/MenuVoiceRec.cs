using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;


public delegate void ChangeName();
public delegate void Play();
public delegate void QuitGame();
public delegate void Replay();
public delegate void MainMenu();
public delegate void Credits();
public delegate void Settings();
public delegate void HowTo();

public delegate void SetGameMode(string gameMode);

public class MenuVoiceRec : MonoBehaviour
{
    public static event ChangeName ChangeName;
    public static event Play Play;
    public static event QuitGame QuitGame;
    public static event Replay Replay;
    public static event MainMenu MainMenu;
    public static event SetGameMode SetGameMode;
    public static event HowTo HowTo;
    public static event Settings Settings;
    public static event Credits Credits;
    
    private KeywordRecognizer actionRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Start()
    {
        GenerateKeyWords();
        InitializeSpeechRecognition();
    }

    private void GenerateKeyWords()
    {
        actions.Add("Play", CallPlay);

        actions.Add("Quit", CallQuitGame);

        actions.Add("Change Name", CallChangeName);

        actions.Add("MainMenu", CallMainMenu);
        actions.Add("Main", CallMainMenu);
        actions.Add("Menu", CallMainMenu);

        actions.Add("Replay", CallReplay);
        actions.Add("PlayAgain", CallReplay);

        actions.Add("Sandbox", CallSetSandbox);
        actions.Add("Normal", CallSetNormal);
        actions.Add("Chaos", CallSetChaos);

        actions.Add("Settings", CallSettings);
        actions.Add("Credits", CallCredits);
        actions.Add("HowTo", CallHowTo);
    }


    private void CallSetNormal()
    {
        SetGameMode?.Invoke("Normal");
    }
    private void CallSetChaos()
    {
        SetGameMode?.Invoke("Chaos");
    }
    private void CallSetSandbox()
    {
        SetGameMode?.Invoke("Sandbox");
    }

    private void CallChangeName()
    {
        ChangeName?.Invoke();
    }

    private void CallQuitGame()
    {
        QuitGame?.Invoke();
    }

    private void CallPlay()
    {
        Play?.Invoke();
    }

    private void CallReplay()
    {
        Replay?.Invoke();
    }

    private void CallMainMenu()
    {
        MainMenu?.Invoke();
    }

    private void CallSettings()
    {
        Settings?.Invoke();
    }
    private void CallCredits()
    {
        Credits?.Invoke();
    }
    private void CallHowTo()
    {
        HowTo?.Invoke();
    }

    private void InitializeSpeechRecognition()
    {
        actionRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);

        actionRecognizer.OnPhraseRecognized += WordRecognized;
        actionRecognizer.Start();
    }

    private void WordRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void StopRecognizer()
    {
        actionRecognizer.Stop();
    }
    public void StartRecognizer()
    {
        actionRecognizer.Start();
    }

    private void OnDisable()
    {
        actionRecognizer?.Dispose();
    }
}
