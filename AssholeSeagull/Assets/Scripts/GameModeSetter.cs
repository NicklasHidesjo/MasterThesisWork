using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class GameModeSetter : MonoBehaviour
{
	[SerializeField] private SteamVR_LaserPointer rightHand;
	[SerializeField] private SteamVR_LaserPointer leftHand;

	[SerializeField] private GameObject border;
	[SerializeField] private List<Transform> borderPositions = new List<Transform>();

	[SerializeField] private List<GameSettings> gameModes = new List<GameSettings>();

	void Start()
	{
		rightHand.PointerClick += PointerClick;
		leftHand.PointerClick += PointerClick;

		MenuVoiceRec.SetGameMode += SetGameMode;

		string lastGameMode = PlayerPrefs.GetString("LastGameMode", "Normal");

		SetGameMode(lastGameMode);
	}

	private void SetGameMode(string gameMode)
	{
		Debug.Log(gameMode);

		for (int index = 0; index < borderPositions.Count; index++)
		{
			if (gameMode == borderPositions[index].name)
			{
				border.transform.position = borderPositions[index].position;
				GameManager.Settings = gameModes[index];
				PlayerPrefs.SetString("LastGameMode", gameMode);
				break;
			}
		}
		// play the sound
	}

	private void PointerClick(object sender, PointerEventArgs e)
	{
		switch (e.target.name)
		{
			case "Normal":
				SetGameMode("Normal");
				break;

			case "Sandbox":
				SetGameMode("Sandbox");
				break;

			case "Chaos":
				SetGameMode("Chaos");
				break;

			default:
				Debug.Log(e.target.name + " did not exist in our switch!");
				break;
		}
	}

    private void OnDisable()
    {
        MenuVoiceRec.SetGameMode -= SetGameMode;
    }
}
