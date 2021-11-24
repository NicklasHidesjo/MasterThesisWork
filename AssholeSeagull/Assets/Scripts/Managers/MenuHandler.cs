using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;


public delegate void RotateBoards(string board);
public class MenuHandler : MonoBehaviour // rename to something better
{
	// this script uses events from SteamVR_LaserPointer 
	// to see if either the right or left hand clicks any
	// buttons. 
	public static event RotateBoards RotateBoards;

	[SerializeField] private SteamVR_LaserPointer rightHand;
	[SerializeField] private SteamVR_LaserPointer leftHand;

	CorkBoardController boardController;
	private AudioSource buttonPlayer;

	private void Awake()
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
			MenuVoiceRec.Credits += Credits;
			MenuVoiceRec.Settings += Settings;
			MenuVoiceRec.HowTo += HowTo;
		}
		else if(SceneLoader.GetSceneName() == "EndScene")
		{
			MenuVoiceRec.MainMenu += MainMenu;
			MenuVoiceRec.Replay += Replay;
		}
	}
	private void GetReferences()
	{
		boardController = FindObjectOfType<CorkBoardController>();
		buttonPlayer = GetComponent<AudioSource>();
	}

	public void PointerClick(object sender, PointerEventArgs e)
	{
		if(boardController != null )
		{
			if(boardController.Rotating)
			{
				return;
			}
		}

		switch(e.target.name)
		{
			case "Play":
				Play();
				break;

			case "Replay":
				Replay();
				break;

			case "Quit":
				Quit();
				break;

			case "Main Menu":
				MainMenu();
				break;

			case "NamePoster":
				RotateBoards?.Invoke("Name");
				break;
			case "Settings":
				RotateBoards?.Invoke("Settings");
				break;
			case "How To":
				RotateBoards?.Invoke("HowTo");
				break;
			case "Credits":
				RotateBoards?.Invoke("Credits");
				break;
			case "Back":
				RotateBoards?.Invoke("Back");
				break;

			default:
				Debug.LogWarning(e.target.name + " Was not found in the Switch!");
				break;
		}
	}

	private void MainMenu()
	{
		PlayButtonSound();
		FindObjectOfType<SceneFader>().ChangeScene("MainMenu");
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
		PlayButtonSound();
		RotateBoards?.Invoke("Name");
	}

	private void Credits()
	{
		PlayButtonSound();
		RotateBoards?.Invoke("Credits");
	}
	private void Settings()
	{
		PlayButtonSound();
		RotateBoards?.Invoke("Settings");
	}
	private void HowTo()
	{
		PlayButtonSound();
		RotateBoards?.Invoke("HowTo");
	}

	private void LoadGame()
	{
		FindObjectOfType<SceneFader>().ChangeScene("NewGameScene");
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

		MenuVoiceRec.Play -= Play;
		MenuVoiceRec.QuitGame -= Quit;

		if (SceneLoader.GetSceneName() == "MainMenu")
		{
			MenuVoiceRec.ChangeName -= ChangeName;
		}
		else if (SceneLoader.GetSceneName() == "EndScene")
		{
			MenuVoiceRec.MainMenu -= MainMenu;
			MenuVoiceRec.Replay -= Replay;
		}
	}
}
