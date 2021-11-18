                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class MenuHandler : MonoBehaviour // rename to something better
{
    // this script uses events from SteamVR_LaserPointer 
    // to see if either the right or left hand clicks any
    // buttons. 

    [SerializeField] private SteamVR_LaserPointer rightHand;
    [SerializeField] private SteamVR_LaserPointer leftHand;

    [SerializeField] private GameObject setNameField;
    [SerializeField] private GameObject gameButtons;

    private AudioSource buttonPlayer;

    private void Start()
	{
		SubscribeToEvents();
		GetReferences();
	}
	private void SubscribeToEvents()
	{
		rightHand.PointerClick += PointerClick;
		leftHand.PointerClick += PointerClick;

        MenuVoiceRec.Play += Play;
        MenuVoiceRec.QuitGame += Quit;

        if(SceneLoader.GetSceneName() == "MainMenu")
        {
            MenuVoiceRec.ChangeName += ChangeName;
        }
        else if(SceneLoader.GetSceneName() == "EndScene")
        {
            MenuVoiceRec.MainMenu += MainMenu;
            MenuVoiceRec.Replay += Replay;
        }
	}
	private void GetReferences()
	{
        buttonPlayer = GetComponent<AudioSource>();
	}

	public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Play")
        {
            Play();
        }
        if (e.target.name == "Replay")
        {
            Replay();
        }


        if (e.target.name == "Quit")
        {
            Quit();
        }

        if (e.target.name == "Main Menu")
        {
            MainMenu();
        }

        if (e.target.name == "SetName")
        {
            ChangeName();
        }

    }

    private void MainMenu()
    {
        PlayButtonSound();
        SceneLoader.LoadScene("MainMenu");
    }

    private void Quit()
    {
        PlayButtonSound();
        SceneLoader.Quit();
    }

    private void Replay()
    {
        PlayButtonSound();
        LoadGame();
    }

    private void Play()
    {
        PlayButtonSound();
        Debug.Log(GameManager.Settings);
        LoadGame();
    }

    private void ChangeName()
    {
        setNameField.SetActive(true);
        gameButtons.SetActive(false);
    }

    private void LoadGame()
	{
		SceneLoader.LoadScene("NewGameScene");
	}

	public void PlayButtonSound()
    {
        buttonPlayer.Play();
    }

    private void OnDestroy()
	{
		UnsubscribeFromEvents();
	}
	private void UnsubscribeFromEvents()
	{
		rightHand.PointerClick -= PointerClick;
		leftHand.PointerClick -= PointerClick;
        MenuVoiceRec.ChangeName -= ChangeName;
	}
}
