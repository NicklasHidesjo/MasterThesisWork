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

	[SerializeField] private GameSettings normal;
	[SerializeField] private GameSettings sandbox;

	void Start()
	{
		rightHand.PointerClick += PointerClick;
		leftHand.PointerClick += PointerClick;

		SetBorderPos();
	}

	private void SetBorderPos()
	{
		string gameMode = GameManager.Settings.GameMode.ToString();
		Debug.Log(gameMode);

		for (int index = 0; index < borderPositions.Count; index++)
		{
			if (gameMode == borderPositions[index].name)
			{
				border.transform.position = borderPositions[index].position;
				break;
			}
		}
	}

	private void PointerClick(object sender, PointerEventArgs e)
	{
		switch (e.target.name)
		{
			case "Normal":
				GameManager.Settings = normal;
				SetBorderPos();
				break;

			case "Sandbox":
				GameManager.Settings = sandbox;
				SetBorderPos();
				break;

			default:
				Debug.Log(e.target.name + " did not exist in our switch!");
				break;
		}
	}
}
