                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class MenuPointer : MonoBehaviour // rename to something better
{
    // this script uses events from SteamVR_LaserPointer 
    // to see if either the right or left hand clicks any
    // buttons. 


    // look into the posibility for it to not require being buttons in the canvas
    // but just objects with colliders.

    [SerializeField] private SteamVR_LaserPointer rightHand;
    [SerializeField] private SteamVR_LaserPointer leftHand;

    [SerializeField] private GameSettings normalMode;
    [SerializeField] private GameSettings peacefulMode;

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
	}
	private void GetReferences()
	{
        buttonPlayer = GetComponent<AudioSource>();
	}

	public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Play")
        {
            PlayButtonSound();
            GameManager.Settings = normalMode;
            Debug.Log(GameManager.Settings);
            LoadGame();
        }
        if (e.target.name == "Free Roam")
        {
            PlayButtonSound();
            GameManager.Settings = peacefulMode;
            Debug.Log(GameManager.Settings);
            LoadGame();
        }
        if (e.target.name == "Replay")
		{
			PlayButtonSound();
			LoadGame();
		}


		if (e.target.name == "Quit")
        {
            PlayButtonSound();
            SceneLoader.Quit();
        }

        if(e.target.name == "Main Menu")
		{
            PlayButtonSound();
            SceneLoader.LoadScene("MainMenu");
		}


    }

	private void LoadGame()
	{
		SceneLoader.LoadScene("GameScene");
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
	}
}
