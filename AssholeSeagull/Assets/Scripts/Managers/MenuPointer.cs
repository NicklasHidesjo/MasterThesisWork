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

    private SceneLoader sceneLoader;
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
		buttonPlayer = FindObjectOfType<AudioSource>();
		sceneLoader = FindObjectOfType<SceneLoader>();
	}

	public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Play")
        {
            PlayButtonSound();
            sceneLoader.LoadScene("GameScene");
            Debug.Log("Button was clicked");
        }

        if(e.target.name == "Replay")
        {
            PlayButtonSound();
            sceneLoader.LoadScene("GameScene");
        }    

        if (e.target.name == "Quit")
        {
            PlayButtonSound();
            sceneLoader.Quit();
        }

        if(e.target.name == "Main Menu")
		{
            PlayButtonSound();
            sceneLoader.LoadScene(0);
		}

        if(e.target.name == "Free Roam")
        {
            PlayButtonSound();
            sceneLoader.LoadScene("FreeRoamMode");
        }
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
